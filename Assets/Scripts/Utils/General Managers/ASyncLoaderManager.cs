using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts.Utils.Managers
{
    public class ASyncLoaderManager : MonoBehaviourSingleton<ASyncLoaderManager>
    {
        [Header("References")]
        public float CurrentLoadProgress;
        public GameObject LoadingScreenItem;
        public Slider LoadingScreenSlider;

        public override void Awake()
        {
            base.Awake();

            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        public void InitiateSceneLoad(string sceneToLoad)
        {
            StartCoroutine(LoadSceneAsync(sceneToLoad));
            LoadingScreenItem.SetActive(true);
        }

        IEnumerator LoadSceneAsync(string sceneToLoad)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);

            while (!loadOperation.isDone)
            {
                CurrentLoadProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
                LoadingScreenSlider.value = CurrentLoadProgress;
                yield return null;
            }            
        }

        private void OnSceneChanged(Scene current, Scene next)
        { 
            LoadingScreenItem.GetComponent<Canvas>().worldCamera = Camera.main;
            LoadingScreenItem.SetActive(false);
        }
    }
}