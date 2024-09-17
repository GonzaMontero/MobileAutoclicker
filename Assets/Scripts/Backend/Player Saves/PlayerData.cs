using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class PlayerData
{
    //Game Stats
    public List<long> PlayerGold = new List<long>();
    public Dictionary<int, int> PlayerStatUpgradeDictionary = new Dictionary<int, int>();

    //Settings
    public int CurrentLanguage;
    public float CurrentVolume;
}
