using System.Collections;
using System.Collections.Generic;

using TMPro;

using TowerDefense.Scripts.Backend.PlayerSaves;
using TowerDefense.Scripts.Utils;
using TowerDefense.Scripts.Utils.Localization;
using TowerDefense.Scripts.Utils.Managers;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class ShopMenuFunctions : MonoBehaviour
    {
        [Header("References")]
        public TextMeshProUGUI GemsText;
        public TextMeshProUGUI UpgradeHealthText;
        public TextMeshProUGUI HealthText;
        public TextMeshProUGUI UpgradeGoldText;
        public TextMeshProUGUI GoldText;

        public static int UpgradeCost = 5;

        private void Start()
        {
            GemsText.text = Loc.ReplaceKey("key_gems") + " " + PlayerDataBridge.Get().GetPlayerData().PlayerGems;

            UpgradeGoldText.text = Loc.ReplaceKey("key_upgrade") + " (" + UpgradeCost + ")" ;
            UpgradeHealthText.text = Loc.ReplaceKey("key_upgrade") + " (" + UpgradeCost + ")";

            GoldText.text = Loc.ReplaceKey("key_gold") + (' ') + PlayerDataBridge.Get().GetPlayerData().MatchStartGold;
            HealthText.text = Loc.ReplaceKey("key_health") + (' ') + PlayerDataBridge.Get().GetPlayerData().MatchStartHealth;

            PlayerDataBridge.OnGemsGained.AddListener(UpdateGemsText);
        }

        private void OnDestroy()
        {
            PlayerDataBridge.OnGemsGained.RemoveListener(UpdateGemsText);
        }

        public void UpgradeHealth()
        {
            if (!MathUtils.CanSpend(PlayerDataBridge.Get().GetPlayerData().PlayerGems,
                UpgradeCost, out int newCurrency))
                return;

            PlayerDataBridge.Get().GetPlayerData().PlayerGems = newCurrency;
            PlayerDataBridge.Get().GetPlayerData().MatchStartHealth += 3;
            GemsText.text = Loc.ReplaceKey("key_gems") + " " + PlayerDataBridge.Get().GetPlayerData().PlayerGems;

            HealthText.text = Loc.ReplaceKey("key_health") + (' ') + PlayerDataBridge.Get().GetPlayerData().MatchStartHealth;

            PlayerDataBridge.Get().QuickSaveData();
        }

        public void UpgradeGold()
        {
            if (!MathUtils.CanSpend(PlayerDataBridge.Get().GetPlayerData().PlayerGems,
                UpgradeCost, out int newCurrency))
                return;

            PlayerDataBridge.Get().GetPlayerData().PlayerGems = newCurrency;
            PlayerDataBridge.Get().GetPlayerData().MatchStartGold += 25;
            GemsText.text = Loc.ReplaceKey("key_gems") + " " + PlayerDataBridge.Get().GetPlayerData().PlayerGems;

            GoldText.text = Loc.ReplaceKey("key_gold") + (' ') + PlayerDataBridge.Get().GetPlayerData().MatchStartGold;

            PlayerDataBridge.Get().QuickSaveData();
        }

        public void UpdateGemsText()
        {
            GemsText.text = Loc.ReplaceKey("key_gems") + " " + PlayerDataBridge.Get().GetPlayerData().PlayerGems;
        }

        public void WatchAdEarnGems()
        {
            AdManager.Get().ShowRevardAd();            
        }
    }
}