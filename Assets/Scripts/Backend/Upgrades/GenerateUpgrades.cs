using Autoclicker.Scripts.Backend.PlayerSaves;
using Autoclicker.Scripts.Frontend.UIElements;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Autoclicker.Scripts.Backend.Upgrades
{
    public class GenerateUpgrades : MonoBehaviour
    {
        public UpgradeItem UpgradeItemPrototype;

        private List<UpgradeItem> upgrades;
        private bool upgradesGenerated = false;

        public void OnEnable()
        {            
            if(!upgradesGenerated)
            {
                Upgrade current = null;

                for (short i = 0; i < PlayerDataBridge.Get().GetPlayerData().PlayerUpgrades.Count; i++)
                {
                    current = PlayerDataBridge.Get().GetPlayerData().PlayerUpgrades[i];
                    GameObject temp = Instantiate(UpgradeItemPrototype.gameObject, transform);
                    temp.GetComponent<UpgradeItem>().SetData(current.Name, current.Level);
                    upgrades.Add(temp.GetComponent<UpgradeItem>());
                }

                UpgradeItemPrototype.gameObject.SetActive(false);

                upgradesGenerated = true;
            }
        }

    }
}