using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Backend.PlayerSaves;
using UnityEngine;
using UnityEngine.Advertisements;

namespace TowerDefense.Scripts.Utils.Managers
{
    public class AdManager : MonoBehaviourSingleton<AdManager>, IUnityAdsInitializationListener
        , IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public ADItem IOSItem;
        public ADItem AndroidItem;

        private string IDSelected;
        private string IDRewardAddSelected;
        private string IDForcedAddSelected;

        public bool TestMode = false;

        public override void Awake()
        {
            base.Awake();

            InitAdds();
        }

        private void InitAdds()
        {
#if UNITY_ANDROID
            IDSelected = AndroidItem.PlatformID;
            IDRewardAddSelected = AndroidItem.RewardAddsID;
            IDForcedAddSelected = AndroidItem.ForcedAddID;
#elif UNITY_IOS
            IDSelected = IOSItem.PlatformID;
            IDRewardAddSelected = IOSItem.RewardAddsID;
            IDForcedAddSelected = IOSItem.ForcedAddID;
#elif UNITY_EDITOR
            IDSelected = AndroidItem.PlatformID;
            IDRewardAddSelected = AndroidItem.RewardAddsID;
            IDForcedAddSelected = AndroidItem.ForcedAddID;
#endif
            if (!Advertisement.isInitialized)
                Advertisement.Initialize(IDSelected, TestMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Successful Ads Init");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Failed adds " + message);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Advertisement.Show(placementId, this);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log("Add Failed to Load with Message " + message);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log("Add Failed to Show with Message " + message);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("Add Shown Successfully");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log("Add Clicked Successfully");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if(placementId.Equals(IDRewardAddSelected) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                PlayerDataBridge.Get().GetPlayerData().PlayerGems += 5;
            }
        }

        public void ShowRevardAd()
        {
            Advertisement.Load(IDRewardAddSelected, this);
        }

        public void ShowForcedAdd()
        {
            Advertisement.Load(IDForcedAddSelected, this);
        }
    }

    [System.Serializable]
    public class ADItem
    {
        public string PlatformID;
        public string RewardAddsID;
        public string ForcedAddID;
    }
}