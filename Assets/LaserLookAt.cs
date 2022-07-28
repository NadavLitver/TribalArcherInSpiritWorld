using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLookAt : MonoBehaviour
{
    public Transform target;
    private void Start()
    {
        if (target == null)
        {
            target = PlayerController.playerTransform;
        }
    }
    void Update()
    {
        
    }
}
