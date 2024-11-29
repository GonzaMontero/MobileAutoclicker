using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefense.Scripts.Utils.Managers
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject ItemToInstanciate;
        public int AmountToInstantiate;
        public int ItemID;
        public Transform DefaultPosition;
        public Transform DefaultParent;

        public bool ContainsID(int id)
        {
            return ItemID == id;
        }
    }

    public class ObjectPooler : MonoBehaviourSingletonInScene<ObjectPooler>
    {
        public ObjectPoolItem[] PoolItems;
        public List<GameObject>[] PoolItemLists;

        private void Start()
        {
            PoolItemLists = new List<GameObject>[PoolItems.Length];

            for (short i = 0; i < PoolItems.Length; i++)
            {
                PoolItemLists[i] = new List<GameObject>();

                for (short j = 0; j < PoolItems[i].AmountToInstantiate; j++)
                {
                    GameObject temp = GameObject.Instantiate(PoolItems[i].ItemToInstanciate, PoolItems[i].DefaultPosition.position,
                        Quaternion.identity, PoolItems[i].DefaultParent);

                    PoolItemLists[i].Add(temp);

                    temp.SetActive(false);
                }
            }
        }

        public bool EnableItem(int itemID)
        {
            for (short i = 0; i < PoolItems.Length; i++)
            {
                if (PoolItems[i].ContainsID(itemID))
                {
                    if (FindAvailableGameobject(PoolItemLists[i], out GameObject gameObject))
                    {
                        gameObject.SetActive(true);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool EnableItem(int itemID, out GameObject obj)
        {
            for (short i = 0; i < PoolItems.Length; i++)
            {
                if (PoolItems[i].ContainsID(itemID))
                {
                    if(FindAvailableGameobject(PoolItemLists[i], out GameObject gameObject))
                    {
                        gameObject.SetActive(true);
                        obj = gameObject;
                        return true;
                    }
                    else
                    {
                        obj = null;
                        return false;
                    }
                }
            }

            obj = null;
            return false;
        }

        public void DisableItem(int itemID, GameObject reference)
        {
            for (short i = 0; i < PoolItems.Length; i++)
            {
                if (PoolItems[i].ContainsID(itemID))
                {
                    reference.SetActive(false);

                    reference.transform.position = PoolItems[i].DefaultPosition.position;

                    return;
                }
            }

#if UNITY_EDITOR
            Debug.LogError("This item is not pooled!");
#endif
        }

        public void DisableAllItems(int itemID)
        {
            for (short i = 0; i < PoolItems.Length; i++)
            {
                if (PoolItems[i].ContainsID(itemID))
                {
                    foreach(GameObject item in PoolItemLists[i])
                    {
                        item.SetActive(false);
                    }
                }
            }
        }

        public bool FindAvailableGameobject(List<GameObject> list, out GameObject gameObject)
        {
            foreach (GameObject go in list)
            {
                if (!go.activeSelf)
                {
                    gameObject = go;

                    return true;
                }
            }

            gameObject = null;
            return false;
        }
    }
}

