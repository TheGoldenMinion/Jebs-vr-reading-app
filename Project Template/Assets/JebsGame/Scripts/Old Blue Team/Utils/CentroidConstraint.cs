using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zinnia.Action;

namespace JorgeJGnz {

    public enum Axis
    {
        None,
        X,
        Y,
        Z
    }

    public class FloatEvent : UnityEvent<float>
    {

    }

    public class CentroidConstraint : FloatAction
    {
        [Header("Place this")]
        public Transform receiver;

        [Header("Between this")]
        public Transform A;
        public Transform B;

        [Header("Rotation")]
        public Transform lookAt;
        public Axis axisRelationX;
        public Axis axisRelationY;
        public Axis axisRelationZ;
        public Vector3 rotationOffset;

        [Header("Events")]
        public FloatEvent onNewDistance = new FloatEvent();

        [Header("Debugging")]
        public float currentDistance;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            receiver.transform.position = (A.position + B.position) / 2.0f;
            if (lookAt != null) receiver.LookAt(lookAt);

            Vector3 receiverAngles = receiver.eulerAngles;
            Quaternion meanRotation = Quaternion.Lerp(A.rotation, B.rotation, 0.5f);

            receiverAngles = TransformRot(receiverAngles,meanRotation);

            receiverAngles += rotationOffset;

            receiver.eulerAngles = receiverAngles;

            currentDistance = Vector3.Distance(A.position, B.position);
            onNewDistance.Invoke(currentDistance);

            Receive(currentDistance);
        }

        Vector3 TransformRot(Vector3 receiverAngles, Quaternion rotation)
        {
            switch (axisRelationX)
            {
                case Axis.X:
                    receiverAngles.x = rotation.eulerAngles.x;
                    break;
                case Axis.Y:
                    receiverAngles.x = rotation.eulerAngles.y;
                    break;
                case Axis.Z:
                    receiverAngles.x = rotation.eulerAngles.z;
                    break;
            }

            switch (axisRelationY)
            {
                case Axis.X:
                    receiverAngles.y = rotation.eulerAngles.x;
                    break;
                case Axis.Y:
                    receiverAngles.y = rotation.eulerAngles.y;
                    break;
                case Axis.Z:
                    receiverAngles.y = rotation.eulerAngles.z;
                    break;
            }

            switch (axisRelationZ)
            {
                case Axis.X:
                    receiverAngles.z = rotation.eulerAngles.x;
                    break;
                case Axis.Y:
                    receiverAngles.z = rotation.eulerAngles.y;
                    break;
                case Axis.Z:
                    receiverAngles.z = rotation.eulerAngles.z;
                    break;
            }

            return receiverAngles;
        }

    }
}