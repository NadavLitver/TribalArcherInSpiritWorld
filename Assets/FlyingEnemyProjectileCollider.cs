using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyingEnemyProjectileCollider : MonoBehaviour
{
    [FoldoutGroup("Events")]
    public UnityEvent OnPlayerHit;
    [SerializeField, FoldoutGroup("Properties")]
    int damage;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Arrow Hit" + other.gameObject.name);
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
            PlayerHit(currentLivebody);
        }
    }
    private void PlayerHit(Livebody currentLivebody)
    {
        OnPlayerHit?.Invoke();
        Debug.Log(" Player Hit For " + " " + damage);
        currentLivebody.TakeDamage(damage);

    }
}