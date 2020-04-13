using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public Transform headset;
    public Transform pointBetweenControllers;
    public Transform receiver;

    Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        receiver.position = headset.position;

        Vector3 receiverAngles = receiver.eulerAngles;
        Quaternion meanRotation = Quaternion.Lerp(headset.rotation, pointBetweenControllers.rotation, 0.5f);
        receiverAngles.y = meanRotation.eulerAngles.y;
        receiver.eulerAngles = receiverAngles;
    }
}
