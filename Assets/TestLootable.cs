using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLootable : InteractableBase
{
    public MeshRenderer renderer;
    public Material AfterEnterMaterial;
    private Transform player;
    public float duration;
    public AnimationCurve FlyCurve;
    private Material BeforeInteractMaterial;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }
    private void OnEnable()
    {
         
        BeforeInteractMaterial = renderer.material;
    }
    public override void OnPlayerEnter()
    {
        renderer.material = AfterEnterMaterial;
        StartCoroutine(FlyTowardsPlayer());
        base.OnPlayerEnter();
    }
    public override void Interact()
    {

        return;


    }
    private IEnumerator FlyTowardsPlayer()
    {
        var startPos = transform.position;
        float curDuration = 0;
        while((player.position - transform.position).magnitude > 0.1f)
        {
            curDuration += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, player.position, FlyCurve.Evaluate(curDuration / duration));
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
        // after it ends do stuff to player 
    }
}
