using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Autoclicker.Scripts.Utils;
using Autoclicker.Scripts.Utils.Localization;

namespace Autoclicker.Scripts.Backend.PlayerSaves
{
    public class PlayerDataManager : MonoBehaviourSingleton<PlayerDataManager>
    {
        private string _filePath;

        public override void Awake()
        {
            base.Awake();
            _filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        }

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

            return new PlayerData();
        }
    }

    public class PlayerDataBridge : MonoBehaviourSingleton<PlayerDataBridge>
    {
        private PlayerData _playerData;

        public override void Awake()
        {
            base.Awake();

            _playerData = PlayerDataManager.Get().LogIn();

            Loc.CurrentLanguage = (Loc.Language)_playerData.CurrentLanguage;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _playerData.CurrentLanguage = (int)Loc.CurrentLanguage;
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
        #endregion
    }
}