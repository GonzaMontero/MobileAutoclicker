using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerDefense.Scripts.Backend.Facebook;
using TowerDefense.Scripts.Backend.PlayerSaves;
using TowerDefense.Scripts.Utils;
using TowerDefense.Scripts.Utils.Managers;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class InGamePanelFunctions : MonoBehaviourSingletonInScene<InGamePanelFunctions>
    {
        [Header("References")]
        public GameObject EndGamePanel;
        public TextMeshProUGUI GemsEarnedText;

        private int totalWaves;
        
        public void GameEnd(int waveReached)
        {
            totalWaves = waveReached;

            GemsEarnedText.text = "You have earned " + totalWaves.ToString() + " gems";

            PlayerDataBridge.Get().GetPlayerData().PlayerGems += totalWaves;

            EndGamePanel.SetActive(true);
        }

        public void ReturnToMenu()
        {
            ASyncLoaderManager.Get().InitiateSceneLoad("Main Menu");
        }

        public void ShareFacebook()
        {
            FacebookManager.Get().FacebookSharefeed(totalWaves);
        }
    }
}