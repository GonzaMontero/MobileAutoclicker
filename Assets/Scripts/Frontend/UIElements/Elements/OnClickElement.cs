using Autoclicker.Scripts.Utils.Managers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Autoclicker.Scripts.Frontend.UIElements.Elements
{
    public class OnClickElement : MonoBehaviour
    {
        public Transform ThisTransform;
        public TextMeshProUGUI ThisText;

        public float MoveUpAmount = 2.0f;
        public float Duration = 1.0f;

        private Vector3 startPos;
        private Vector3 endPos;

        private float startTime;
        private float endTime;

        public void Initialize(string text)
        {
            SetData(text);

            startPos = ThisTransform.position;
            endPos = startPos + Vector3.up * MoveUpAmount;

            startTime = TimeManager.Get().GetAccessDeltaTime();
            endTime = startTime + Duration;

            TimeManager.Get().OnUpdateTick += UpdatePosition;
        }

        public void SetData(string text)
        {
            ThisText.text = text;
        }

        public void UpdatePosition()
        {
            if (TimeManager.Get().GetAccessDeltaTime() < endTime)
            {
                float t = (TimeManager.Get().GetAccessDeltaTime() - startTime) / Duration;

                ThisTransform.position = Vector3.Lerp(startPos, endPos, t);
                
                return;
            }

            ThisTransform.position = startPos;

            Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            TimeManager.Get().OnUpdateTick -= UpdatePosition;
        }
    }
}