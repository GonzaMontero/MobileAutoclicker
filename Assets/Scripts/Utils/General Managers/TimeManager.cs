using System;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefense.Scripts.Utils.Managers
{
    public class TimeManager : MonoBehaviourSingleton<TimeManager>
    {
        public event Action OnUpdateTick;
        public event Action OnSecondTick;

        private float _timer;
        private float _accessDeltaTime;

        private void Update()
        {
            _timer += Time.deltaTime;
            _accessDeltaTime += Time.deltaTime;

            OnUpdateTick?.Invoke();

            if(_timer >= 1.0f)
            {
                _timer = 0.0f;
                OnSecondTick?.Invoke();
            }
        }

        public float GetAccessDeltaTime()
        {
            return _accessDeltaTime;
        }
    }
}