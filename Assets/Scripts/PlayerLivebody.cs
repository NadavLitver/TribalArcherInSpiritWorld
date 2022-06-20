using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivebody : Livebody
{
    Vector3 startingWorldPos;
    [SerializeField, ReadOnly,FoldoutGroup("Refrences")]
    PlayerController controller;
    [SerializeField, FoldoutGroup("Refrences")] FadeToBlack fadeToBlack;
   
    void Start()
    {
        startingWorldPos = transform.position;
        controller = GetComponent<PlayerController>();
    }
    protected override void SummonDeadBody()
    {
        
      SceneMaster.instance.StartCoroutine(DeathRoutine());


    }
    IEnumerator DeathRoutine()
    {
        controller.Stop(2);
        SoundManager.Play(SoundManager.Sound.PlayerDead, audioSource, 1f);
        fadeToBlack.PlayFade();
        yield return new WaitForSeconds(3);
        SceneMaster.instance.LoadLevel(SceneMaster.instance.GetCurrentIndex());
        
        //transform.position = startingWorldPos;
        //health = 100;
        //PostProccessManipulator.SetVignette(health);
    }
    void OnEnable()
    {
        isVulnerable = true;
        SoundManager.Play(SoundManager.Sound.PlayerEnter, audioSource, 1f);
    }
    void OnDisable()
    {
        isVulnerable = false;

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
        base.RecieveHealth(hp);
        PostProccessManipulator.SetVignette(health);

    }

}
