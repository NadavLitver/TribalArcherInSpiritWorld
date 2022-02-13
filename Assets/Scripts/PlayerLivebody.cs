using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivebody : Livebody
{
  
    public override void RecieveHealth(int hp)
    {
        Debug.Log("Player Recieved Health" + hp);
        base.RecieveHealth(hp);
    }
}
