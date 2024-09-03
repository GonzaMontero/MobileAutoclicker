using Autoclicker.Scripts.Backend.PlayerSaves;
using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

            if (PlayerDataBridge.Get().GetPlayerData().PlayerGold > 10000000000)
                text = PlayerDataBridge.Get().GetPlayerData().PlayerGold.ToString("0.000e+00");
            else
                text = PlayerDataBridge.Get().GetPlayerData().PlayerGold.ToString();

            thisText.text = text;
        }
    }
}