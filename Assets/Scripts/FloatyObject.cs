using UnityEngine;

public class FloatyObject : MonoBehaviour
{
    public AnimationCurve myCurve;
    private float myTime;
    private float ranNum;
    public Transform baseHeight;
    void Start()
    {
        ranNum = Randomizer.ReturnRandomFloat(new Vector2(1f, 2f));
        myTime = ranNum;
    }
    void Update()
    {

        myTime += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate((myTime % ranNum)), transform.position.z);



    }

}
