using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtHandler : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
    }
}
