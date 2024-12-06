using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Backend.PlayerSaves;
using TowerDefense.Scripts.Frontend.UIElements;
using TowerDefense.Scripts.Utils;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.Level
{
    public class MapManager : MonoBehaviourSingletonInScene<MapManager>
    {
        [Header("References")]
        public Transform StartPoint;
        public Transform[] PathNodes;
        public Transform EndPoint;

        public int Currency;
        public int Health;
        public bool IsPaused = false;

        private void Start()
        {
            if (PlayerDataBridge.Get() != null)
            {
                Currency = PlayerDataBridge.Get().GetPlayerData().MatchStartGold;
                Health = PlayerDataBridge.Get().GetPlayerData().MatchStartHealth;
            }
            else
            {
                Currency = 50;
                Health = 5;
            }

            InGamePanelFunctions.Get().UpdateCurrency(Currency);
            InGamePanelFunctions.Get().UpdateHealth(Health);
        }

        public void IncreaseCurrency(int amount)
        {
            Currency += amount;
            Shop.Get().CurrencyUI.text = Currency.ToString();
            InGamePanelFunctions.Get().UpdateCurrency(Currency);
        }

        public void UpdateCurrency(int newCurrency)
        {
            Currency = newCurrency;
            Shop.Get().CurrencyUI.text = Currency.ToString();
            InGamePanelFunctions.Get().UpdateCurrency(Currency);
        }

        public void TogglePause(bool toggle)
        {
            IsPaused = toggle;
        }

        public void DecreaseHealth(int damage)
        {
            Health -= damage;

            InGamePanelFunctions.Get().UpdateHealth(Health);

            if (Health <= 0)
            {
                TogglePause(true);

                EnemyManager.Get().GameEnded();

                InGamePanelFunctions.Get().GameEnd(EnemyManager.Get().GetCurrentWave());

#if UNITY_EDITOR
                Debug.Log("You Lost!");
#endif
            }
        }
    }
}