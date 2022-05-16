using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingBolt : MonoBehaviour
{
    public GameObject ps_LightingBolt;
    public GameObject ps_LightingExplosion;
  

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

        RaycastHit[] enemiesHit =   Physics.BoxCastAll(lbPos, Vector3.one * 4, Vector3.up,livebody.transform.rotation, 0.1f, enemyMask);
        for (int i = 0; i < enemiesHit.Length; i++)
        {
            Livebody currentLivebody = enemiesHit[i].collider.gameObject.GetComponent<Livebody>();
            if(currentLivebody != null )
                currentLivebody.TakeDamage(explosionDamage);
        }
        //Debug.Break();
    }
}
