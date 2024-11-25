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
        public TextMeshProUGUI WaveText;

        private void Start()
        {
            CurrencyText.text = "Gold - " + MapManager.Get().Currency.ToString();
            WaveText.text = "Wave - " + EnemyManager.Get().GetCurrentWave().ToString();
        }

        public void UpdateCurrency(int NewCurrency)
        {
            CurrencyText.text = "Gold - " + NewCurrency.ToString();
        }

        public void UpdateWave(int NewWave)
        {
            WaveText.text = "Wave - " + NewWave;
        }
    }
}