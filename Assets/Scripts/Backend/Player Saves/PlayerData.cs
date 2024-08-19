using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    //Game Stats
    public int PlayerGold;
    public Dictionary<int, int> PlayerStatUpgradeDictionary;

    //Settings
    public int CurrentLanguage;
}
