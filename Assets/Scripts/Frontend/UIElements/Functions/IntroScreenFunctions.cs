using TowerDefense.Scripts.Utils.Managers;

using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class IntroScreenFunctions : MonoBehaviour
    {
        [Header("References General")]
        public GameObject MainMenu;

        [Header("References Loading Scene")]
        public GameObject LoadingScene;
        public Slider loadingSlider;

        private bool _isLoadingScene = false;

        public void LoadLevelButton(string sceneToLoad)
        {
            MainMenu.SetActive(false);
            LoadingScene.SetActive(true);

            ASyncLoaderManager.Get().InitiateSceneLoad(sceneToLoad);

            _isLoadingScene = true;
        }

        private void Update()
        {
            if (_isLoadingScene)
                loadingSlider.value = ASyncLoaderManager.Get().CurrentLoadProgress;
        }
    }
}