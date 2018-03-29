using UnityEngine;
using System.Collections.Generic;

namespace GalleryTest.Misc
{
    public class ObjectPool : MonoBehaviour
    {
        private Stack<GameObject> inactiveInstances = new Stack<GameObject>();
        private GameObject prefab;

        /// <summary>
        /// Initializes the Object Pool
        /// </summary>
        /// <param name="prefab">Prefab type for the object pool</param>
        public void Setup(GameObject prefab)
        {
            this.prefab = prefab;
        }

        /// <summary>
        /// It will also set the new object's parent if stated in the arguments
        /// </summary>
        /// <param name="parent">Parent to set for the new object</param>
        /// <returns>Newly created or borrowed object</returns>
        public GameObject BorrowObject(Transform parent = null)
        {
            GameObject spawnedGameObject;

            if (inactiveInstances.Count > 0)
            {
                spawnedGameObject = inactiveInstances.Pop();
            }
            else
            {
                spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);
                PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
                pooledObject.pool = this;
            }

            spawnedGameObject.transform.SetParent(parent);
            spawnedGameObject.SetActive(true);

            return spawnedGameObject;
        }

        public void ReturnObject(GameObject toReturn)
        {
            PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

            if (pooledObject != null && pooledObject.pool == this)
            {
                toReturn.transform.SetParent(transform);
                toReturn.SetActive(false);

                inactiveInstances.Push(toReturn);
            }
            else
            {
                Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
                Destroy(toReturn);
            }
        }
    }

    public class PooledObject : MonoBehaviour
    {
        public ObjectPool pool;
    }
}