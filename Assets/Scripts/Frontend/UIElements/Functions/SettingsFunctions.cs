using TowerDefense.Scripts.Backend;
using TowerDefense.Scripts.Backend.PlayerSaves;
using TowerDefense.Scripts.Utils.Localization;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class SettingsFunctions : MonoBehaviour
    {
        [Header("References")]
        public TMP_Dropdown LanguageDropdown;
        public Slider VolumeSlider;

        private void Start()
        {
            SetupDropdown();
            //SetupSlider();
        }

        #region Setups
        private void SetupDropdown()
        {
            List<TMP_Dropdown.OptionData> languageList = new List<TMP_Dropdown.OptionData>();

            LanguageDropdown.ClearOptions();

            for (short i = 0; i < Enum.GetNames(typeof(Loc.Language)).Length; i++)
            {
                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();

                Loc.Language language = (Loc.Language)i;
                data.text = language.ToString();

                languageList.Add(data);
            }

            LanguageDropdown.AddOptions(languageList);

            LanguageDropdown.onValueChanged.AddListener(delegate
            {
                Loc.CurrentLanguage = (Loc.Language)LanguageDropdown.value;
                CommandManager.Get().OnSettingsChanged.Invoke();
            });

            LanguageDropdown.value = (int)Loc.CurrentLanguage;
        }

        private void SetupSlider()
        {
            VolumeSlider.value = PlayerDataBridge.Get().GetPlayerData().CurrentVolume;

            VolumeSlider.onValueChanged.AddListener(delegate
            {
                PlayerDataBridge.Get().GetPlayerData().CurrentVolume = VolumeSlider.value;
                CommandManager.Get().OnSettingsChanged.Invoke();
            });
        }
        #endregion   
    }
}