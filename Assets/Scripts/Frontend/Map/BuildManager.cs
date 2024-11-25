using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Frontend.Towers;
using TowerDefense.Scripts.Utils;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.Level
{
    public class BuildManager : MonoBehaviourSingletonInScene<BuildManager>
    {
        [Header("References")]
        public TowerItem[] TowerPrefabs;
        public Transform DefaultParent;

        private int currentSelectedTower = 0;

        public TowerItem GetSelectedTower()
        {
            return TowerPrefabs[currentSelectedTower];
        }

        public void SetSelectedTower(int selectedTower)
        {
            currentSelectedTower = selectedTower;
        }
    }
}