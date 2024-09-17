using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Autoclicker.Scripts.Utils;
using Autoclicker.Scripts.Utils.Localization;
using UnityEngine.Events;
using System.Numerics;
using Unity.VisualScripting;

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
            _filePath.Replace("\\", "/");
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

        public void GainGold(long goldGained)
        {
            long goldNeeds = 0;

            while (goldGained > 0)
            {
                goldNeeds = long.MaxValue - _playerData.PlayerGold[^1]; //goldNeeds = amount of gold needed to reach the long.max value

                if (goldGained - goldNeeds < 0) //if we need more gold than what we currently are gaining
                {
                    _playerData.PlayerGold[^1] += goldGained;
                    break;
                }
                else                            //if we have more gold than what we need to reach long.max
                {
                    _playerData.PlayerGold[^1] = long.MaxValue;
                    _playerData.PlayerGold.Add(0);
                    goldGained -= goldNeeds;
                }
            }

            OnGoldGained?.Invoke();
        }

        public bool OnSpendGold(long goldSpent)
        {
            long goldNeeds = 0;

            goldNeeds = _playerData.PlayerGold[^1] - goldSpent;

            if (goldNeeds > 0)
            {
                _playerData.PlayerGold[^1] -= goldSpent;
                OnGoldSpent?.Invoke();
                return true;
            }
            else
            {
                if (_playerData.PlayerGold.Count > 1)
                {
                    goldSpent -= _playerData.PlayerGold[^1];
                    _playerData.PlayerGold.RemoveAt(_playerData.PlayerGold.Count - 1);
                    _playerData.PlayerGold[^1] -= goldSpent;
                    OnGoldSpent?.Invoke();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public BigInteger GetTotalGold()
        {
            BigInteger total = 0;

            for (int i = 0; i < _playerData.PlayerGold.Count; i++)
            {
                total += _playerData.PlayerGold[i];
            }

            return total;
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

            startingData.PlayerGold.Add(0);

            return startingData;
        }

        #endregion
    }
}