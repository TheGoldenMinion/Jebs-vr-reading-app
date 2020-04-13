using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zinnia.Haptics;

namespace JorgeJGnz
{
    public class LoadingBar : MonoBehaviour
    {
        [Header("Configuration")]
        public Image image;
        public float maxTime = 2.0f;
        float time;

        [Header("Events")]
        public UnityEvent onFinish;
        public UnityEvent onReset;

        public bool canAnimate { get; set; }

        // Update is called once per frame
        void Update()
        {
            if (time > 0 && canAnimate)
            {
                time -= Time.deltaTime;
                image.fillAmount = time / maxTime;

                if (time <= 0.0f) onFinish.Invoke();
            }
        }

        public void ResetTimer()
        {
            time = maxTime;
            onReset.Invoke();
        }

    }
}
