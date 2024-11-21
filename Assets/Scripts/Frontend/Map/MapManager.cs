using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Backend.PlayerSaves;
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

        private void Start()
        {
            //Currency = PlayerDataBridge.Get().GetPlayerData().MatchStartGold;
            //Health = PlayerDataBridge.Get().GetPlayerData().MatchStartHealth;
        }

        public void IncreaseCurrency(int amount)
        {
            Currency += amount;
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
                return true;
            }
        }
    }
}