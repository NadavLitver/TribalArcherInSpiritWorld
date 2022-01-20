using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersHandler : MonoBehaviour
{
    public float PowerBuffer;
    public int CurrPower;
    const int MAX_POWER = 3;
    void Start()
    {
        CurrPower = 0;
    }
    public void AddPower()
    {
        if (CurrPower < MAX_POWER)
        {
            CurrPower++;
        }
    }
    public void UsePowers(int useAmount)
    {
        if (useAmount <= CurrPower)
        {
            CurrPower -= useAmount;
        }
        if (CurrPower < 0)
        {
            CurrPower = 0;
        }
    }
}
