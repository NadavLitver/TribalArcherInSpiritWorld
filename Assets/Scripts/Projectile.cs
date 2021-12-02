using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float force;
    public float startingForce;
    public Vector3 direction;
    public bool isRelease;
    [ReadOnly,SerializeField]
    private Vector3 velocity;
    public Rigidbody rb;
    [SerializeField]
    private float rotationSpeed;
    public  TrailRenderer m_trail;

    private void OnEnable()
    {
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
        if (m_trail == null)
            yield break;
        m_trail.enabled = false;
        yield return new WaitForSeconds(0.05f);
        m_trail.enabled = true;
        m_trail.Clear();

    }
    private void OnDisable()
    {
        force = 0;
        direction = Vector3.zero;
        isRelease = false;
        velocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        if(m_trail!= null)
         m_trail.Clear();
    }
}
