using Autoclicker.Scripts.Backend;
using Autoclicker.Scripts.Backend.PlayerSaves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autoclicker.Scripts.Utils.Audio
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