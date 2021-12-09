using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections;


public class ProjectileBase : MonoBehaviour
{
    public float force;
    public Vector3 direction;
    [ReadOnly, SerializeField]
    protected Vector3 velocity;
    public Rigidbody rb;


    private void OnEnable()
    {
       
        velocity = direction * force;
        rb.AddForce(velocity, ForceMode.Force);

    }
    private void Update()
    {
        if (direction != Vector3.zero)
            transform.forward = rb.velocity;



    }
  
    private void OnDisable()
    {
        force = 0;
        direction = Vector3.zero;
        velocity = Vector3.zero;
        rb.velocity = Vector3.zero;
      
    }
}
