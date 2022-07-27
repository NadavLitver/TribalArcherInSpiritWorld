using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealthOrb : InteractableBase
{
    
    private PlayerLivebody player;
    public float duration;
    public AnimationCurve FlyCurve;
    public AnimationCurve FlyToGroundCurve;
    public GameObject AfterHitEffect;
    public float ConsumeDistance;
    public int healingToApply;
    public LayerMask groundLayer;
    public bool floatinessEnabled;
    public float frequency;
    public float speed;
    private float calcSpeed;

    private void Awake()
    {
        player = FindObjectOfType<PlayerLivebody>();

    }
    private void OnEnable()
    {
        StartCoroutine(FlyTowardsGround());
        calcSpeed = Random.Range(speed - 1, speed + 1);
    }
    private IEnumerator FlyTowardsGround()
     {
        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 50, groundLayer))
        {
            yield break;
        }
        Vector3 goal = hitInfo.point + (Vector3.up * 0.9f);
        var startPos = transform.position;
        float curDuration = 0;
        while ((goal - transform.position).magnitude > ConsumeDistance)
        {
            curDuration += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, goal, FlyToGroundCurve.Evaluate(curDuration / duration));
            yield return new WaitForEndOfFrame();
        }
        float currentTime = Time.time - Randomizer.ReturnRandomNum(new Vector2Int(0, 3));
        while (floatinessEnabled)
        {
            
            currentTime += Time.deltaTime;
            float y =  goal.y + Mathf.PingPong(currentTime * calcSpeed, 1) * frequency;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            yield return new WaitForEndOfFrame();

        }
    }
    public override void OnPlayerEnter()
    {
        if (HUD.instance.healthPower.isFull)
        {
            return;
        }
        StopAllCoroutines();
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
        while ((player.transform.position - transform.position).magnitude > ConsumeDistance)
        {
            curDuration += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, player.transform.position, FlyCurve.Evaluate(curDuration / duration));
            yield return new WaitForEndOfFrame();
        }
        AfterHitEffect.transform.parent = null;
        AfterHitEffect.SetActive(true);
        AfterHitEffect.transform.position = transform.position;
        HUD.instance.healthPower.GainOrb();
        Destroy(gameObject);
    }
}

