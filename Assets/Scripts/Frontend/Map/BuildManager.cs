using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Utils;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.Level
{
    public class BuildManager : MonoBehaviourSingletonInScene<BuildManager>
    {
        [Header("References")]
        public GameObject[] TowerPrefabs;

        private int currentSelectedTower = 0;

        public GameObject GetSelectedTower()
        {
            return TowerPrefabs[currentSelectedTower];
        }
    }
}