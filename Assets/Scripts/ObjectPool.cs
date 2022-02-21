using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    private void Awake()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject GO = Instantiate(objectToPool);
            pooledObjects.Add(GO);
            GO.SetActive(false);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                
                return pooledObjects[i];
            }
        }
        GameObject GO = Instantiate(objectToPool, transform);
        GO.SetActive(false);
        pooledObjects.Add(GO);
        return GO;
    }//CALL THIS LIKE THIS
     //GameObject bullet = ObjectPooler.GetPooledObject(); 


     //if (bullet != null) {
     //  bullet.transform.position = turret.transform.position;
     //  bullet.transform.rotation = turret.transform.rotation;
     //  bullet.SetActive(true);
     //}
}
