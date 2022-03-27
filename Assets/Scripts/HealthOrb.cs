using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealthOrb : InteractableBase
{
    
    private PlayerLivebody player;
    public float duration;
    public AnimationCurve FlyCurve;
    public GameObject AfterHitEffect;
    public int healingToApply;
    private void Awake()
    {
        player = FindObjectOfType<PlayerLivebody>();

    }
    public override void OnPlayerEnter()
    {
        StartCoroutine(FlyTowardsPlayer());
        base.OnPlayerEnter();
    }
    public override void Interact()
    {
    }
    private IEnumerator FlyTowardsPlayer()
    {
        var startPos = transform.position;
        float curDuration = 0;
        while ((player.transform.position - transform.position).magnitude > 0.1f)
        {
            curDuration += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, player.transform.position, FlyCurve.Evaluate(curDuration / duration));
            yield return new WaitForEndOfFrame();
        }
        AfterHitEffect.transform.parent = null;
        AfterHitEffect.SetActive(true);
        AfterHitEffect.transform.position = transform.position;
        player.RecieveHealth(healingToApply);
        Destroy(gameObject);
    }
}

