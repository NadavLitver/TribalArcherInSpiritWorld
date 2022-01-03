using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections;

public class ArrowProjectile : MonoBehaviour
{
    public float force;
    public float startingForce;
    public Vector3 direction;
    [ReadOnly,SerializeField]
    private Vector3 velocity;
    public Rigidbody rb;
    [SerializeField]
    public  TrailRenderer[] m_trails;
    public int maxDamage;
    [ReadOnly]
    public int appliedDamage;
    private float[] trailTimes;


    private void OnEnable()
    {
        trailTimes = new float[m_trails.Length];
        for (int i = 0; i < m_trails.Length; i++)
        {
            trailTimes[i] = m_trails[i].time;
        }
        velocity = direction * force;
        rb.AddForce(velocity, ForceMode.Force);
       
            StartCoroutine(ActivateTrail());
    }
    private void Update()
    {
        if (direction != Vector3.zero)
            transform.up = rb.velocity;           



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
            yield return new WaitForSeconds(0.1f);
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
