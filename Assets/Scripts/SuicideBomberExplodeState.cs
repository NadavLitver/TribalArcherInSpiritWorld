using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SuicideBomberExplodeState : State
{
    [FoldoutGroup("Properties"),SerializeField]
    float explosionRadius;
    [FoldoutGroup("Refrences"), SerializeField]
    Transform ExplosionPos;
    [FoldoutGroup("Properties")]
    private Collider[] explosionTargets;
    [FoldoutGroup("Events")]
    public UnityEvent OnPlayerHit;
    [FoldoutGroup("Properties")]
    public int damage;
    protected override void OnStateDisabled()
    {
      
    }

    protected override void OnStateEnabled()
    {
        
         explosionTargets = Physics.OverlapBox(ExplosionPos.position, explosionRadius * Vector3.one, ExplosionPos.rotation, playerLayer);
        if(explosionTargets.Length != 0)
        {
            for (int i = 0; i < explosionTargets.Length; i++)
            {
              Livebody currentLivebody = explosionTargets[i].GetComponent<Livebody>() ?? explosionTargets[i].GetComponentInParent<Livebody>() ?? explosionTargets[i].GetComponentInChildren<Livebody>();
                if(currentLivebody != null && currentLivebody.gameObject.CompareTag("Player"))
                {
                    OnPlayerHit?.Invoke();
                    currentLivebody.TakeDamage(damage);
                    VFXManager.Play(VFXManager.Effect.HeadshotEffect, ExplosionPos.position);
                    SoundManager.Play(SoundManager.Sound.SuicideExplosions, transform.position);
                    transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(ExplosionPos.position, explosionRadius * Vector3.one);
    }
}
