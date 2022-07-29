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
    [SerializeField] Livebody StatueEliteEnemy;
    [SerializeField] Livebody flyingEliteEnemy;

    [SerializeField] Transform positionToSpawn;
    public UnityEvent GroupFinishedSpawning;
    [FoldoutGroup("Phases")]
    public List<Phase> phases;
    private int currentIndex;
    [SerializeField] bool doShufflePhases;
    public LayerMask GroundLayer;
    private void Start()
    {
        GroundSelf();
    }
    public void OnEnable()
    {
        if (doShufflePhases)
        {
            ShufflePhases();
        }
        CallFirstPhase();
    }
    [Button]
    public void ShufflePhases()
    {
        phases.Shuffle();
    }
    [Button]
    public void GroundSelf()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 50, GroundLayer))
        {
            transform.position = hit.point;
        }
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
    IEnumerator SpawnPhase(Phase currentPhase)
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
        yield return new WaitForSeconds(currentPhase.timeBetweenType);
        /////
        for (int i = 0; i < currentPhase.amountPhase_FlyingElite; i++)//FLYING
        {
            var currentBody = Instantiate(flyingEliteEnemy, positionToSpawn.position, Quaternion.identity);
            currentBody.m_stateHandler.addToEnemySpawner = false;
            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
            currentBody.m_stateHandler.ToggleAllStatesTarget();
            currentBody.gameObject.SetActive(true);

            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
        }
        yield return new WaitForSeconds(currentPhase.timeBetweenType);
        /////
        for (int i = 0; i < currentPhase.amountPhase_StatueElite; i++)//FLYING
        {
            var currentBody = Instantiate(StatueEliteEnemy, positionToSpawn.position, Quaternion.identity);
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
public class Phase
{
    public int amountPhase_FlyingEnemy, amountPhase_SuicideEnemy, amountPhase_StatueEnemy, amountPhase_StatueElite, amountPhase_FlyingElite;
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
public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> cList)
    {
        var count = cList.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = cList[i];
            cList[i] = cList[r];
            cList[r] = tmp;
        }
    }
}