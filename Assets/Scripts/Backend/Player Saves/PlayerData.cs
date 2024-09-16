using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class PlayerData
{
    //Game Stats
    public List<long> PlayerGold;
    public Dictionary<int, int> PlayerStatUpgradeDictionary;

    //Settings
    public int CurrentLanguage;
    public float CurrentVolume;
}
