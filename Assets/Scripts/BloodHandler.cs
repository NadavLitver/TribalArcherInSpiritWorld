using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodHandler : MonoBehaviour
{
    private Image m_image;
    [SerializeField] private float duration;
    [SerializeField] private Sprite[] spriteList;
    [SerializeField] AnimationCurve m_curve;

    [SerializeField] Color IdleColor;
    [SerializeField] Color ActiveColor;
    private void Start()
    {
        m_image = GetComponent<Image>();
        SetRandomImage();
        m_image.color = Color.clear;
    }
    private void SetRandomImage()
    {
        m_image.sprite = spriteList[Random.Range(0, spriteList.Length)];
        m_image.SetNativeSize();
    }
    public void Splatter(float rad)
    {
        StopAllCoroutines();
        StartCoroutine(SplatterRoutine(rad));
    }
    private IEnumerator SplatterRoutine(float rad)
    {
        SetRandomImage();
        float currDurr = 0;
        
        transform.localRotation = new Quaternion(0,0,rad,0);
        m_image.color = ActiveColor;
        while (currDurr < duration)
        {
            currDurr += Time.deltaTime;
            m_image.color = Color.Lerp(ActiveColor, IdleColor, currDurr / duration);
            yield return new WaitForEndOfFrame();
        }
        m_image.color = ActiveColor;
    }
}
