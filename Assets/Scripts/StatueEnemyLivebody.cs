using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueEnemyLivebody : Livebody
{
    void OnEnable()
    {
        isVulnerable = true;
    }
    void OnDisable()
    {
        isVulnerable = false;

    }
}
