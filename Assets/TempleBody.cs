using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleBody : Livebody
{
    public static Transform TempleTransform;
    [SerializeField] Transform StaticTransform;
    private void Awake()
    {
        health = maxHealth;
        TempleTransform = StaticTransform;
        isVulnerable = true;
    }
}
