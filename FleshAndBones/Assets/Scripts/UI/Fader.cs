using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.World.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private List<Image> fadingImage;
        [Range(0, 1)]
        [SerializeField] private float initialAlpha;

        private float Alpha
        {
            get => fadingImage[0].color.a;
            set
            {
                var prevColor = fadingImage[0].color;
                fadingImage.ForEach(image => image.color = new Color(prevColor.r, prevColor.g, prevColor.b, value));
            }
        }

        private void Awake()
        {
            if (fadingImage.Count == 0)
                throw new System.InvalidOperationException("List of fading images must not be empty.");
            Alpha = initialAlpha;
        }

        public void FadeToOneSeconds(float seconds) => FadeToSeconds(1f, seconds);

        public void FadeToZeroSeconds(float seconds) => FadeToSeconds(0f, seconds);

        public void FadeToSeconds(float newAlpha, float seconds)
            => StartCoroutine(FadeToSecondsCoroutine(newAlpha, seconds));

        public IEnumerator FadeToSecondsCoroutine(float newAlpha, float seconds)
        {
            var steps = seconds / Time.fixedDeltaTime;
            var speed = (newAlpha - Alpha) / seconds;

            for (; steps > 0; --steps)
            {
                Alpha += speed * Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            Alpha = newAlpha;
        }

        public void SetAlpha(float a)
        {
            if (a < 0f) a = 0f;
            if (a > 1f) a = 1f;
            Alpha = a;
        }
    }
}