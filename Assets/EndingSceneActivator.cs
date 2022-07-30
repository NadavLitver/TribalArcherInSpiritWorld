using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingSceneActivator : InteractableBase
{
    [SerializeField] DayNightManager nightManager;
    [SerializeField] GameObject PhaseOneFather;
    [SerializeField] GameObject PhaseTwoFather;
    [SerializeField] GameObject PhaseThreeFather;


    [SerializeField] GameObject PhaseOneRelic;
    [SerializeField] GameObject PhaseTwoRelic;
    [SerializeField] GameObject PhaseThreeRelic;
    [SerializeField] Transform newPositionTransform;
    public UnityEvent OnNewPos;
    public UnityEvent OnFirstInteract;
    public UnityEvent OnSecondInteract;
    public UnityEvent OnThridInteract;

    bool firstInteract;
    bool secondInteract;
    bool thirdInteract;

    private void Start()
    {
        firstInteract = false;
        secondInteract = false;
        thirdInteract = false;
    }
    public override void Interact()
    {
        if (!firstInteract)//first phase
        {
            OnFirstInteract?.Invoke();
            nightManager.Toggle();
            EnemySpawnerManager.instance.spawn = false;
            firstInteract = true;
            PhaseOneRelic.SetActive(true);
            StartCoroutine(MovePositionDelay());
          //  PhaseOneFather.SetActive(true);
            return;
        }
        if (!secondInteract)
        {
            OnSecondInteract?.Invoke();

            //PhaseTwoFather.SetActive(true);
            base.Interact();

            PhaseTwoRelic.SetActive(true);

            secondInteract = true;
            return;

        }
        if (!thirdInteract)
        {
            OnThridInteract?.Invoke();

            // PhaseThreeFather.SetActive(true);
            PhaseThreeRelic.SetActive(true);

            thirdInteract = true;
            return;

        }
        IEnumerator MovePositionDelay()
        {
            yield return new WaitForSeconds(20);
            Vector3 newPos = newPositionTransform.position;
            transform.position = newPos;
            OnNewPos?.Invoke();
        }
    }
}
