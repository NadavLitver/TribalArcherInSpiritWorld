using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    public static EnemySpawnerManager instance;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] List<Livebody> enemies;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] List<Vector3> enemyPositions;
    [SerializeField, FoldoutGroup("Refrences")] private int distanceToActivate;
    public bool spawn;
    [SerializeField, FoldoutGroup("Refrences")] internal TempleBody templeBody;
    internal List<Livebody> endingSceneLiveBodies;
    private void Awake()
    {
        enemies.Clear();
        enemyPositions.Clear();
        instance = this;
        endingSceneLiveBodies = new List<Livebody>();


    }
    void Start()
    {
        instance = this;
        // InvokeRepeating("Spawn", 2, 1);
        StartCoroutine(Spawn());
    }

    public void RemoveMe(Livebody bodyToRemove)
    {
        if (enemies.Contains(bodyToRemove))
        {
            enemyPositions.RemoveAt(enemies.IndexOf(bodyToRemove));
            enemies.Remove(bodyToRemove);
        }
       

    }
    public Transform getPointOnTempleToAttack()
    {
       return templeBody.GetRandomTransform();
    }
    public void ToggleRandomActiveEnemy()
    {
        int randomNum = Random.Range(0, 10);
        if(randomNum  > 4)
        {
            return;
        }
        for (int i = 0; i < endingSceneLiveBodies.Count; i++)
        {
             int ranNum = Random.Range(0, endingSceneLiveBodies.Count);
            if (endingSceneLiveBodies[ranNum].gameObject.activeInHierarchy)
            {
                endingSceneLiveBodies[ranNum].m_stateHandler.ToggleAllStatesTarget();
                break;
            }

        }
    }
    public void AddMe(Livebody bodyToRemove)
    {
        
        enemyPositions.Add(bodyToRemove.transform.position);
        enemies.Add(bodyToRemove);
        bodyToRemove.gameObject.SetActive(false);

    }
    [Button]
    public void DeActivateBaseEnemies()
    {
        StopAllCoroutines();
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
          
        }
        enemies.Clear();
        enemyPositions.Clear();
    }
    IEnumerator Spawn()
    {
        while (this.enabled && spawn)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                if (Vector2.Distance(enemyPositions[i], PlayerController.playerTransform.position) < distanceToActivate)
                {
                    VFXManager.Play(VFXManager.Effect.SpawnEffect, enemies[i].transform.position, enemies[i].transform.rotation);
                    yield return new WaitForSeconds(1f);
                    enemies[i].gameObject.SetActive(true);
                    RemoveMe(enemies[i]);
                }
            }
        }
      

    }
    IEnumerator SpawnRoutine(Livebody ObjectToSetActive)
    {
        VFXManager.Play(VFXManager.Effect.SpawnEffect, ObjectToSetActive.transform.position, ObjectToSetActive.transform.rotation);
        yield return new WaitForSeconds(2f);

        ObjectToSetActive.gameObject.SetActive(true);
        
    }
}
