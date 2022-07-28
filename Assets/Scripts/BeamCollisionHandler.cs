using Sirenix.OdinInspector;
using UnityEngine;
public class BeamCollisionHandler : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Properties")]
    int damage;
    [SerializeField, ReadOnly, FoldoutGroup("Properties")]
    bool IsOnTarget;
    Livebody currentLivebody;
    float timeOnPlayer;
    float timeEnabled;
    [SerializeField, FoldoutGroup("Refrences")] Collider m_collider;
    private void OnEnable()
    {
        m_collider = GetComponent<Collider>();
        m_collider.enabled = false;
        timeEnabled = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        currentLivebody = other.GetComponent<Livebody>() ?? other.GetComponentInParent<Livebody>() ?? other.GetComponentInChildren<Livebody>();
        //  Quaternion effectRotation = (other.ClosestPointOnBounds(transform.position) - PlayerController.playerTransform);
        if (currentLivebody == null)
        {
            VFXManager.Play(VFXManager.Effect.TerrainHitEffect, other.ClosestPointOnBounds(transform.position));
            return;
        }
        else
        {
            HitLiveBody(currentLivebody, other.ClosestPointOnBounds(transform.position));

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        IsOnTarget = false;
    }
    private void HitLiveBody(Livebody currentLivebody, Vector3 hitPoint)
    {
        currentLivebody.TakeDamage(damage);
        IsOnTarget = true;
    }
    private void Update()
    {
        
        if (IsOnTarget)
        {
            timeOnPlayer += Time.deltaTime;
            if(timeOnPlayer > 0.25f)
            {
                if(currentLivebody!= null)
                  currentLivebody.TakeDamage(damage);

                timeOnPlayer = 0;
            }
        }
        else if (!m_collider.enabled)
        {
            timeEnabled += Time.deltaTime;
            if (timeEnabled > 1)
                m_collider.enabled = true;

        }
    }
}
