using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class PlayerData
{
    //Game Stats
    public List<long> PlayerGold = new List<long>();

    //Settings
    public int CurrentLanguage;
    public float CurrentVolume;
}
