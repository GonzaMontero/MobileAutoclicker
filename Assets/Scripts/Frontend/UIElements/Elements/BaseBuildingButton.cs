using System.Collections;
using System.Collections.Generic;

using TowerDefense.Scripts.Utils.Localization;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class BaseBuildingButton : MonoBehaviour
    {
        public TextMeshProUGUI ItemText;
        public Image ItemImage;

        private int itemID;
        private int itemCost;

        public void SetData(string itemName, Sprite itemSprite, int id, int cost)
        {
            ItemText.text = Loc.ReplaceKey(itemName) + " - " + cost.ToString();
            ItemImage.sprite = itemSprite;
            itemID = id;
            itemCost = cost;
        }
    }
}

