using UnityEngine;

namespace TowerDefense.Scripts.Frontend.Towers
{
    [System.Serializable]
    public class TowerItem
    {
        public string Name;
        public int Cost;
        public int TowerID;
        public GameObject Prefab;
        public Sprite ShopImage;

        public TowerItem(string name, int id, int cost, GameObject prefab, Sprite shopImage)
        {
            Name = name;
            Cost = cost;
            TowerID = id;
            Prefab = prefab;
            ShopImage = shopImage;
        }
    }
}