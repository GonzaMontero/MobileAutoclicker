using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Autoclicker.Scripts.Utils.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AutomaticTextLocalizerTMPro : MonoBehaviour
    {
        public string Key;

        private TextMeshProUGUI textField;

        private void Start()
        {
            textField = GetComponent<TextMeshProUGUI>();

            string s = "hi";

            textField.text = s;
        }
    }
}