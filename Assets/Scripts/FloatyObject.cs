using UnityEngine;

public class FloatyObject : MonoBehaviour
{
    //public AnimationCurve myCurve;
    //private float myTime;
    //private float ranNum;
    public Transform baseHeight;
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    void Start()
    {

        //ranNum = Randomizer.ReturnRandomFloat(new Vector2(1f, 2f));
        //myTime = ranNum;
        posOffset = transform.position;
    }
    void Update()
    {

        //myTime += Time.deltaTime;
        //transform.position = new Vector3(transform.position.x, (baseHeight.position.y + myCurve.Evaluate((myTime % ranNum))), transform.position.z);
        
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        if(tempPos.y < baseHeight.position.y)
        {
            tempPos.y = baseHeight.position.y;
        }
        transform.position = new Vector3(transform.position.x, tempPos.y,transform.position.z) ;

    }

}
