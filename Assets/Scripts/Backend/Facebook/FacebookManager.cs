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
                    else
                        print("Couldn't initialize");
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
                Debug.Log("Facebook is Login!");
                string s = "client token" + FB.ClientToken + "User Id" + AccessToken.CurrentAccessToken.UserId + "token string" + AccessToken.CurrentAccessToken.TokenString;
            }
            else
            {
                Debug.Log("Facebook is not Logged in!");
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


        void DisplayUsername(IResult result)
        {
            if (result.Error == null)
            {
                string name = "" + result.ResultDictionary["first_name"];
                Debug.Log("" + name);
            }
            else
            {
                Debug.Log(result.Error);
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

                print(aToken.UserId);

                foreach (string perm in aToken.Permissions)
                {
                    print(perm);
                }
            }
            else
            {
                print("Failed to log in");
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
                print("Logging Out");
                yield return null;
            }
            print("Logout Successful");
        }

        public void FacebookSharefeed()
        {
            string url = "https:developers.facebook.com/docs/unity/reference/current/FB.ShareLink";
            FB.ShareLink(
                new Uri(url),
                "Checkout COCO 3D channel",
                "I just watched " + "22" + " times of this channel",
                null,
                ShareCallback);

        }

        private static void ShareCallback(IShareResult result)
        {
            Debug.Log("ShareCallback");
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
    }
}
