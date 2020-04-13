using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zinnia.Action;

namespace JorgeJGnz
{
    public enum State
    {
        Undefined,
        MinReached,
        Nominal,
        MaxReached
    }

    public class RangeAction : FloatAction
    {
        [Header("Theresolds")]
        public float minTheresold = 0.2f;
        public float maxTheresold = 0.5f;

        [Header("Events")]
        public UnityEvent onMinReached;
        public UnityEvent onNominalReached;
        public UnityEvent onMaxReached;
        State state = State.Undefined;

        [Header("Debugging")]
        public float currentNormalizedValue;

        // Update is called once per frame
        public void DoTransform(float receivedValue)
        {
            if (receivedValue <= minTheresold)
            {
                currentNormalizedValue = 0.0f;

                if (state != State.MinReached)
                {
                    state = State.MinReached;
                    onMinReached.Invoke(0.0f);
                }
            }
            else if (receivedValue > minTheresold && receivedValue < maxTheresold)
            {
                currentNormalizedValue = (receivedValue - minTheresold) / (maxTheresold - minTheresold);

                if (state != State.Nominal)
                {
                    state = State.Nominal;
                    onNominalReached.Invoke(currentNormalizedValue);
                }
            }
            else if (receivedValue >= maxTheresold)
            {
                currentNormalizedValue = 1.0f;

                if (state != State.MaxReached)
                {
                    state = State.MaxReached;
                    onMaxReached.Invoke(1.0f);
                }
            }

            Receive(currentNormalizedValue);
        }

    }
}