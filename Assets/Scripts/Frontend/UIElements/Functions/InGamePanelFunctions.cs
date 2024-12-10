using TMPro;

using TowerDefense.Scripts.Backend.Facebook;
using TowerDefense.Scripts.Backend.PlayerSaves;
using TowerDefense.Scripts.Frontend.Level;
using TowerDefense.Scripts.Utils;
using TowerDefense.Scripts.Utils.Localization;
using TowerDefense.Scripts.Utils.Managers;

using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class InGamePanelFunctions : MonoBehaviourSingletonInScene<InGamePanelFunctions>
    {
        [Header("References")]
        public GameObject EndGamePanel;
        public TextMeshProUGUI GemsEarnedText;

        [Header ("GameUIReferences")]
        public TextMeshProUGUI CurrencyText;
        public TextMeshProUGUI HealthText;
        public TextMeshProUGUI WaveText;

        [Header("FacebookReferences")]
        public Button shareButton;

        private int totalWaves;
        
        public void GameEnd(int waveReached)
        {
            totalWaves = waveReached;

            GemsEarnedText.text = Loc.ReplaceKey("key_gained") + " " + totalWaves.ToString() + " " + Loc.ReplaceKey("key_gems");

            PlayerDataBridge.Get().GainGems(totalWaves);

            if(!FacebookManager.Get().IsLoggedIn())
            {
                shareButton.gameObject.SetActive(false);
            }

            EndGamePanel.SetActive(true);
        }

        public void EndGame()
        {
            MapManager.Get().TogglePause(true);

            EnemyManager.Get().GameEnded();

            InGamePanelFunctions.Get().GameEnd(EnemyManager.Get().GetCurrentWave());

            AdManager.Get().ShowForcedAdd();
        }

        public void ShareFacebook()
        {
            FacebookManager.Get().FacebookSharefeed(totalWaves);
        }

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
            WaveText.text = Loc.ReplaceKey("key_wave") + "- " + newWave.ToString();
        }

        public void ReturnToMenu()
        {
            ASyncLoaderManager.Get().InitiateSceneLoad("Main Menu");
        }
    }
}