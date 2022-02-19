using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private ObjectPool enemyPool;
    [SerializeField]
    private float summonRate;
    [SerializeField]
    private bool DoSummon;
    [SerializeField]
    private Transform SpawnPoint;
    [SerializeField,ReadOnly]
    private List<GameObject> enemiesSpawned;
    [SerializeField]
    private int maxEnemiesForSpawnPoint = 10;
    private void Start()
    {

        StartCoroutine(SummonEnemy());
    }
    IEnumerator SummonEnemy()
    {
        while (DoSummon )
        {
            while(enemiesSpawned.Count < maxEnemiesForSpawnPoint)
            {
                yield return new WaitForSeconds(summonRate);
                GameObject clone = enemyPool.GetPooledObject();
                enemiesSpawned.Add(clone);
                clone.transform.position = SpawnPoint.position;
                clone.SetActive(true);
            }
            yield return new WaitForSeconds(5);
            for (int i = 0; i < enemiesSpawned.Count; i++)
            {
                if (!enemiesSpawned[i].activeInHierarchy)
                {
                    enemiesSpawned.Remove(enemiesSpawned[i]);

                }
            }
        }
      

    }
}
