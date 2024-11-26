using System.Collections;
using System.Collections.Generic;
using TMPro;
using TowerDefense.Scripts.Frontend.Level;
using TowerDefense.Scripts.Frontend.Towers;
using TowerDefense.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class Shop : MonoBehaviourSingletonInScene<Shop>
    {
        [Header("References")]
        public TextMeshProUGUI CurrencyUI;
        public BaseBuildingButton BuildingButtonPrototype;
        public Transform BuildingButtonParent;


        public bool IsOpen = false;
        private Plot currentPlot;

        private void Start()
        {
            GenerateNewButtons();

            ToggleShop(false);

            CurrencyUI.text = MapManager.Get().Currency.ToString();
        }

        public void ToggleShop(bool toggle)
        {
            gameObject.SetActive(toggle);
            IsOpen = toggle;
        }

        public void SetPlot(Plot plot)
        {
            this.currentPlot = plot;
        }

        public void UpdateCurrency(int newCurrency)
        {
            CurrencyUI.text = newCurrency.ToString();
        }

        public void GenerateNewButtons()
        {
            for (short i = 0; i < BuildManager.Get().TowerPrefabs.Length; i++)
            {
               BaseBuildingButton temp = Instantiate(BuildingButtonPrototype, BuildingButtonParent).GetComponent<BaseBuildingButton>();
                temp.SetData(BuildManager.Get().TowerPrefabs[i].Name, BuildManager.Get().TowerPrefabs[i].ShopImage,
                    BuildManager.Get().TowerPrefabs[i].TowerID, BuildManager.Get().TowerPrefabs[i].Cost);

                int id = BuildManager.Get().TowerPrefabs[i].TowerID;

                temp.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => CreateTower(id) );
            }

            BuildingButtonPrototype.gameObject.SetActive(false);
        }

        private void CreateTower(int id)
        {            
            BuildManager.Get().SetSelectedTower(id);

            GameObject towerToBuild = BuildManager.Get().GetSelectedTower().Prefab;

            if(!MathUtils.CanSpend(MapManager.Get().Currency, BuildManager.Get().GetSelectedTower().Cost, out int newCurrency))
            {
                return;
            }

            MapManager.Get().UpdateCurrency(newCurrency);

            Instantiate(towerToBuild, currentPlot.transform.position, Quaternion.identity, BuildManager.Get().DefaultParent);

            currentPlot.gameObject.SetActive(false);

            ToggleShop(false);
        }
    }
}