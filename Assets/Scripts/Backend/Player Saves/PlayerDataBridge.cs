using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Autoclicker.Scripts.Utils;
using Autoclicker.Scripts.Utils.Localization;
using UnityEngine.Events;
using System.Numerics;

namespace Autoclicker.Scripts.Backend.PlayerSaves
{
    public class PlayerDataBridge : MonoBehaviourSingleton<PlayerDataBridge>
    {
        private PlayerData _playerData;

        public UnityEvent OnGoldGained;
        public UnityEvent OnGoldSpent;
        public UnityEvent OnUpgradeGained;

        private string _filePath;

        public override void Awake()
        {
            base.Awake();

            _filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
            _playerData = LogIn();

            Loc.CurrentLanguage = (Loc.Language)_playerData.CurrentLanguage;

            if (OnGoldGained == null)
                OnGoldGained = new UnityEvent();

            if (OnGoldSpent == null)
                OnGoldSpent = new UnityEvent();

            if (OnUpgradeGained == null)
                OnUpgradeGained = new UnityEvent();
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

        public void GainGold(BigInteger goldGained)
        {
            _playerData.PlayerGold += goldGained;

            OnGoldGained.Invoke();
        }

        public bool OnSpendGold(BigInteger goldSpent)
        {
            if (_playerData.PlayerGold - goldSpent < 0)
                return false;

            _playerData.PlayerGold -= goldSpent;
            OnGoldSpent.Invoke();

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

            return new PlayerData();
        }

        #endregion
    }
}