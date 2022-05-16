using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyGfxHandler : MonoBehaviour
{
    private void OnEnable()
    {
        ResetGFXRotation();
    }
    public void ResetGFXRotation()
    { 
      transform.rotation = new Quaternion(0,0,0,0);
    }
    private void OnDisable()
    {
      transform.rotation = new Quaternion(0, 0, 0, 0);

    }
}
