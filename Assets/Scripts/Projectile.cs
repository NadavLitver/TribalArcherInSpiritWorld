using Sirenix.OdinInspector;
using UnityEngine;

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
    public TrailRenderer m_trail;

    private void OnEnable()
    {
        velocity = direction * force;
        rb.AddForce(velocity, ForceMode.Force);
    }
    private void Update()
    {
        if (direction != Vector3.zero)
            transform.up = rb.velocity;           



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
