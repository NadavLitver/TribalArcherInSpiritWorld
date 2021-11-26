using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileCollider : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Properties")]
    int damage;
    public UnityEvent OnPlayerHit;
    public UnityEvent OnHitLiveBody;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Arrow Hit" + other.gameObject.name);
        Livebody currentLivebody = other.GetComponent<Livebody>() ?? other.GetComponentInParent<Livebody>() ?? other.GetComponentInChildren<Livebody>();
      //  Quaternion effectRotation = (other.ClosestPointOnBounds(transform.position) - PlayerController.playerTransform);
        VFXManager.Play(VFXManager.Effect.TestEffect, other.ClosestPointOnBounds(transform.position));
        if (currentLivebody == null)
        {
            this.gameObject.SetActive(false);
            return;
        }


        if (currentLivebody.transform.CompareTag("Player"))
        {
            PlayerHit(currentLivebody);
        }
        else
        {
            if (currentLivebody.transform.CompareTag("Head"))
            {
             currentLivebody.TakeDamage((damage * 2));

            }
            else
            {
             currentLivebody.TakeDamage(damage);

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
