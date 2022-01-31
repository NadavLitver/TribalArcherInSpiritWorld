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
    protected virtual void OnTriggerEnter(Collider other)
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
            return;
        }
        else
        {
            if (other.gameObject.CompareTag("Head"))
            {
                OnLivebodyHeadshot?.Invoke();
                currentLivebody.TakeDamage(ArrowProj.appliedDamage * 2);
                VFXManager.Play(VFXManager.Effect.HeadshotEffect, other.ClosestPointOnBounds(transform.position));
                HitMarkHandler.instance.PlayHeadShotHitMark();
                AbilityStackHandler.instance.IncreaseBufferValue(90);



            }
            else
            {
                currentLivebody.TakeDamage(ArrowProj.appliedDamage);
                VFXManager.Play(VFXManager.Effect.EnemyHit, other.ClosestPointOnBounds(transform.position));
                HitMarkHandler.instance.PlayNormalHitMark();
                AbilityStackHandler.instance.IncreaseBufferValue(50);

            }

            OnHitLiveBody?.Invoke();
        }
        this.gameObject.SetActive(false);
    }

}
