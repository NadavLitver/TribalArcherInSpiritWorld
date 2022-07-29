using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleBody : Livebody
{
    
    [SerializeField] Transform[] hitPoints;
    private void Awake()
    {
        health = maxHealth;
       // TempleTransform = StaticTransform;
        isVulnerable = true;
    }
    public Transform GetRandomTransform()
    {
        return hitPoints[Random.Range(0, hitPoints.Length)];
    }
}
