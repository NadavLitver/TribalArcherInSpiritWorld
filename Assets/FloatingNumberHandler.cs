using System.Collections;
using TMPro;
using UnityEngine;
public class FloatingNumberHandler : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private AnimationCurve moveEase = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private AnimationCurve fadeEase = AnimationCurve.Linear(0, 0, 1, 1);
    private TextMeshPro text;
    [SerializeField] private Color lowColor;
    [SerializeField] private Color highColor;
    [SerializeField] private float lowDamage = 0;
    [SerializeField] private float highDamage = 100;
    [SerializeField] private float height = 5;
    [SerializeField] private float lowSize = 30;
    [SerializeField] private float highSize = 50;
    void OnEnable()
    {
        transform.LookAt(Camera.main.transform.position);
        text = GetComponentInChildren<TextMeshPro>();
    }
    public void Float(int num, float heightMod)
    {
        text.text = num.ToString();
        text.color = Color.Lerp(lowColor, highColor, (num / highDamage) + lowDamage);
        text.fontSize = Mathf.Lerp(lowSize, highSize, (num / highDamage) + lowDamage);
        StartCoroutine(FloatRoutine(heightMod));
    }
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
    private IEnumerator FloatRoutine(float heightMod)
    {
        float curDur;
        curDur = 0;
        Vector3 startPos = transform.position + (Vector3.up * heightMod);
        Vector3 targetPos = startPos + (Vector3.up * height);
        while (curDur < 1)
        {
            curDur += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, moveEase.Evaluate(curDur));
            text.color = new Color(text.color.r, text.color.g, text.color.b, fadeEase.Evaluate(curDur));
            yield return null;
        }
        Destroy(gameObject);
    }
}
