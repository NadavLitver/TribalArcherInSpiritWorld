using Sirenix.OdinInspector;
using UnityEngine;

public class StatueEnemyLivebody : Livebody
{
    [FoldoutGroup("Properties"), Range(0, 100), SerializeField]
    float chanceToDropHealthOrb;
    [SerializeField] private HitEffectHandler hitEffect;
    protected override void SummonDeadBody()
    {
        float ran = Randomizer.ReturnRandomFloat(new Vector2(0, 100));
        if (HealthOrb != null && ran > chanceToDropHealthOrb)
        {
            Instantiate(HealthOrb, CenterPivot.position, Quaternion.identity, null);
        }
        SoundManager.Play(SoundManager.Sound.StatueDead, transform.position, 1);
        EnemySpawnerManager.instance.RemoveMe(this);

    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        VFXManager.PlayFloatingNumber(transform.position, damage, 12f);
        hitEffect.Hit();
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
