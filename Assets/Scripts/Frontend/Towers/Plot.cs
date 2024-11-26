using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Frontend.Level;
using TowerDefense.Scripts.Frontend.UIElements;
using UnityEngine;


namespace TowerDefense.Scripts.Frontend.Towers
{
    public class Plot : MonoBehaviour
    {
        public void Update()
        {
            if (MapManager.Get().IsPaused)
                return;

            if (Input.touchCount>0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    CheckTouch(touch.position);
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                CheckTouch(Input.mousePosition);
            }
        }

        private void CheckTouch(Vector2 screenPosition)
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // Perform a physics check
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if(!Shop.Get().IsOpen)
                    HandleClick();
            }
        }

        public void HandleClick()
        {
            Shop.Get().SetPlot(this);

            Shop.Get().ToggleShop(true);
        }
    }
}