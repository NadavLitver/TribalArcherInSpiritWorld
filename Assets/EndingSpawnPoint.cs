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

    [SerializeField] Transform[] positionToSpawn;
    public UnityEvent GroupFinishedSpawning;
    [FoldoutGroup("Phases")]
    public List<Group> Groups;
    private int currentIndex;
    [SerializeField] bool doShuffleGroups;
    public LayerMask GroundLayer;
    bool finished;
    private void Start()
    {
        GroundSelf();
    }
    public void OnEnable()
    {
        if (doShuffleGroups)
        {
            ShufflePhases();
        }
        CallFirstGroup();
    }
    [Button]
    public void ShufflePhases()
    {
        Groups.Shuffle();
    }
    [Button]
    public void GroundSelf()
    {
        for (int i = 0; i < positionToSpawn.Length; i++)
        {
            if (Physics.Raycast(positionToSpawn[i].position, Vector3.down, out RaycastHit hit, 50, GroundLayer))
            {
                positionToSpawn[i].position = hit.point;
            }
        }
       
    }
    private void CallFirstGroup()
    {
        currentIndex = 0;
        CallGroup(0);
    }
   

    [Button]
    public void CallGroup(int phaseIndex)
    {
        Debug.Log("Starting Phase" + phaseIndex);
        StartCoroutine(SpawnGroup(Groups[phaseIndex]));
    }
    IEnumerator SpawnGroup(Group currentPhase)
    {
        for (int i = 0; i < currentPhase.amountPhase_SuicideEnemy; i++)//SUICIDE
        {
            
            var currentBody = Instantiate(SuicideEnemy, GetRandomTransformPoint().position, Quaternion.identity);
            EnemySpawnerManager.instance.endingSceneLiveBodies.Add(currentBody);
            VFXManager.Play(VFXManager.Effect.SpawnEffect, currentBody.transform.position);
            currentBody.m_stateHandler.addToEnemySpawner = false;
            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
            currentBody.m_stateHandler.ToggleAllStatesTarget();
            currentBody.gameObject.SetActive(true);
           
        }
        yield return new WaitForSeconds(currentPhase.timeBetweenType);
        /////
        for (int i = 0; i < currentPhase.amountPhase_StatueEnemy; i++)//Statues
        {
            var currentBody = Instantiate(StatueEnemy, GetRandomTransformPoint().position, Quaternion.identity);
            EnemySpawnerManager.instance.endingSceneLiveBodies.Add(currentBody);
            VFXManager.Play(VFXManager.Effect.SpawnEffect, currentBody.transform.position);
            currentBody.m_stateHandler.addToEnemySpawner = false;
            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
          //  currentBody.m_stateHandler.ToggleAllStatesTarget();
            currentBody.gameObject.SetActive(true);

          
        }
        yield return new WaitForSeconds(currentPhase.timeBetweenType);
        /////
        for (int i = 0; i < currentPhase.amountPhase_FlyingEnemy; i++)//FLYING
        {
            var currentBody = Instantiate(flyingEnemy, GetRandomTransformPoint().position, Quaternion.identity);
            EnemySpawnerManager.instance.endingSceneLiveBodies.Add(currentBody);
            VFXManager.Play(VFXManager.Effect.SpawnEffect, currentBody.transform.position);
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
            var currentBody = Instantiate(flyingEliteEnemy, GetRandomTransformPoint().position, Quaternion.identity);
            EnemySpawnerManager.instance.endingSceneLiveBodies.Add(currentBody);
            currentBody.m_stateHandler.addToEnemySpawner = false;
            VFXManager.Play(VFXManager.Effect.SpawnEffect, currentBody.transform.position);
            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
            currentBody.m_stateHandler.ToggleAllStatesTarget();
            currentBody.gameObject.SetActive(true);

            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
        }
        yield return new WaitForSeconds(currentPhase.timeBetweenType);
        /////
        for (int i = 0; i < currentPhase.amountPhase_StatueElite; i++)//Statue
        {
            var currentBody = Instantiate(StatueEliteEnemy, GetRandomTransformPoint().position, Quaternion.identity);
            EnemySpawnerManager.instance.endingSceneLiveBodies.Add(currentBody);
            currentBody.m_stateHandler.addToEnemySpawner = false;
            VFXManager.Play(VFXManager.Effect.SpawnEffect, currentBody.transform.position);
            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
           // currentBody.m_stateHandler.ToggleAllStatesTarget();
            currentBody.gameObject.SetActive(true);

            yield return new WaitForSeconds(currentPhase.timeBetweenEachEnemy);
        }
        for (int i = 0; i < 3; i++)
        {
            EnemySpawnerManager.instance.ToggleRandomActiveEnemy();

        }
        GroupFinishedSpawning?.Invoke();
        CallNextWave();
    }
    void CallNextWave()
    {
        currentIndex++;
        if (currentIndex < Groups.Count - 1)
        {

           CallGroup(currentIndex);
        }
        else
        {
            finished = true;
        }
    }
    public Transform GetRandomTransformPoint()
    {
        int randomNum = Random.Range(0, positionToSpawn.Length);
        return positionToSpawn[randomNum];
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
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < positionToSpawn.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(positionToSpawn[i].position, 5);
        }
    }
}

[System.Serializable]
public class Group
{
    public int amountPhase_FlyingEnemy, amountPhase_SuicideEnemy, amountPhase_StatueEnemy, amountPhase_FlyingElite, amountPhase_StatueElite;
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