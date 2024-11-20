using TowerDefense.Scripts.Backend;
using TowerDefense.Scripts.Backend.PlayerSaves;

using UnityEngine;

namespace TowerDefense.Scripts.Utils.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AutomaticVolumeUpdater : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            CommandManager.Get().OnSettingsChanged.AddListener(
                () => audioSource.volume = PlayerDataBridge.Get().GetPlayerData().CurrentVolume
            );
        }

        private void OnDestroy()
        {
            CommandManager.Get().OnSettingsChanged.RemoveListener(
                () => audioSource.volume = PlayerDataBridge.Get().GetPlayerData().CurrentVolume
            );
        }
    }
}