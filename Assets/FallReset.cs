using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReset : InteractableBase
{
    [SerializeField] FadeToBlack fade;
    [SerializeField] private float yOffset;
    public override void OnPlayerEnter()
    {
        base.OnPlayerEnter();
    }
    public void ResetPos()
    {
        StartCoroutine(ResetPosRoutine());
    }
    private IEnumerator ResetPosRoutine()
    {
        fade.PlayFade();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        PlayerController.playerTransform.position = Path_RoadStones.lastPos + Vector3.up * yOffset;
        Time.timeScale = 1;
        fade.PlayFromFade();
    }
}
