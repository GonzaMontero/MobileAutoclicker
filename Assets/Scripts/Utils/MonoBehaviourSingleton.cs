using UnityEngine;

namespace TowerDefense.Scripts.Utils
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Get()
        {
            return instance;
        }

        public virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public virtual void OnDestroy()
        {
            if (instance != null)
            {
#if UNITY_EDITOR
                Debug.Log(instance.name + " was destroyed successfully");
#endif
            }
        }
    }
}