using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Autoclicker.Scripts.Utils.Managers
{
    public class TimeManager : MonoBehaviourSingleton<TimeManager>
    {
        public event Action OnSecondTick; 

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if(_timer >= 1.0f)
            {
                _timer = 0.0f;
                OnSecondTick?.Invoke();
            }
        }
    }
}