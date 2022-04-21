using Sirenix.OdinInspector;
using UnityEngine;
public class BeamCollisionHandler : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Properties")]
    int damage;
    [SerializeField, ReadOnly, FoldoutGroup("Properties")]
    bool isOnPlayer;
    Livebody currentLivebody;
    float timeOnPlayer;
    private void OnTriggerEnter(Collider other)
    {
        currentLivebody = other.GetComponent<Livebody>() ?? other.GetComponentInParent<Livebody>() ?? other.GetComponentInChildren<Livebody>();
        //  Quaternion effectRotation = (other.ClosestPointOnBounds(transform.position) - PlayerController.playerTransform);
        if (currentLivebody == null)
        {
            VFXManager.Play(VFXManager.Effect.TerrainHitEffect, other.ClosestPointOnBounds(transform.position));
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHit(currentLivebody, other.ClosestPointOnBounds(transform.position));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOnPlayer = false;
    }
    private void PlayerHit(Livebody currentLivebody, Vector3 hitPoint)
    {
        currentLivebody.TakeDamage(damage);
        isOnPlayer = true;
    }
    private void Update()
    {
        if (isOnPlayer)
        {
            timeOnPlayer += Time.deltaTime;
            if(timeOnPlayer > 0.5f)
            {
                currentLivebody.TakeDamage(damage);
                timeOnPlayer = 0;
            }
        }
        
    }
}
