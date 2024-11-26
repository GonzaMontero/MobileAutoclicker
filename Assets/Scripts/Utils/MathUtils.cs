using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Frontend.UIElements;
using UnityEngine;

namespace TowerDefense.Scripts.Utils
{
    public class MathUtils
    {
        /// <summary>
        /// Recieves a current currency amount and returns true if the cost can be paid successfully 
        /// </summary>
        /// <returns>Wether the currency can be spent successfully</returns>
        public static bool CanSpend(int currentCurrency, int cost, out int newCurrency)
        {
            if (currentCurrency < cost)
            {
                Debug.Log("No money (poor ahh)!");
                newCurrency = currentCurrency;
                return false;
            }
            else
            {
                newCurrency =  currentCurrency - cost;
                return true;
            }
        }
    }
}