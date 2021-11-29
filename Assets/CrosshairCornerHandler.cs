using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCornerHandler : MonoBehaviour
{

    public Vector3 goal;
    [ReadOnly]
    public Vector3 startingPos;
    private void Awake()
    {
        startingPos = transform.localPosition;
    }
}
