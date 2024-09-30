using UnityEngine;

using Autoclicker.Scripts.Backend.PlayerSaves;
using Autoclicker.Scripts.Utils.Managers;

namespace Autoclicker.Scripts.Backend.Upgrades
{
    public class GoldGeneration : Upgrade
    {
        public long CurrentGeneration;

        private float growthRate = 0.15f;
        private long baseCurrencyGenerator = 10;

        public GoldGeneration(string name, long baseCost) : base(name, baseCost)
        {
            TimeManager.Get().OnSecondTick += OnSecondClick;
        }

        public long GetCurrentGeneration()
        {
            return ((long)(baseCurrencyGenerator * Mathf.Pow(1 + growthRate, Level)));
        }

        public void OnSecondClick()
        {
            PlayerDataBridge.Get().GainGold(GetCurrentGeneration());
        }
    }
}