using TMPro;
using UnityEngine;

namespace App.World.UI
{
    public class LoadingAnimation : MonoBehaviour
    {
        [SerializeField] private float oneDotAppearanceTime;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private string defaultText;
        private float passedTime;
        private int currentNumOfDots;
        private const int maxNumOfDots = 3;

        private void Awake()
        {
            passedTime = 0f;
            text.text = defaultText;
        }

        private void Update()
        {
            if (passedTime > oneDotAppearanceTime)
            {
                passedTime = 0f;
                AddOneDot();
            }
            else
            {
                passedTime += Time.deltaTime;
            }
        }

        private void AddOneDot()
        {
            if (currentNumOfDots == maxNumOfDots)
            {
                text.text = defaultText;
                currentNumOfDots = 0;
            }
            else
            {
                text.text += '.';
                ++currentNumOfDots;
            }
        }
    }

}
