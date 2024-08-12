using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autoclicker.Scripts.Utils;
using Autoclicker.Scripts.Utils.Localization;

namespace Autoclicker.Scripts.Backend.PlayerSaves
{
    public class PlayerSettingsManager : MonoBehaviourSingleton<PlayerSettingsManager>
    {
        public override void Awake()
        {
            base.Awake();

            Loc.CurrentLanguage = (Loc.Language)PlayerPrefs.GetInt("language", 0);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt("language", (int)Loc.CurrentLanguage);
            PlayerPrefs.Save();
        }
    }
}