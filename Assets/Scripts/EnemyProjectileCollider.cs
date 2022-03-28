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
        if (currentLivebody == null)
        {
            this.gameObject.SetActive(false);
            VFXManager.Play(VFXManager.Effect.TerrainHitEffect, other.ClosestPointOnBounds(transform.position));
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHit(currentLivebody, other.ClosestPointOnBounds(transform.position));
        }
       
    }
    private void PlayerHit(Livebody currentLivebody,Vector3 hitPoint)
    {
        OnPlayerHit?.Invoke();
        currentLivebody.TakeDamage(damage);
        VFXManager.Play(VFXManager.Effect.HeadshotEffect, hitPoint);
        this.gameObject.SetActive(false);


    }
}