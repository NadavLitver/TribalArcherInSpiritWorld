using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyProjectileCollider : MonoBehaviour
{
    [FoldoutGroup("Events")]
    public UnityEvent OnPlayerHit;
    [SerializeField, FoldoutGroup("Properties")]
    int damage;
    private void OnTriggerEnter(Collider other)
    {
       
        Livebody currentLivebody = other.GetComponent<Livebody>() ?? other.GetComponentInParent<Livebody>() ?? other.GetComponentInChildren<Livebody>();
        //  Quaternion effectRotation = (other.ClosestPointOnBounds(transform.position) - PlayerController.playerTransform);
        Vector3 hitPoint = other.ClosestPointOnBounds(transform.position);
        if (currentLivebody == null)
        {
            VFXManager.Play(VFXManager.Effect.TerrainHitEffect, hitPoint);
            SoundManager.Play(SoundManager.Sound.OwlProjectileHit, transform.position, 0.55f);
            transform.parent.gameObject.SetActive(false);
            return;
        }
        else
        {
            LiveBodyHit(currentLivebody, hitPoint);
          
        }
       
       
    }
    private void LiveBodyHit(Livebody currentLivebody,Vector3 hitPoint)
    {
        if (currentLivebody.gameObject.CompareTag("Player"))
            OnPlayerHit?.Invoke();

        currentLivebody.TakeDamage(damage);
        VFXManager.Play(VFXManager.Effect.HeadshotEffect, hitPoint);
        transform.parent.gameObject.SetActive(false);
        SoundManager.Play(SoundManager.Sound.OwlProjectileHit,transform.position, 0.75f);

    }
}