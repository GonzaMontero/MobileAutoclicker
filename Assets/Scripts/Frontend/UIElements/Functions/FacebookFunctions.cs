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
            FacebookManager.Get().OnLoginSuccess.AddListener(UpdatePositiveText);
            FacebookManager.Get().OnLogoutSuccess.AddListener(UpdateNegativeText);

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
            FacebookManager.Get().FacebookShareMessage("Play Tower Defense - Survival!", "Currently on Closed Beta");
        }

        public void UpdatePositiveText()
        {
            FacebookNameText.text = Loc.ReplaceKey("key_isLogged");
        }

        public void UpdateNegativeText()
        {
            FacebookNameText.text = Loc.ReplaceKey("key_notLogged");
        }
    }
}