using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autoclicker.Scripts.Backend.Upgrades
{
    public abstract class Upgrade
    {
        public string Name { get; protected set; }
        public int Level { get; protected set; }
        public long BaseCost { get; protected set; }
        public long CostMultiplier {  get; protected set; }

        public Upgrade(string name, long baseCost)
        {
            Name = name;
            BaseCost = baseCost;
        }

        public long GetCurrentCost()
        {
            return (long)(BaseCost * Mathf.Pow(CostMultiplier, Level));
        }

        public virtual void UpgradeEffect()
        {

        }

        public void Enhance()
        {
            Level++;
            UpgradeEffect();
        }
    }
}