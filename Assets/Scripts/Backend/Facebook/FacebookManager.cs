using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Scripts.Utils;
using Facebook.Unity;
using System;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Backend.Facebook
{
    public class FacebookManager : MonoBehaviourSingleton<FacebookManager>
    {
        public override void Awake()
        {
            base.Awake();

            FB.Init(SetInit, onHidenUnity);

            if (!FB.IsInitialized)
            {
                FB.Init(() =>
                {
                    if (FB.IsInitialized)
                        FB.ActivateApp();
#if UNITY_EDITOR
                    else
                        print("Couldn't initialize");
#endif
                },
                isGameShown =>
                {
                    if (!isGameShown)
                        Time.timeScale = 0;
                    else
                        Time.timeScale = 1;
                });
            }
            else
                FB.ActivateApp();
        }

        void SetInit()
        {
            if (FB.IsLoggedIn)
            {

#if UNITY_EDITOR
                Debug.Log("Facebook is Login!");
#endif
                string s = "client token" + FB.ClientToken + "User Id" + AccessToken.CurrentAccessToken.UserId + "token string" + AccessToken.CurrentAccessToken.TokenString;

                Debug.Log(s);
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Facebook is not Logged in!");
#endif
            }
        }

        void onHidenUnity(bool isGameShown)
        {
            if (!isGameShown)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }


        public string DisplayUsername(IResult result)
        {
            if (result.Error == null)
            {
                string name = "" + result.ResultDictionary["first_name"];
#if UNITY_EDITOR
                Debug.Log("" + name);
#endif
                return name;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(result.Error);
#endif
                return null;
            }
        }

        public void Facebook_LogIn()
        {
            List<string> permissions = new List<string>();
            permissions.Add("public_profile");
            //permissions.Add("user_friends");
            FB.LogInWithReadPermissions(permissions, AuthCallBack);

        }

        void AuthCallBack(IResult result)
        {
            if (FB.IsLoggedIn)
            {
                SetInit();
                //AccessToken class will have session details
                var aToken = AccessToken.CurrentAccessToken;

#if UNITY_EDITOR
                print(aToken.UserId);
#endif


                foreach (string perm in aToken.Permissions)
                {
                    print(perm);
                }
            }
            else
            {
#if UNITY_EDITOR
                print("Failed to log in");
#endif
            }
        }


        public void Facebook_LogOut()
        {
            StartCoroutine(LogOut());
        }

        IEnumerator LogOut()
        {
            FB.LogOut();
            while (FB.IsLoggedIn)
            {
#if UNITY_EDITOR
                print("Logging Out");
#endif
                yield return null;
            }
#if UNITY_EDITOR
            print("Logout Successful");
#endif
        }

        public void FacebookSharefeed(int waveReached)
        {
            string url = "https://www.youtube.com";
            FB.ShareLink(
                new Uri(url),
                "Play Tower Defense: Outlast",
                "I just reached wave " + waveReached.ToString(),
                null,
                ShareCallback);

        }

        public void FacebookShareMessage(string contentTitle, string contentContents)
        {
            string url = "https://www.youtube.com";
            FB.ShareLink(
                new Uri(url),
                contentTitle,
                contentContents,
                null,
                ShareCallback);
        }

        private static void ShareCallback(IShareResult result)
        {
#if UNITY_EDITOR
            Debug.Log("ShareCallback");
#endif
            SpentCoins(2, "sharelink");
            if (result.Error != null)
            {
                Debug.LogError(result.Error);
                return;
            }
            Debug.Log(result.RawResult);
        }

        public static void SpentCoins(int coins, string item)
        {
            var param = new Dictionary<string, object>();
            param[AppEventParameterName.ContentID] = item;
            FB.LogAppEvent(AppEventName.SpentCredits, (float)coins, param);
        }

        public bool IsLoggedIn()
        {
            return FB.IsLoggedIn;
        }
    }
}
