using System;
using UnityEngine;

public class SmoothScale : MonoBehaviour
{
        public float duration = 1f;
        public float speed = 2.0f;
        public float expectedScale = 2.0f;
        
        private float timer;
        private Vector3 maxScale;
        void Start()
        {
                timer = 0.0f;
                gameObject.transform.localScale = Vector3.one;
                maxScale = gameObject.transform.localScale * expectedScale;
        }

        void Update ()
        {
                if(timer < duration / 2)
                {
                        transform.localScale = Vector3.Lerp(transform.localScale, maxScale, speed * Time.deltaTime);
                        timer += Time.deltaTime;
                }
                else if(duration / 2 <= timer && timer < duration)
                {
                        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, speed * Time.deltaTime);
                        timer += Time.deltaTime;
                }
                else
                {
                        this.enabled = false;
                        timer = 0.0f;
                }
        }
}