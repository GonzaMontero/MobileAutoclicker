using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class TutorialFunctions : MonoBehaviour
    {
        public GameObject[] TutorialObjects;
        public int CurrentIndex = 0;

        public void AdvanceTutorialScreen()
        {
            CurrentIndex = (CurrentIndex + 1) % TutorialObjects.Length;
            UpdateActiveObject();
        }

        public void BackTutorialScreen()
        {
            CurrentIndex = (CurrentIndex - 1 + TutorialObjects.Length) % TutorialObjects.Length;
            UpdateActiveObject();
        }

        private void UpdateActiveObject()
        {
            for (short i = 0; i < TutorialObjects.Length; i++)
            {
                TutorialObjects[i].SetActive(i== CurrentIndex);
            }
        }
    }
}