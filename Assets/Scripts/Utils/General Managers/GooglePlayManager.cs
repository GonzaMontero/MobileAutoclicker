using System;
using System.Collections;
using System.Collections.Generic;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using TowerDefense.Scripts.Backend.PlayerSaves;
using UnityEngine;

namespace TowerDefense.Scripts.Utils.Managers
{
    public class GooglePlayManager : MonoBehaviourSingleton<GooglePlayManager>
    {
#if UNITY_ANDROID || PLATFORM_ANDROID
        public static PlayGamesPlatform platform = null;
#endif

        public override void Awake()
        {
            base.Awake();

#if UNITY_EDITOR || PLATFORM_STANDALONE
            return;
#endif

#if UNITY_ANDROID || PLATFORM_ANDROID
            if (platform == null)
            {
                PlayGamesPlatform.DebugLogEnabled = true;
                platform = PlayGamesPlatform.Activate();
                Debug.Log("GPGS - Play Games activated successfully");
            }
            else
                Debug.Log("GPGS - Play Games activation failed");

            LogIn(true);
#endif
        }

        public void LogIn(bool logInFromPlatform = false)
        {
#if UNITY_EDITOR || PLATFORM_STANDALONE
            return;
#endif
            if (logInFromPlatform)
            {
#if UNITY_ANDROID || PLATFORM_ANDROID
                platform.Authenticate(OnGoogleLogIn);
#endif
            }
            else
                Social.localUser.Authenticate(OnUnityLogIn);
        }

        internal void ProcessAuthentitacion(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                Debug.Log("Login Success");
            }
            else if (status == SignInStatus.Canceled)
            {
                Debug.Log("Login Canceled");
            }
            else
            {
                Debug.Log("Login Failed");
            }
        }

        public void UnlockAchievement(string achievementName)
        {
            if(!PlayGamesPlatform.Instance.IsAuthenticated())
                PlayGamesPlatform.Instance.Authenticate(ProcessAuthentitacion);

            PlayGamesPlatform.Instance.ReportProgress(achievementName, 100.0f, (bool success) =>
            {
                Debug.Log("Achievement" + achievementName + "Unlocked");
                if(success)
                {
#if UNITY_ANDROID || PLATFORM_ANDROID
                    platform.ShowAchievementsUI();
#endif
                }
            });
        }

        void OnUnityLogIn(bool success)
        {
#if UNITY_EDITOR || PLATFORM_STANDALONE
            return;
#endif
            if (success)
            {
                Debug.Log("GameSocials - Logged in successfully");
                Debug.Log("GameSocials - ID: " + Social.localUser.id);
                Debug.Log("GameSocials - Name: " + Social.localUser.userName);
            }
            else
            {
                Debug.Log("GameSocials - Failed to login");
            }
        }

#if UNITY_ANDROID || PLATFORM_ANDROID
        void OnGoogleLogIn(SignInStatus signInStatus)
        {
            if (signInStatus == SignInStatus.Success)
            {
                Debug.Log("GPGS - Logged in successfully");
                Debug.Log("GPGS - ID: " + platform.localUser.id);
                Debug.Log("GPGS - Name: " + platform.localUser.userName);
            }
            else
            {
                Debug.Log("GPGS - Failed to login: " + signInStatus);
            }
        }
#endif

    }
}