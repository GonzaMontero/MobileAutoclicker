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
        }

        public void IncreaseCurrency(int amount)
        {
            Currency += amount;

            Shop.Get().CurrencyUI.text = Currency.ToString();

            DataHolder.Get().UpdateCurrency(Currency);
        }

        public bool DecreaseCurrency(int amount)
        {
            if(Currency < amount)
            {
                Debug.Log("No money (poor ahh)!");
                return false;
            }
            else
            {
                Currency -= amount;
                Shop.Get().CurrencyUI.text = Currency.ToString();
                DataHolder.Get().UpdateCurrency(Currency);
                return true;
            }
        }

        public void TogglePause(bool toggle)
        {
            IsPaused = toggle;
        }

        public void DecreaseHealth(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                TogglePause(true);

                Debug.Log("You Lost!");
            }
        }
    }
}