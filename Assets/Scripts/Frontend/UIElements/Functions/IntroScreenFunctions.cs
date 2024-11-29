using TowerDefense.Scripts.Utils.Managers;

using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class IntroScreenFunctions : MonoBehaviour
    {
        [Header("References General")]
        public GameObject MainMenuPanel;
        public GameObject ShopPanel;
        public GameObject SettingsPanel;
        public GameObject TutorialPanel;

        public void LoadLevelButton(string sceneToLoad)
        {
            MainMenuPanel.SetActive(false);
            ASyncLoaderManager.Get().InitiateSceneLoad(sceneToLoad);
        }

        public void ToggleSettings(bool toggle)
        {
            MainMenuPanel.SetActive(!toggle);
            SettingsPanel.SetActive(toggle);
        }

        public void ToggleShop(bool toggle)
        {
            MainMenuPanel.SetActive(!toggle);
            ShopPanel.SetActive(toggle);
        }

        public void ToggleTutorial(bool toggle)
        {
            SettingsPanel.SetActive(!toggle);
            TutorialPanel.SetActive(toggle);
        }
    }
}