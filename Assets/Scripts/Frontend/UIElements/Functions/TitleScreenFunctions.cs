using TowerDefense.Scripts.Utils.Managers;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class TitleScreenFunctions : MonoBehaviour
    {
        public void EnterGame()
        {
            ASyncLoaderManager.Get().InitiateSceneLoad("Main Menu");
        }
    }
}