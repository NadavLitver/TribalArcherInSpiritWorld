using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileCollider : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Properties")]
    int damage;
    [FoldoutGroup("Events")]
    public UnityEvent OnPlayerHit;
    [FoldoutGroup("Events")]
    public UnityEvent OnHitLiveBody;
    [FoldoutGroup("Events")]
    public UnityEvent OnLivebodyHeadshot;
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
        else
        {
            if (other.gameObject.CompareTag("Head"))
            {
             OnLivebodyHeadshot?.Invoke();
             currentLivebody.TakeDamage((damage * 2));
                VFXManager.Play(VFXManager.Effect.HeadshotEffect, other.ClosestPointOnBounds(transform.position));
                HitMarkHandler.instance.PlayHeadShotHitMark();


            }
            else
            {
             currentLivebody.TakeDamage(damage);
                VFXManager.Play(VFXManager.Effect.EnemyHit, other.ClosestPointOnBounds(transform.position));
                HitMarkHandler.instance.PlayNormalHitMark();

            }

            OnHitLiveBody?.Invoke();
        }
        this.gameObject.SetActive(false);
    }
    private void PlayerHit(Livebody currentLivebody)
    {
        OnPlayerHit?.Invoke();
        currentLivebody.TakeDamage(damage);

    }
}
