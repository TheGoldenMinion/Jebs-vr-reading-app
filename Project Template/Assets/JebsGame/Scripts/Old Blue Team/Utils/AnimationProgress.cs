using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zinnia.Action;

namespace JorgeJGnz {

    public class AnimationProgress : FloatAction
    {
        public Animator receiver;

        public void UpdateAnimation(float f)
        {
            receiver.SetFloat("progress",f);
            Receive(f);
        }
    }
}
