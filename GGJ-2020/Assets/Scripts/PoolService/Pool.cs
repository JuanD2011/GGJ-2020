using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    #region Variables
    //GameObject for pooling
    public GameObject poolGameObject { get; set; }
    //Pool size
    public int poolSize { get; set; } = 1;
    //List for pooling
    private List<GameObject> pool;
    #endregion

    #region Private Methods
    private void Start()
    {
        try { InitializePool(); }
        catch { Debug.LogWarning("No gameObject has been serialized in this instance"); }
    }

    //Initialize pool
    private void InitializePool()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++) ProduceGameObjectForPool();
    }
    //Choose an inactive object from the pool to allocate it
    private GameObject AllocateGameObjectFromPool()
    {
        for (int i = 0; i < pool.Count; i++)
            //In case of an inactive object in the pool
            if (!pool[i].activeInHierarchy)
            {
                //We return the pool object and we eliminate it from the list to use it
                GameObject objectToAllocate = pool[i];
                pool.Remove(pool[i]);
                objectToAllocate.SetActive(true);
                return objectToAllocate;
            }
        return null;
    }
    //Production Function of gameObjects
    private void ProduceGameObjectForPool()
    {
        GameObject cloneObjectPool = Instantiate(poolGameObject);
        cloneObjectPool.SetActive(false);
        pool.Add(cloneObjectPool);
    }
    #endregion

    #region Public Methods
    //Function to get an object from the pool
    public GameObject GetGameObjectFromPool()
    {
        if (pool.Count > 0) return AllocateGameObjectFromPool();
        else if (pool.Count == 0)
        {
            ProduceGameObjectForPool();
            return AllocateGameObjectFromPool();
        }
        return null;
    }
    //Function to return an object to the pool
    public void ReturnGameObjectToList(GameObject _gamObject)
    {
        _gamObject.transform.position = Vector3.zero;
        _gamObject.transform.rotation = Quaternion.identity;
        _gamObject.SetActive(false);
        pool.Add(_gamObject);
    }
    #endregion
}
