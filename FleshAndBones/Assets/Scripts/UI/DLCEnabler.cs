using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App.World.UI
{
    public class DLCEnabler : MonoBehaviour
    {
        private bool isDLCEnabled = false;
        [SerializeField]
        private TextMeshProUGUI buttonText;


        private void Awake()
        {
            isDLCEnabled = PlayerPrefs.GetInt("DLCEnabled", 0) == 1;
            buttonText.text = isDLCEnabled ? "Enabled!" : "Disabled!";
        }

        public void SwitchDLCEnabled()
        {
            if (isDLCEnabled)
            {
                isDLCEnabled = false;
            }
            else
            {
                isDLCEnabled = true;
            }
            buttonText.text = isDLCEnabled ? "Enabled!" : "Disabled!";
            PlayerPrefs.SetInt("DLCEnabled", isDLCEnabled ? 1 : 0);
        }
    }

}
