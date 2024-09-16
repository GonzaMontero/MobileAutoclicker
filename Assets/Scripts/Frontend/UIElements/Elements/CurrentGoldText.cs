using Autoclicker.Scripts.Backend.PlayerSaves;

using UnityEngine;
using TMPro;

namespace Autoclicker.Scripts.Frontend.UIElements.Elements
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CurrentGoldText : MonoBehaviour
    {
        private TextMeshProUGUI thisText;

        private void Start()
        {
            thisText = GetComponent<TextMeshProUGUI>();

            PlayerDataBridge.Get().OnGoldGained.AddListener(
                () => UpdateText()
                );

            PlayerDataBridge.Get().OnGoldSpent.AddListener(
                () => UpdateText()
                );
        }

        public void UpdateText()
        {
            string text;

            if (PlayerDataBridge.Get().GetTotalGold() > 10000000000)
                text = PlayerDataBridge.Get().GetTotalGold().ToString("0.000e+00");
            else
                text = PlayerDataBridge.Get().GetTotalGold().ToString();

            thisText.text = text;
        }
    }
}