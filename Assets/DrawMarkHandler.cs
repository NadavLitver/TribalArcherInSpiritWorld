using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMarkHandler : MonoBehaviour
{
    [FoldoutGroup("Refrences"),SerializeField]
    private CrosshairCornerHandler[] CrosshairCorners;
    [FoldoutGroup("Refrences"), SerializeField]
    private BowHandler playerBow;
    [FoldoutGroup("Properties"), SerializeField,ReadOnly]
    private bool isPlayerCharging;
    [FoldoutGroup("Properties"), SerializeField]
    private float lerpSpeed = 150;


    public void OnPlayerStartCharge()
    {
        isPlayerCharging = true;
        StartCoroutine(LerpCrosshairCornersRoutine());
    }
    public void OnPlayerReleaseCharge()
    {
        isPlayerCharging = false;
        SetCornersBackToStartingPos();

    }
    void SetCornersBackToStartingPos()
    {
        for (int i = 0; i < CrosshairCorners.Length; i++)
        {
            CrosshairCorners[i].transform.localPosition = Vector3.zero;
         
        }
    }
    IEnumerator LerpCrosshairCornersRoutine()
    {
        while (isPlayerCharging)
        {
            for (int i = 0; i < CrosshairCorners.Length; i++)
            {
                CrosshairCorners[i].transform.localPosition = Vector3.MoveTowards(CrosshairCorners[i].transform.localPosition, CrosshairCorners[i].goal, playerBow.shootHoldTime * 150 *  Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }




}
