using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtHandler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool onlyY = false;
    private void OnEnable()
    {
        if (target == null)
        {
            target = PlayerController.playerTransform;
        }
    }
    void Update()
    {
        transform.LookAt(target.position);
        if (onlyY)
        {
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
        }

    }
}
