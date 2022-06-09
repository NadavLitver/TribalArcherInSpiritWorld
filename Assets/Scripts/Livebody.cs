using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Livebody : MonoBehaviour
{
    [FoldoutGroup("Properties"), ReadOnly]
    public int health;
    [FoldoutGroup("Properties")]
    public int maxHealth;
    [FoldoutGroup("Properties")]
    public bool isVulnerable = true;
    [FoldoutGroup("Properties")]
    public float timeBackToVulnerable = 0.1f;
    [FoldoutGroup("Refrences")]
    public GameObject DeadBody;
    [FoldoutGroup("Refrences")]
    public GameObject HealthOrb;
    [FoldoutGroup("Refrences"), ReadOnly, SerializeField]
    protected Animator animator;
    [FoldoutGroup("Refrences")]
    public Transform CenterPivot;
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
    internal LivebodyStateHandler m_stateHandler;

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

        if (isVulnerable)
        {
            health -= damage;
            StartCoroutine(SetVulnerablelFalse());
            updateUIBars.Invoke();
            hitEvent?.Invoke();

            if (health <= 0)//death
            {
                health = 0;
                SummonDeadBody();
                OnDeath.Invoke();
                isVulnerable = false;
            }
        }

        

    }
    public virtual void RecieveHealth(int hp)
    {


        health += hp;
        HealEvent?.Invoke();
        updateUIBars.Invoke();



        if (health > maxHealth)
        {
            health = maxHealth;
        }


    }
    protected virtual void SummonDeadBody()
    {
        if (DeadBody != null)
            Instantiate(DeadBody, CenterPivot.position, Quaternion.identity, null);
        if (HealthOrb != null)
            Instantiate(HealthOrb, CenterPivot.position, Quaternion.identity, null);

    }
    bool isResetingVulnerable;
    protected IEnumerator SetVulnerablelFalse()
    {
        if (isResetingVulnerable)
            yield break;
        isResetingVulnerable = true;
        isVulnerable = false;
        yield return new WaitForSeconds(timeBackToVulnerable);
        if(health > 0){
            isVulnerable = true;
        }
        
        isResetingVulnerable = false;

    }
}
