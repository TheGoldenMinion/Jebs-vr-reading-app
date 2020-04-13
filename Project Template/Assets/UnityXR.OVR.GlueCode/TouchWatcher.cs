using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class TouchWatcher : BooleanAction
{
    [Header("Listen to")]
    public UnityEngine.XR.XRNode node = UnityEngine.XR.XRNode.LeftHand;
    public Usage usage = Usage.Grip;

    bool savedState = false;
    bool retrievedState;
    bool successfulReading;

    UnityEngine.XR.InputDevice device;

    [Header("Debugging")]
    public bool currentValue;

    void Update()
    {
        var devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(node, devices);

        // If there is only one instance for this controller
        if (devices.Count == 1)
        {
            device = devices[0];

            // We read different attributes of the class CommonUsages according to "usage" desired
            switch (usage)
            {
                case Usage.Trigger:
                    successfulReading = device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out retrievedState);
                    break;
                case Usage.Grip:
                    successfulReading = device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryTouch, out retrievedState);
                    break;
                default:
                    successfulReading = false;
                    break;
            }

            // If we can get the buttons state succesfully
            if (successfulReading)
            {
                // If the buttons is pressed after having been unpressed
                if (retrievedState && !savedState)
                {
                    savedState = true;
                    Debug.Log("Trigger button is touched");
                    Receive(true);
                }

                // If the buttons is unpressed after having been pressed
                if (!retrievedState && savedState)
                {
                    savedState = false;
                    Debug.Log("Trigger button is untouched");
                    Receive(false);
                }
            }
        }
        else if (devices.Count > 1)
        {
            Debug.Log("Found more than one of that hand!");
        }

        currentValue = Value;
    }
}
