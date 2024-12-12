using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using TowerDefense.Scripts.Utils;
using TowerDefense.Scripts.Utils.Localization;
using System;
using UnityEngine.Events;

namespace TowerDefense.Scripts.Backend.PlayerSaves
{
    public class PlayerDataBridge : MonoBehaviourSingleton<PlayerDataBridge>
    {
        private PlayerData _playerData;

        private string _filePath;

        public static UnityEvent OnGemsGained;

        public override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "pData.json");
            _filePath.Replace("\\", "/");
            _playerData = LogIn();

            Debug.Log("Gems " + _playerData.PlayerGems);

            Loc.CurrentLanguage = (Loc.Language)_playerData.CurrentLanguage;

            OnGemsGained = new UnityEvent();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _playerData.CurrentLanguage = (int)Loc.CurrentLanguage;

            LogOut(_playerData);
        }

        #region Setter & Getters
        public PlayerData GetPlayerData()
        {
            return _playerData;
        }

        public void SetVolume(float volume)
        {
            _playerData.CurrentVolume = volume;
        }

        public void GainGems(int gems)
        {
            _playerData.PlayerGems += gems;
            OnGemsGained.Invoke();

            PlayerDataBridge.Get().QuickSaveData();
        }

        #endregion

        #region Player Data

        public void LogOut(PlayerData playerData)
        {
            SavePlayerData(playerData);
        }

        public void QuickSaveData()
        {
            _playerData.CurrentLanguage = (int)Loc.CurrentLanguage;

            SavePlayerData(_playerData);
        }

        private void SavePlayerData(PlayerData playerData)
        {
            string jsonDat = JsonUtility.ToJson(playerData);

            File.WriteAllText(_filePath, jsonDat);

            Debug.Log(_filePath);
        }

        public PlayerData LogIn()
        {
            PlayerData playerData = LoadPlayerData();

            return playerData;
        }

        private PlayerData LoadPlayerData()
        {
            if (File.Exists(_filePath))
            {
                Debug.Log("File Exists");

                string jsonData = File.ReadAllText(_filePath);

                return JsonUtility.FromJson<PlayerData>(jsonData);

                //return JsonConvert.DeserializeObject<PlayerData>(jsonData);
            }

            Debug.Log("File Does not exist");

            PlayerData startingData = new PlayerData();

            startingData.MatchStartGold = 50;
            startingData.MatchStartHealth = 5;

            return startingData;
        }

        #endregion
    }
}