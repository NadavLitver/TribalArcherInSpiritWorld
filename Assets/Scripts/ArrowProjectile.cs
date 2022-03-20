using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject gfx;
    [ReadOnly]
    public float force;
    [ReadOnly]
    public float startingForce;
    public Vector3 direction;
    [ReadOnly, SerializeField]
    private Vector3 velocity;
    public Rigidbody rb;
    [SerializeField]
    public TrailRenderer[] m_trails;
    public int maxDamageBody;
    public int minDamageBody;
    [ReadOnly]
    public int appliedDamage;
    private float[] trailTimes;
    [SerializeField]
    private float gravityScale;
    public LayerMask rayMask;
    [SerializeField,ReadOnly]
    public Vector3 rayHitPoint;
    Ray ray;
    bool rayHit;
    float timeAlive;
  
    private void OnEnable()
    {
        
        velocity = direction * force;
        Debug.Log(direction);
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
        if (direction != Vector3.zero && timeAlive > 0.1f)
            transform.up = rb.velocity;
        timeAlive += Time.deltaTime;
    }
    private void SetRayPoint()
    {
        if (rayHit)
            return;

        ray = new Ray(transform.position, transform.up);

        if (Physics.Raycast(ray, out RaycastHit hit, 2f, rayMask, QueryTriggerInteraction.Collide))
        {
            rayHitPoint = hit.point;
            rayHit = true;
             
        }
        else
        {
            rayHitPoint = ray.GetPoint(10);
        }


    }
    private void FixedUpdate()
    {
        rb.velocity += Vector3.down * gravityScale;
        SetRayPoint();
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
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, transform.up * 2);


    }
}