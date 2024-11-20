using UnityEngine.Events;
using TowerDefense.Scripts.Utils;

namespace TowerDefense.Scripts.Backend
{
    public class CommandManager : MonoBehaviourSingleton<CommandManager>
    {
        public UnityEvent OnSettingsChanged;

        public override void Awake()
        {
            base.Awake();

            if (OnSettingsChanged == null)
                OnSettingsChanged = new UnityEvent();
        }
    }
}