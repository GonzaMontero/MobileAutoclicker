using TowerDefense.Scripts.Backend;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDefense.Scripts.Utils.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AutomaticTextLocalizerTMPro : MonoBehaviour
    {
        public string Key;

        private TextMeshProUGUI textField;

        private void Start()
        {
            textField = GetComponent<TextMeshProUGUI>();

            string s = Loc.ReplaceKey(Key);

            textField.text = s;

            CommandManager.Get().OnSettingsChanged.AddListener(
                () => textField.text = Loc.ReplaceKey(Key)
            );
        }

        private void OnDestroy()
        {
            CommandManager.Get().OnSettingsChanged.RemoveListener(
                () => textField.text = Loc.ReplaceKey(Key)
            );
        }
    }
}