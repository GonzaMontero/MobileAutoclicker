using Autoclicker.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Autoclicker.Scripts.Frontend.UIElements.Elements
{
    [RequireComponent(typeof(Button))]
    public class ClickerObject : MonoBehaviourSingletonInScene<ClickerObject>
    {
        [Header("References")]
        public GameObject ClickElement;
        public InputActionReference ClickActionReference;

        private int goldPerClick = 1;
        private Button thisButton;
        private InputAction clickAction;

        public override void Awake()
        {
            base.Awake();

            clickAction = ClickActionReference.action;
            clickAction.Enable();

            thisButton = GetComponent<Button>();
            thisButton.onClick.AddListener(AddOnClickObject);
        }

        public void AddOnClickObject()
        {
            Vector2 clickPosition = clickAction.ReadValue<Vector2>();

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(clickPosition.x, clickPosition.y, 0));

            GameObject tempObject = Instantiate(ClickElement, worldPosition, Quaternion.identity, transform);

            tempObject.GetComponent<OnClickElement>().Initialize("+" + goldPerClick);
        }
    }
}