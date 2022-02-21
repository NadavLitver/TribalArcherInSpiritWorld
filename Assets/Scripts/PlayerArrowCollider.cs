using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class PlayerArrowCollider : MonoBehaviour
{
    [FoldoutGroup("Events")]
    public UnityEvent OnPlayerHit;
    [FoldoutGroup("Events")]
    public UnityEvent OnHitLiveBody;
    [FoldoutGroup("Events")]
    public UnityEvent OnLivebodyHeadshot;
    [FoldoutGroup("Refrences")]
    public ArrowProjectile ArrowProj;
    public int StackOnBodyHit;
    public int StackOnHeadHit;
    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Arrow Hit" + other.gameObject.name);
        Livebody currentLivebody = other.GetComponent<Livebody>() ?? other.GetComponentInParent<Livebody>() ?? other.GetComponentInChildren<Livebody>();
        //  Quaternion effectRotation = (other.ClosestPointOnBounds(transform.position) - PlayerController.playerTransform);
        if (currentLivebody == null)
        {
            this.gameObject.SetActive(false);
            VFXManager.Play(VFXManager.Effect.TerrainHitEffect,other.ClosestPointOnBounds(ArrowProj.rayHitPoint));
            return;
        }


        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        {
            if (other.gameObject.CompareTag("Head"))
            {
                OnLivebodyHeadshot?.Invoke();
                currentLivebody.TakeDamage(ArrowProj.appliedDamage + 5);
                VFXManager.Play(VFXManager.Effect.HeadshotEffect, other.ClosestPointOnBounds(ArrowProj.rayHitPoint));
                HitMarkHandler.instance.PlayHeadShotHitMark();
                AbilityStackHandler.instance.IncreaseBufferValue(StackOnHeadHit);



            }
            else
            {
                currentLivebody.TakeDamage(ArrowProj.appliedDamage);
                VFXManager.Play(VFXManager.Effect.EnemyHit, other.ClosestPointOnBounds(ArrowProj.rayHitPoint));
                HitMarkHandler.instance.PlayNormalHitMark();
                AbilityStackHandler.instance.IncreaseBufferValue(StackOnBodyHit);

            }

            OnHitLiveBody?.Invoke();
        }
        this.gameObject.SetActive(false);
    }

}
