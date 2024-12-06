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
        private Texture2D savedImage;

        public override void Awake()
        {
            base.Awake();

            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentitacion);
        }

        internal void ProcessAuthentitacion(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                Debug.Log("Login Success");
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
            });
        }
    }
}