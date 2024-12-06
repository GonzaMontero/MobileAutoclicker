using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Scripts.Frontend.UIElements
{
    public class PluginFunctions : MonoBehaviour
    {
        private string logs;

        public void ReadLogs()
        {
            PluginManager.Get().GenerateLogs();
        }

        public void ClearLogs()
        {
            PluginManager.Get().ClearLogs();
        }

        public void SendLog()
        {
            PluginManager.Get().RegisterTimeLog();
        }
    }
}