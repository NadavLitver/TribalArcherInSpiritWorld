using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMaskEffectHandler : MonoBehaviour
{
    private MeshRenderer renderer;
    private Rigidbody rb;

    private bool flag = false;

    [SerializeField] private float forceMod = 20f;
    [SerializeField] private AnimationCurve sizeEase;
    [SerializeField] private float TTL = 5f;
    private void OnEnable()
    {
        renderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        renderer.enabled = false;
    }
    public void Detach()
    {
        if (flag)
        {
            return;
        }
        else
        {
            flag = true;
        }
        renderer.enabled = true;
        rb.isKinematic = false;
        rb.AddExplosionForce(forceMod, ArrowProjectile.savedPos, forceMod);
        //Debug.Log("arrow name: " + transform.GetChild(transform.childCount - 1).name + "; applied force: " + transform.GetChild(transform.childCount - 1).forward.normalized);
        transform.parent = null;
        StartCoroutine(DisableRoutine());
    }
    private IEnumerator DisableRoutine()
    {
        float startSize = transform.localScale.x;
        float endSize = 0;
        float curDur = 0;
        while (curDur < 1)
        {
            curDur += Time.deltaTime / TTL;
            transform.localScale = Vector3.one * Mathf.Lerp(startSize, endSize, sizeEase.Evaluate(curDur));
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
