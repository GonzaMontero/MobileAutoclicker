using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerDefense.Scripts.Frontend.Level;
using TowerDefense.Scripts.Utils;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class DataHolder : MonoBehaviourSingletonInScene<DataHolder>
    {
        [Header("References")]
        public TextMeshProUGUI CurrencyText;
        public TextMeshProUGUI HealthText;
        public TextMeshProUGUI WaveText;

        public void UpdateCurrency(int newCurrency)
        {
            CurrencyText.text = "- " + newCurrency.ToString();
        }

        public void UpdateHealth(int newHealth)
        {
            HealthText.text = "- " + newHealth.ToString();
        }

        public void UpdateWave(int newWave)
        {
            WaveText.text = "Wave - " + newWave.ToString();
        }
    }
}