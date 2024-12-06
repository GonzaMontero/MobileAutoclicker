using TMPro;
using TowerDefense.Scripts.Backend.Facebook;
using TowerDefense.Scripts.Utils.Localization;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class FacebookFunctions : MonoBehaviour
    {
        public TextMeshProUGUI FacebookNameText;

        private void Start()
        {
            if (FacebookManager.Get().IsLoggedIn())
                FacebookNameText.text = Loc.ReplaceKey("key_isLogged");
        }

        public void LogIn()
        {
            FacebookManager.Get().Facebook_LogIn();
        }

        public void LogOut()
        {
            FacebookManager.Get().Facebook_LogOut();
        }

        public void ShareGame()
        {
            FacebookManager.Get().FacebookSharefeed(0);
        }
    }
}