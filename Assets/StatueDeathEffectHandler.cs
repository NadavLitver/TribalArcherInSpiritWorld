using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueDeathEffectHandler : MonoBehaviour
{
    private void OnEnable()
    {
        transform.forward = -ArrowProjectile.savedNormal;
        Debug.Log(ArrowProjectile.savedNormal);
    }
}
