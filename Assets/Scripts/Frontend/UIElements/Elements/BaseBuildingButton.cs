using System.Collections;
using System.Collections.Generic;
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
            ItemText.text = itemName;
            ItemImage.sprite = itemSprite;
            itemID = id;
            itemCost = cost;
        }
    }
}

