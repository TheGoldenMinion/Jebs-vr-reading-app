using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityResistance : MonoBehaviour
{
    [Header("Objects")]
    public Collider receiver;
    public Collider floorCollider;

    [Header("Control")]
    public Transform forceFieldCenter; // Asuming receiver is inside it when it's behind field's forward
    public float linearDepthWeight = 1.0f;
    public float angularDepthWeight = 1.0f;
    public float maxDepth = 0.3f;
    public float zeroVelocityTheresold = 0.01f;

    Vector3 lastValidPosition, lastValidVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (receiver != null)
        {
            Rigidbody rb = receiver.attachedRigidbody;

            Vector3 heading = forceFieldCenter.position - rb.position;

            // forceField forward is heading to ground
            if (Vector3.Dot(heading, forceFieldCenter.forward) < 0)
            {
                Vector3 surfacePoint = Math3D.ProjectPointOnPlane(forceFieldCenter.forward,forceFieldCenter.position,rb.position);
                float depth = Vector3.Distance(surfacePoint, rb.position);

                Debug.DrawLine(rb.position, surfacePoint, Color.yellow);

                float lerp = Mathf.InverseLerp(0.0f, maxDepth, depth);
                Debug.Log("Entre 0 y " + maxDepth + ", " + depth + " representa el " + lerp * 100.0f + "%");

                // New velocity
                Vector3 v;
                if (rb.velocity.magnitude > zeroVelocityTheresold)
                    v = rb.velocity / (1 + (linearDepthWeight * depth));
                else
                    v = Vector3.zero;

                // New angular velocity
                Vector3 av;
                if (rb.velocity.magnitude > zeroVelocityTheresold)
                    av = rb.angularVelocity / (1 + (angularDepthWeight * depth));
                else
                    av = Vector3.zero;

                // New position
                Vector3 p = lastValidPosition + v * Time.fixedDeltaTime;

                rb.velocity = v;
                rb.angularVelocity = av;
                rb.position = p;
            }

            lastValidPosition = rb.position;
            lastValidVelocity = rb.velocity;
            // lastValidAngularVelocity?
        }
    }

    public void SetReceiver(Collider c)
    {
        receiver = c;

        lastValidPosition = c.attachedRigidbody.position;
        lastValidVelocity = c.attachedRigidbody.velocity;

        Physics.IgnoreCollision(receiver, floorCollider, true);
    }

    public void ResetReceiver()
    {
        if (receiver != null) Physics.IgnoreCollision(receiver, floorCollider, false);

        receiver = null;
    }
}
