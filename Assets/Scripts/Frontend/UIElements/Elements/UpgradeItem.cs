using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Autoclicker.Scripts.Frontend.UIElements
{
    [RequireComponent(typeof(Button))]
    public class UpgradeItem : MonoBehaviour
    {
        public TextMeshProUGUI UpgradeNameItem;
        public TextMeshProUGUI UpgradeLevelItem;

        public void SetData(string name, int level)
        {
            UpgradeNameItem.text = name;
            UpgradeLevelItem.text = level.ToString();
        }
    }
}