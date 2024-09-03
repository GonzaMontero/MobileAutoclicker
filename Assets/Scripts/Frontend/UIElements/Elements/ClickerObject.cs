using Autoclicker.Scripts.Backend.PlayerSaves;
using Autoclicker.Scripts.Utils;

using UnityEngine;
using UnityEngine.UI;

namespace Autoclicker.Scripts.Frontend.UIElements.Elements
{
    [RequireComponent(typeof(Button))]
    public class ClickerObject : MonoBehaviourSingletonInScene<ClickerObject>
    {
        [Header("References")]
        public GameObject ClickElement;
        public Button ItemButton;

        private int goldPerClick = 1;
        private RectTransform rectTransformCurrent;

        public override void Awake()
        {
            base.Awake();

            ItemButton.onClick.AddListener(AddOnClickObject);
            ItemButton.onClick.AddListener(
                () => PlayerDataBridge.Get().GainGold(goldPerClick)
                );

            rectTransformCurrent = GetComponent<RectTransform>();
        }

        public void AddOnClickObject()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransformCurrent,
                Input.mousePosition,
                null,
                out Vector2 localpoint
                );

            GameObject tempObject = Instantiate(ClickElement);

            RectTransform tempRectTransform = tempObject.GetComponent<RectTransform>();

            if (tempRectTransform != null)
            {
                tempRectTransform.SetParent(transform, false);
                tempRectTransform.anchoredPosition = localpoint;
            }
                
            tempObject.GetComponent<OnClickElement>().Initialize("+" + goldPerClick);
        }
    }
}