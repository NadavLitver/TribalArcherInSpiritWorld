using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackHandler : MonoBehaviour
{
    public bool isDuringKnockback;
    private PlayerController playerRef;
    private void Awake()
    {
        playerRef = GetComponent<PlayerController>();
    }
    public void CallKnockback(float time, float force, Vector3 dir
)
    {
        StartCoroutine(KnockbackRoutine(time, force, dir));
    }
   
    IEnumerator KnockbackRoutine(float time, float force, Vector3 dir)
    {
        PlayerController.canMove = false;
        playerRef.playerVelocity = dir * force;
        yield return new WaitForSeconds(time);
        playerRef.playerVelocity = Vector3.zero;
        PlayerController.canMove = true;
         

    }
}
