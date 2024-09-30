using Autoclicker.Scripts.Backend.Upgrades;
using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class PlayerData
{
    //Game Stats
    public List<long> PlayerGold = new List<long>();
    public List<Upgrade> PlayerUpgrades = new List<Upgrade>();

    //Settings
    public int CurrentLanguage;
    public float CurrentVolume;
}
