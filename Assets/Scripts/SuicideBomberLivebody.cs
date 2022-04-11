using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberLivebody : Livebody
{
    protected override void SummonDeadBody()
    {
        base.SummonDeadBody();
        SoundManager.Play(SoundManager.Sound.OwlDead, audioSource);
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        SoundManager.Play(SoundManager.Sound.OwlHit, audioSource);

    }
    void OnEnable()
    {
        isVulnerable = true;
    }
    void OnDisable()
    {
        isVulnerable = false;

    }
}
