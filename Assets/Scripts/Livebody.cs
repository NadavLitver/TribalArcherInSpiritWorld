using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Livebody : MonoBehaviour
{
    [FoldoutGroup("Properties"), ReadOnly]
    public int health;
    [FoldoutGroup("Properties")]
    public int maxHealth;
    [FoldoutGroup("Properties")]
    public bool isVulnerable;
    [FoldoutGroup("Properties")]
    public float timeBackToVulnerable = 0.1f;
    [FoldoutGroup("Refrences")]
    public GameObject DeadBody;
    [FoldoutGroup("Refrences")]
    public GameObject HealthOrb;
    [FoldoutGroup("Refrences"), ReadOnly, SerializeField]
    protected Animator animator;
    [FoldoutGroup("Refrences")]
    public AudioSource audioSource;
    [FoldoutGroup("Events")]
    public UnityEvent hitEvent;
    [FoldoutGroup("Events")]
    public UnityEvent HealEvent;
    [FoldoutGroup("Events")]
    public UnityEvent updateUIBars;
    [FoldoutGroup("Properties"), ReadOnly, Tooltip("Set By Tag")]
    public bool isPlayer;
    [FoldoutGroup("Events")]
    public UnityEvent OnDeath;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        if (hitEvent == null)
            hitEvent = new UnityEvent();
        if (updateUIBars == null)
            updateUIBars = new UnityEvent();
        if (OnDeath == null)
            OnDeath = new UnityEvent();

        isVulnerable = true;
        isPlayer = gameObject.CompareTag("Player");
        health = maxHealth;

    }
    

    public virtual void TakeDamage(int damage)
    {
       
        if (isVulnerable)//normal hit
        {
            StartCoroutine(SetVulnerablelFalse());
            health -= damage;
            updateUIBars.Invoke();
            hitEvent?.Invoke();
            //if(isPlayer)
            //animator.SetTrigger("Hit");
        }

        if (health <= 0)//death
        {
            SummonDeadBody();
            OnDeath.Invoke();
        }


    }
    public virtual void RecieveHealth(int hp)
    {

        if (isVulnerable)//normal hit
        {
            health += hp;
            HealEvent?.Invoke();
            updateUIBars.Invoke();
            //if(isPlayer)
            //animator.SetTrigger("Hit");
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }


    }
    protected virtual void SummonDeadBody()
    {
        if (DeadBody != null)
            Instantiate(DeadBody, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        if (HealthOrb != null)
            Instantiate(HealthOrb, transform.position +(Vector3.up * 5),Quaternion.identity,null);
        
    }
    bool isResetingVulnerable;
    protected IEnumerator SetVulnerablelFalse()
    {
        if (isResetingVulnerable)
            yield break;
        isResetingVulnerable = true;
        isVulnerable = false;
        yield return new WaitForSeconds(timeBackToVulnerable);
        isVulnerable = true;
        isResetingVulnerable = false;

    }
}
