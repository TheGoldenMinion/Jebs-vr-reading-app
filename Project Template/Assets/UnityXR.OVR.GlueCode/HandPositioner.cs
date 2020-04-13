using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPositioner : MonoBehaviour
{
    [Header("Move this")]
    public Transform receiver;

    [Header("Here")]
    public UnityEngine.XR.XRNode node = UnityEngine.XR.XRNode.LeftHand;

    UnityEngine.XR.Hand receivedHandData;
    UnityEngine.XR.Bone receivedRootBone;
    Vector3 receivedPosition;
    Quaternion receivedRotation;
    bool successfulHandReading, successfullRootBoneReading, successfulPositionReading, successfulRotationReading;

    UnityEngine.XR.InputDevice device;

    void Update()
    {
        var devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(node, devices);

        // If there is only one instance for this controller
        if (devices.Count == 1)
        {
            device = devices[0];

            // We read hand, then root bone and the position and rotation of that bone
            successfulHandReading = device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.handData, out receivedHandData);
            
            if (successfulHandReading)
            {
                successfullRootBoneReading = receivedHandData.TryGetRootBone(out receivedRootBone);

                if (successfullRootBoneReading)
                {
                    successfulPositionReading = receivedRootBone.TryGetPosition(out receivedPosition);
                    successfulRotationReading = receivedRootBone.TryGetRotation(out receivedRotation);
                }
            }

            // If we can get the buttons state succesfully
            if (successfulPositionReading && successfulRotationReading)
            {
                receiver.position = receivedPosition;
                receiver.rotation = receivedRotation;
            }
        }
        else if (devices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }
    }
}
