using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodHandler : MonoBehaviour
{
    private Image m_image;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;

    [SerializeField] private Sprite[] spriteList;

    [SerializeField] Color IdleColor;
    [SerializeField] Color ActiveColor;

    public float angle { private get; set; }
    private void OnEnable()
    {
        m_image = GetComponent<Image>();
        SetRandomImage();
        m_image.color = Color.clear;
        transform.localRotation = new Quaternion(0, 0, angle, 0);
        Splatter();
    }
    private void SetRandomImage()
    {
        m_image.sprite = spriteList[Random.Range(0, spriteList.Length)];
        m_image.SetNativeSize();
    }
    public void Splatter()
    {
        StopAllCoroutines();
        StartCoroutine(SplatterRoutine());
    }
    private IEnumerator SplatterRoutine()
    {
        SetRandomImage();
        float currDurr = 0;
        float duration = Random.Range(minDuration, maxDuration);
        m_image.color = ActiveColor;
        while (currDurr < duration)
        {
            currDurr += Time.deltaTime;
            m_image.color = Color.Lerp(ActiveColor, IdleColor,  currDurr / duration);
            yield return new WaitForEndOfFrame();
        }
        m_image.color = ActiveColor;
        gameObject.SetActive(false);
    }
}
