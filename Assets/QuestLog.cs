using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    [SerializeField] GameObject GoToTemple;
    [SerializeField] GameObject CollectRelics;
    [SerializeField] GameObject ActivateTemple;


    void Start()
    {
        TurnOnQuest(GoToTemple,6);
    }
    public void TurnOnQuest(GameObject QuestToTurnOn,float delay)
    {
        StartCoroutine(DelayQuestTurnOn(QuestToTurnOn, delay));
    }
    IEnumerator DelayQuestTurnOn(GameObject QuestToTurnOn,float delay)
    {
        yield return new WaitForSeconds(delay);
        QuestToTurnOn.SetActive(true);

    }
    // Update is called once per frame

}
