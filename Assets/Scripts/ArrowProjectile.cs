using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ArrowProjectile : MonoBehaviour
{

    [SerializeField] private GameObject gfx;
    [ReadOnly] public float force;
    [ReadOnly] public float startingForce;
    public Vector3 direction;
    [ReadOnly, SerializeField] private Vector3 velocity;
    public Rigidbody rb;
    [SerializeField]  public TrailRenderer[] m_trails;
    [ReadOnly] public int appliedDamage;
    private float[] trailTimes;
    [SerializeField] private float gravityScale;
    [SerializeField, ReadOnly] public Vector3 rayHitPoint;
    [SerializeField] private LightingBolt lightingBolt;
    [SerializeField] private bool doStun;
    [SerializeField] private float stunDuration;
    [SerializeField] private float timeToLive;
    Vector3 boxCastPosition => transform.position;
    [SerializeField] Vector3 colliderBounds;

    Ray ray;
    bool rayHit;
    float timeAlive;
    public LayerMask rayMask;
    public int maxDamageBody;
    public int minDamageBody;
    public int StackOnBodyHit;
    public int StackOnHeadHit;
    public UnityEvent onHitBody;
    public UnityEvent onHitHead;
    public UnityEvent onHitAnything;
    Livebody currentLivebody;

    private void OnEnable()
    {
        
        velocity = direction * force;
        rb.AddForce(velocity, ForceMode.Impulse);
        transform.up = velocity;
        timeAlive = 0;
      

        if (m_trails.Length > 0)
        {
            trailTimes = new float[m_trails.Length];
            for (int i = 0; i < m_trails.Length; i++)
            {
                trailTimes[i] = m_trails[i].time;
            }
            StartCoroutine(ActivateTrail());
        }
        rayHit = false;
       
    }
    private void Update()
    {
        CheckCollisionWithRay();
        timeAlive += Time.deltaTime;
        if(timeAlive > timeToLive)
        {
            gameObject.SetActive(false);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    onHitAnything?.Invoke();
    //    SoundManager.Play(SoundManager.Sound.BowHit, other.ClosestPointOnBounds(PlayerController.playerTransform.position));
    //    currentLivebody = other.gameObject.GetComponentInParent<Livebody>();
    //    if (currentLivebody == null)
    //    {
    //        this.gameObject.SetActive(false);
    //        VFXManager.Play(VFXManager.Effect.TerrainHitEffect, other.ClosestPointOnBounds(PlayerController.playerTransform.position));
    //        return;
    //    }
    //    if (lightingBolt != null)
    //    {
    //        lightingBolt.OnActivate(currentLivebody);
    //    }
    //    else
    //    {
    //        if (doStun)
    //        {
    //            currentLivebody.m_stateHandler.SwapToStunState();
    //        }
    //        if (other.gameObject.CompareTag("Head"))
    //        {
    //            // OnLivebodyHeadshot?.Invoke();
    //            currentLivebody.TakeDamage(appliedDamage + 5);
    //            VFXManager.Play(VFXManager.Effect.HeadshotEffect, other.ClosestPointOnBounds(PlayerController.playerTransform.position));
    //            HitMarkHandler.instance.PlayHeadShotHitMark();
    //            AbilityStackHandler.instance.IncreaseBufferValue(StackOnHeadHit);
    //            onHitHead?.Invoke();


    //        }
    //        else
    //        {
    //            currentLivebody.TakeDamage(appliedDamage);
    //            VFXManager.Play(VFXManager.Effect.EnemyHit, other.ClosestPointOnBounds(PlayerController.playerTransform.position));
    //            HitMarkHandler.instance.PlayNormalHitMark();
    //            AbilityStackHandler.instance.IncreaseBufferValue(StackOnBodyHit);
    //            onHitBody?.Invoke();
    //        }
    //        this.gameObject.SetActive(false);
    //    }
    //}

    private void CheckCollisionWithRay()
    {
        if (rayHit)
            return;

        ray = new Ray(transform.position, direction);

        if (Physics.BoxCast(boxCastPosition, colliderBounds, ray.direction, out RaycastHit hit, transform.rotation, colliderBounds.magnitude, rayMask))
        {

            rayHitPoint = hit.point;
            currentLivebody = hit.collider.gameObject.GetComponentInParent<Livebody>();
            onHitAnything?.Invoke();
            SoundManager.Play(SoundManager.Sound.BowHit, hit.point);
          
            if (currentLivebody == null)
            {
                this.gameObject.SetActive(false);
                VFXManager.Play(VFXManager.Effect.TerrainHitEffect, rayHitPoint);
                rayHit = true;
                return;
            }
            if (lightingBolt != null)
            {
                lightingBolt.OnActivate(currentLivebody);
            }
            else
            {
                if (doStun)
                {
                    currentLivebody.m_stateHandler.SwapToStunState();
                }
                if (hit.collider.gameObject.CompareTag("Head"))
                {
                    // OnLivebodyHeadshot?.Invoke();
                    currentLivebody.TakeDamage(appliedDamage);
                    VFXManager.Play(VFXManager.Effect.HeadshotEffect, rayHitPoint);
                    HitMarkHandler.instance.PlayHeadShotHitMark();
                    AbilityStackHandler.instance.IncreaseBufferValue(StackOnHeadHit);
                    onHitHead?.Invoke();


                }
               

            }
            rayHit = true;
         
            this.gameObject.SetActive(false);
        }



    }
    private void FixedUpdate()
    {
        
        if (direction != Vector3.zero && timeAlive > 0.05f /*&& !rayHit*/)
        {
            rb.velocity += Vector3.down * gravityScale;
            
            transform.up = rb.velocity;
        }
        
    }
    IEnumerator ActivateTrail()
    {
        for (int i = 0; i < m_trails.Length; i++)
        {
            if (m_trails[i] == null)
            {
                yield break;
            }
            m_trails[i].enabled = false;
            yield return new WaitForSeconds(0.01f);
            m_trails[i].enabled = true;
            m_trails[i].time = 0;
            m_trails[i].Clear();
            m_trails[i].time = trailTimes[i];
        }
    }
    private void OnDisable()
    {
        force = 0;
        direction = Vector3.zero;
        velocity = Vector3.zero;
        rb.velocity = Vector3.zero;
       
        foreach (TrailRenderer trail in m_trails)
        {
            if (trail != null)
            {
                trail.Clear();
            }
        }
    }
  
}