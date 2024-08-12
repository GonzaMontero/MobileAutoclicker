using UnityEngine.Events;
using Autoclicker.Scripts.Utils;

namespace Autoclicker.Scripts.Backend
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