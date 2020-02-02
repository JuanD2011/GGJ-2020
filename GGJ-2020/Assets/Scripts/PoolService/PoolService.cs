using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pool infos required to initialize the pool dictionary
/// </summary>
[System.Serializable]
public struct PoolInfo
{
    public string tKey;
    public GameObject objectRef;
    public int initialCount;
    public Pool pool { get; set; }

    public void InitializePool()
    {
        pool.poolGameObject = objectRef;
        pool.poolSize = initialCount;
    }
}

/// <summary>
/// The pool factory takes an object from a desired pool with a tKey reference to give it to an object
/// </summary>
public class PoolService : MonoBehaviour
{
    public static PoolService Instance = null;

    [SerializeField] private PoolInfo[] referencesForPooling = null; 
    private Dictionary<string, Pool> gameObjectPoolsDict = null;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        //Setting up the pools
        for (int i = 0; i < referencesForPooling.Length; i++)
        {
            Pool newPool = gameObject.AddComponent<Pool>();
            referencesForPooling[i].pool = newPool;
            referencesForPooling[i].InitializePool();
        }
        gameObjectPoolsDict = referencesForPooling.ToDictionary(poolInfo => poolInfo.tKey, poolInfo => poolInfo.pool);
    }

    /// <summary>
    /// Get a gameObject with the tKey
    /// </summary>
    /// <param name="tKey"></param>
    /// <returns></returns>
    public GameObject GetGameObjectFromPool(string tKey)
    {
        return gameObjectPoolsDict.ContainsKey(tKey) ? gameObjectPoolsDict[tKey].GetGameObjectFromPool() : null;
    }

    /// <summary>
    /// Return a gameObject with the tKey
    /// </summary>
    /// <param name="objToReturn"></param>
    /// <param name="tKey"></param>
    public void ReturnGameObjectToPools(GameObject objToReturn, string tKey)
    {
        if (gameObjectPoolsDict.ContainsKey(tKey)) gameObjectPoolsDict[tKey].ReturnGameObjectToList(objToReturn);
    }
}
