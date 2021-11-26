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

    private void OnEnable()
    {
        velocity = direction * force;
        rb.AddForce(velocity, ForceMode.Force);
    }
    private void Update()
    {
        if (direction != Vector3.zero)
        {
          // Vector3 force = new Vector3( 0,0, rb.velocity.y);
            // rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(force), Time.deltaTime * rotationSpeed);
            // transform.LookAt(rb.position + new Vector3(0,0, rb.velocity.y));
            transform.up = rb.velocity;           
          //  rb.AddTorque(force, ForceMode.Impulse);

        }
    }
    private void OnDisable()
    {
        force = 0;
        direction = Vector3.zero;
        isRelease = false;
        velocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }
}
