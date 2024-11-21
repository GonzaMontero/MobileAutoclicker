using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using TowerDefense.Scripts.Utils;
using TowerDefense.Scripts.Utils.Localization;
using UnityEngine.Events;
using System.Numerics;
using Unity.VisualScripting;

namespace TowerDefense.Scripts.Backend.PlayerSaves
{
    public class PlayerDataBridge : MonoBehaviourSingleton<PlayerDataBridge>
    {
        private PlayerData _playerData;

        private string _filePath;

        public override void Awake()
        {
            base.Awake();

            _filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
            _filePath.Replace("\\", "/");
            _playerData = LogIn();

            Loc.CurrentLanguage = (Loc.Language)_playerData.CurrentLanguage;
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

        public void GainGold(long goldGained)
        {

        }

        public bool OnSpendGold(long goldSpent)
        {
            return true;
        }


        #endregion

        #region Player Data

        public void LogOut(PlayerData playerData)
        {
            SavePlayerData(playerData);

#if UNITY_EDITOR
            Debug.Log("Player data saved and encrypted");
#endif
        }

        private void SavePlayerData(PlayerData playerData)
        {
            string jsonData = JsonConvert.SerializeObject(playerData);
            string encryptedData = EncryptionUtility.Encrypt(jsonData);

            File.WriteAllText(_filePath, encryptedData);
        }

        public PlayerData LogIn()
        {
            PlayerData playerData = LoadPlayerData();

#if UNITY_EDITOR
            Debug.Log("Player data loaded and decrypted");
#endif

            return playerData;
        }

        private PlayerData LoadPlayerData()
        {
            if (File.Exists(_filePath))
            {
                string encrypredData = File.ReadAllText(_filePath);
                string jsonData = EncryptionUtility.Decrypt(encrypredData);

                return JsonConvert.DeserializeObject<PlayerData>(jsonData);
            }

            PlayerData startingData = new PlayerData();

            startingData.MatchStartGold = 50;
            startingData.MatchStartHealth = 10;

            return startingData;
        }

        #endregion
    }
}