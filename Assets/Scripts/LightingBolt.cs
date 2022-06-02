using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBolt : MonoBehaviour
{
    public GameObject ps_LightingBolt;
    public GameObject ps_LightingExplosion;

    public float AOE_Size;
    public int explosionDamage;
    public LayerMask enemyMask;
    public void OnActivate(Livebody livebody)
    {
        GameObject bolt = Instantiate(ps_LightingBolt);
        GameObject explosion = Instantiate(ps_LightingExplosion);
        bolt.SetActive(false);
        Vector3 lbPos = livebody.transform.position;
        bolt.transform.parent = null;
        explosion.transform.parent = null;
        bolt.transform.position = new Vector3(lbPos.x, lbPos.y + 4, lbPos.z);
        bolt.SetActive(true);
        explosion.transform.position = lbPos;
        explosion.SetActive(true);

        RaycastHit[] enemiesHit = Physics.BoxCastAll(lbPos, Vector3.one * AOE_Size, Vector3.up,livebody.transform.rotation, 0.1f, enemyMask);
        int enemiesDamaged = 0;
        for (int i = 0; i < enemiesHit.Length; i++)
        {
            Livebody currentLivebody = enemiesHit[i].collider.gameObject.GetComponentInParent<Livebody>();
           
            GameObject CurExplosion = Instantiate(ps_LightingExplosion);
            CurExplosion.transform.localScale = Vector3.one;
            CurExplosion.transform.position = enemiesHit[i].collider.transform.position;
            if (currentLivebody != null)
            {
                currentLivebody.m_stateHandler.SwapToStunState();
                currentLivebody.TakeDamage(explosionDamage);
                enemiesDamaged++;
            }
        }
        Debug.Log(enemiesDamaged);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * AOE_Size);
    }
}
