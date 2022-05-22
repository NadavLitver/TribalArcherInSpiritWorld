using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberLivebody : Livebody
{
    [FoldoutGroup("Properties"), Range(0, 100), SerializeField]
    float chanceToDropHealthOrb;
    
    protected override void SummonDeadBody()
    {
        float ran = Randomizer.ReturnRandomFloat(new Vector2(0, 100));
        if (HealthOrb != null && ran > chanceToDropHealthOrb)
        {
            Instantiate(HealthOrb, CenterPivot.position, Quaternion.identity, null);
        }
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
