using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class ChainLightingBehaviour : MonoBehaviour
{
    [FoldoutGroup("Refrences"), SerializeField]
    GameObject chainLightingVFX;
    [FoldoutGroup("Refrences"), SerializeField]
    GameObject m_explosion;
    [FoldoutGroup("Properties"), SerializeField]
    private float size;
    [FoldoutGroup("Properties"), SerializeField]
    private int damage;
    [FoldoutGroup("Properties")]
    public LayerMask HitMask;
    [FoldoutGroup("Properties"), SerializeField, Tooltip("if false will choose a random enemie in radius")]
    private bool hitMultipleEnemies;
    [FoldoutGroup("Properties"), SerializeField]
    private float timeBetweenChainConnections = 0.25f;
    [FoldoutGroup("Properties"), SerializeField]
    private int amountToHit =5;
  

    [Button]
    public void CheckRadiusForEnemies()
    {
       AbilityStackHandler.instance.StartCoroutine(ChainLightingRoutine());

    }
    IEnumerator ChainLightingRoutine()
    {
        Collider[] enemies = Physics.OverlapBox(transform.position, Vector3.one * size, Quaternion.identity, HitMask, QueryTriggerInteraction.Collide);
        if (enemies.Length > 0)
        {
            if (hitMultipleEnemies)
            {

                GameObject chainLighting = Instantiate(chainLightingVFX);
                int length = enemies.Length > amountToHit ? amountToHit : enemies.Length;
                chainLighting.transform.position = transform.position;
                Debug.Log(length + " enemies hit count from chain lighting");
                for (int i = 0; i < length; i++)
                {


                    Livebody currentBody = enemies[i].gameObject.GetComponent<Livebody>() ?? enemies[i].gameObject.GetComponentInParent<Livebody>() ?? enemies[i].gameObject.GetComponentInChildren<Livebody>();
                    if (currentBody != null)
                    {
                        chainLighting.transform.GetChild(i).position = enemies[i].transform.position;
                        currentBody.TakeDamage(damage);
                        Instantiate(m_explosion, enemies[i].transform.position, Quaternion.identity);
                        Debug.Log(i);

                    }
                    yield return new WaitForSeconds(timeBetweenChainConnections);
                }
                yield return new WaitForSeconds(timeBetweenChainConnections * 2);
                Destroy(chainLighting.gameObject);
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
