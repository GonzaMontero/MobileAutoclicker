using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefense.Scripts.Utils.Managers
{
    public class ASyncLoaderManager : MonoBehaviourSingleton<ASyncLoaderManager>
    {
        public float CurrentLoadProgress;
        public GameObject LoadingScreenItem;

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
                yield return null;
            }
        }
    }
}