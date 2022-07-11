using Sirenix.OdinInspector;
using System.Collections.Generic;
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
        InvokeRepeating("Spawn", 2, 1);
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

    void Spawn()
    {
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            if (Vector2.Distance(enemyPositions[i], PlayerController.playerTransform.position) < distanceToActivate)
            {
                enemies[i].gameObject.SetActive(true);
            }
        }

    }
}
