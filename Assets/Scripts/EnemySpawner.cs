using System.Collections;
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
    private void Start()
    {

        StartCoroutine(SummonEnemy());
    }
    IEnumerator SummonEnemy()
    {
        while (DoSummon)
        {
            yield return new WaitForSeconds(summonRate);
            GameObject clone = enemyPool.GetPooledObject();
            clone.transform.position = SpawnPoint.position;
            clone.SetActive(true);
        }
    }
}
