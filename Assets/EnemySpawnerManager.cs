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
    private void Awake()
    {
        enemies.Clear();
        enemyPositions.Clear();
        instance = this;
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
    public void AddMe(Livebody bodyToRemove)
    {
        enemyPositions.Add(bodyToRemove.transform.position);
        enemies.Add(bodyToRemove);
        bodyToRemove.gameObject.SetActive(false);

    }

    IEnumerator Spawn()
    {
        while (this.enabled)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < enemyPositions.Count; i++)
            {
                if (Vector2.Distance(enemyPositions[i], PlayerController.playerTransform.position) < distanceToActivate)
                {
                    VFXManager.Play(VFXManager.Effect.SpawnEffect, enemies[i].transform.position);
                    yield return new WaitForSeconds(1f);
                    enemies[i].gameObject.SetActive(true);
                    RemoveMe(enemies[i]);
                }
            }
        }
      

    }
    IEnumerator SpawnRoutine(Livebody ObjectToSetActive)
    {
        VFXManager.Play(VFXManager.Effect.SpawnEffect, ObjectToSetActive.transform.position);
        yield return new WaitForSeconds(2f);
        ObjectToSetActive.gameObject.SetActive(true);
        
    }
}
