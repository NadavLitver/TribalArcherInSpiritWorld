using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedArrowHandler : MonoBehaviour
{
    [SerializeField] private float TTL;
    private void OnEnable()
    {
        StartCoroutine(DisableRoutine(TTL));
    }
    private IEnumerator DisableRoutine(float ttl)
    {
        yield return new WaitForSeconds(ttl);
        this.gameObject.SetActive(false);
    }
}
