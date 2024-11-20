using System.Collections;
using System.Collections.Generic;
using TowerDefense.Scripts.Utils;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.Level
{
    public class MapManager : MonoBehaviourSingletonInScene<MapManager>
    {
        [Header("References")]
        public Transform StartPoint;
        public Transform[] PathNodes;
        public Transform EndPoint;
    }
}