using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivebody : Livebody
{
    Vector3 startingWorldPos;
    [SerializeField, ReadOnly,FoldoutGroup("Refrences")]
    PlayerController controller;
   
    void Start()
    {
        startingWorldPos = transform.position;
        controller = GetComponent<PlayerController>();
    }
    protected override void SummonDeadBody()
    {
        base.SummonDeadBody();
        SoundManager.Play(SoundManager.Sound.PlayerDead, audioSource, 0.5f);
        controller.Stop(1);
        transform.position = startingWorldPos;
        health = 100;
      


    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        SoundManager.Play(SoundManager.Sound.PlayerHit, audioSource, 0.5f);
        PostProccessManipulator.SetVignette(health);
        PostProccessManipulator.OnHitVignette();
    }
    public override void RecieveHealth(int hp)
    {
        SoundManager.Play(SoundManager.Sound.HealthOrbReceived, audioSource, 0.15f);
        Debug.Log("Player Recieved Health" + hp);
        base.RecieveHealth(hp);
        PostProccessManipulator.SetVignette(health);

    }

}
