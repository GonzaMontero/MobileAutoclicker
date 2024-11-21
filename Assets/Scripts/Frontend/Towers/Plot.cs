using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Frontend.Level;
using UnityEngine;


namespace TowerDefense.Scripts.Frontend.Towers
{
    public class Plot : MonoBehaviour
    {
        private GameObject tower;

        private void OnMouseDown()
        {
            if (tower != null)
                return;

            GameObject towerToBuild = BuildManager.Get().GetSelectedTower();

            tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
        }
    }
}