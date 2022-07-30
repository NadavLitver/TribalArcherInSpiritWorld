using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_RoadStones : InteractableBase
{
    public static Vector3 lastPos;
    [SerializeField] private Transform roadStonesParent;
    private List<GameObject> roadStones;
    private Vector3 playerPos => PlayerController.playerTransform.position;
    [SerializeField]private float KillDistance = 15;

    private void OnEnable()
    {
        roadStones = new List<GameObject>();
        for (int i = 0; i < roadStonesParent.childCount - 1; i++)
        {
            if (roadStonesParent.GetChild(i).gameObject.activeInHierarchy)
            {
                roadStones.Add(roadStonesParent.GetChild(i).gameObject);
            }
        }
    }
    private void Update()
    {
        foreach (GameObject item in roadStones)
        {
            float distance = Vector3.Distance(item.transform.position, playerPos);
            item.SetActive(distance < KillDistance);
        }
    }



}
