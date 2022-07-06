using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBody : Livebody
{
    [FoldoutGroup("Properties"), Range(0, 100),SerializeField]
    float chanceToDropHealthOrb;
    [SerializeField] private HitEffectHandler hitEffect;
    protected override void SummonDeadBody()
    {
        float ran = Randomizer.ReturnRandomFloat(new Vector2(0,100));
        if (HealthOrb != null && ran > chanceToDropHealthOrb)
        {
           Instantiate(HealthOrb, CenterPivot.position, Quaternion.identity, null);
        }
        SoundManager.Play(SoundManager.Sound.OwlDead, audioSource);
        //VFXManager.Play(VFXManager.Effect.FlyingEnemyDead, CenterPivot.position);
    }
    public override void TakeDamage(int damage)
    {
        hitEffect.Hit();
        VFXManager.PlayFloatingNumber(transform.position, damage, 8f);
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
