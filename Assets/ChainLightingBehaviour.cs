using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightingBehaviour : MonoBehaviour
{
    [FoldoutGroup("Properties"),SerializeField]
    private float size;
    [FoldoutGroup("Properties"), SerializeField]
    private int damage;
    [FoldoutGroup("Properties")]
    public LayerMask HitMask;
    [FoldoutGroup("Properties"), SerializeField,Tooltip("if false will choose a random enemie in radius")]
    private bool hitAllEnemies;
    private int amountToHit;

    [Button]
    public void CheckRadiusForEnemies()
    {
        Collider[] enemies = Physics.OverlapBox(transform.position, Vector3.one * size, Quaternion.identity, HitMask, QueryTriggerInteraction.Collide);
        if(enemies.Length > 0)
        {
            if (hitAllEnemies)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    Livebody currentBody = enemies[i].gameObject.GetComponent<Livebody>() ?? enemies[i].gameObject.GetComponentInParent<Livebody>() ?? enemies[i].gameObject.GetComponentInChildren<Livebody>();
                    if(currentBody!= null)
                      currentBody.TakeDamage(damage);
                    Debug.Log("Hit" + " " + enemies.Length + "Amount Of Enemies");
                }
            }
            else
            {
                var index = Random.Range(0, enemies.Length - 1);
                Livebody currentBody = enemies[index].gameObject.GetComponent<Livebody>() ?? enemies[index].gameObject.GetComponentInParent<Livebody>() ?? enemies[index].gameObject.GetComponentInChildren<Livebody>();
                if (currentBody != null)
                    currentBody.TakeDamage(damage);
            }
          
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Vector3.one * size * 2);
    }

}
