using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;
    
    [SerializeField] private List<GameObject> _pooledObjects;

    private void OnEnable()
    {
        PopulatePool();
    }
    
    /// <summary>
    /// Fills the object pool according to the amount set
    /// </summary>
    private void PopulatePool()
    {
        for (var i = 0; i < amountToPool; i++)
        {
            SpawnObject();
        }
    }

    /// <summary>
    /// Instantiates an object and adds it to the object pool
    /// </summary>
    /// <returns>pooled object (GameObject)</returns>
    private GameObject SpawnObject()
    {
        var obj = Instantiate(objectToPool, transform);
        obj.SetActive(false);
        _pooledObjects.Add(obj);
        return obj;
    }

    /// <summary>
    /// Gets pooled object from list
    /// </summary>
    /// <returns>pooled object (GameObject)</returns>
    public GameObject GetPooledObject()
    {
        // Cycles through list of game objects and returns one that is not used 
        foreach (var pooledObject in _pooledObjects)
        {
            if (pooledObject.activeSelf == false)
            {
                return pooledObject;
            }
        }
        // If all items in list are used, create more pooled objects
        return SpawnObject();
    }
    
}
