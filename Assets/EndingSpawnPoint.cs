using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingSpawnPoint : MonoBehaviour
{
    [SerializeField] Livebody flyingEnemy;
    [SerializeField] Livebody SuicideEnemy;
    [SerializeField] Livebody StatueEnemy;
    [SerializeField] Transform positionToSpawn;
    public UnityEvent GroupFinishedSpawning;
    [FoldoutGroup("Phases")]
    public List<Group> phases;
    private int currentIndex;

    public void Start()
    {
        //CallFirstPhase();
    }

    private void CallFirstPhase()
    {
        currentIndex = 0;
        CallPhase(0);
    }

    [Button]
    public void CallPhase(int phaseIndex)
    {
        Debug.Log("Starting Phase" + phaseIndex);
        StartCoroutine(SpawnPhase(phases[phaseIndex]));
    }
    IEnumerator SpawnPhase(Group currentPhase)
    {
        
        for (int i = 0; i < currentPhase.amountPhase_SuicideEnemy; i++)//SUICIDE
        {
            
            var currentBody = Instantiate(SuicideEnemy, positionToSpawn.position, Quaternion.identity);
            currentBody.m_stateHandler.addToEnemySpawner = false;
            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
            currentBody.m_stateHandler.ToggleAllStatesTarget();
            currentBody.gameObject.SetActive(true);
           
        }
        yield return new WaitForSeconds(currentPhase.timeBetweenType);
        /////
        for (int i = 0; i < currentPhase.amountPhase_StatueEnemy; i++)//Statues
        {
            var currentBody = Instantiate(StatueEnemy, positionToSpawn.position, Quaternion.identity);
            currentBody.m_stateHandler.addToEnemySpawner = false;
            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
            currentBody.m_stateHandler.ToggleAllStatesTarget();
            currentBody.gameObject.SetActive(true);

          
        }
        yield return new WaitForSeconds(currentPhase.timeBetweenType);
        /////
        for (int i = 0; i < currentPhase.amountPhase_FlyingEnemy; i++)//FLYING
        {
            var currentBody = Instantiate(flyingEnemy, positionToSpawn.position, Quaternion.identity);
            currentBody.m_stateHandler.addToEnemySpawner = false;
            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
            currentBody.m_stateHandler.ToggleAllStatesTarget();
            currentBody.gameObject.SetActive(true);

            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
        }
        GroupFinishedSpawning?.Invoke();
        CallNextWave();
    }
    void CallNextWave()
    {
        currentIndex++;
        if (currentIndex < phases.Count - 1)
        {

           CallPhase(currentIndex);
        }
    }
    public Livebody GetRandomEnemy()
    {
        int randomNum = Random.Range(0, 3);

        switch (randomNum)
        {
            case 0:
                return flyingEnemy;
            case 1:
                return SuicideEnemy;
            case 2:
                return StatueEnemy;
            default:
                return flyingEnemy;
        }

    }
}

[System.Serializable]
public class Group
{
    public int amountPhase_FlyingEnemy, amountPhase_SuicideEnemy, amountPhase_StatueEnemy;
    public float timeBetweenType;
    public float timeBetweenEachEnemy;
    public int TimeToNextGroup;    



}
public enum EnemyType
{
    flying,
    suicide,
    statue,
}