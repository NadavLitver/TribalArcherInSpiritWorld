using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowHandler : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Refrences"),ReadOnly] private InputManager input;
    [SerializeField, FoldoutGroup("Refrences")] private Transform UXArrow;
    [SerializeField, FoldoutGroup("Refrences")] private ObjectPool objectPool;
    [SerializeField, FoldoutGroup("Refrences"),ReadOnly] private  Camera cam;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] public bool isShooting;
    [SerializeField,FoldoutGroup("Properties"), ReadOnly]  private float uxArrowStartingZ;
    [SerializeField, FoldoutGroup("Properties")] private float arrowForce;
    [FoldoutGroup("Properties"), ReadOnly] public float shootHoldTime;
    

    private void Start()
    {
        cam = Camera.main;
        input = InputManager.Instance;
        input.OnPlayerStartShoot.AddListener(OnShoot);
        input.OnPlayerReleaseShoot.AddListener(OnRelease);
        uxArrowStartingZ = UXArrow.localPosition.z;

    }
    private void Update()
    {
        Shoot();

    }
    private void OnRelease()
    {
        if (!isShooting)
            return;
        isShooting = false;
       StartCoroutine(ReleaseArrow());

    }

    private IEnumerator ReleaseArrow()
    {
        var arrow = objectPool.GetPooledObject();
        arrow.transform.position = UXArrow.position;
        arrow.transform.rotation = UXArrow.rotation;
        UXArrow.gameObject.SetActive(false);

        var arrowProj = arrow.GetComponent<ArrowProjectile>();
        arrowProj.direction = ShootDirection().normalized;
        arrowProj.force = arrowForce * shootHoldTime;
        arrowProj.appliedDamage = Mathf.RoundToInt(arrowProj.maxDamage * shootHoldTime);
        arrow.SetActive(true);
        shootHoldTime = 0;
        yield return new WaitForSeconds(0.1f);
        UXArrow.localPosition = new Vector3(UXArrow.localPosition.x, UXArrow.localPosition.y, uxArrowStartingZ);
        UXArrow.gameObject.SetActive(true);


    }
    private Vector3 ShootDirection()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray,out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(200);
        }
        Vector3 direction = (targetPoint - transform.position);
        return direction;
    }

    private void OnShoot()
    {
        isShooting = true;
        UXArrow.localPosition = new Vector3(UXArrow.localPosition.x, UXArrow.localPosition.y,uxArrowStartingZ);
        UXArrow.gameObject.SetActive(true);
        //  uxArrowLerpT = 0;
    }
    private void Shoot()
    {
        if (isShooting)
        {
            shootHoldTime += Time.deltaTime;
            UXArrow.localPosition = new Vector3(UXArrow.localPosition.x, UXArrow.localPosition.y, Mathf.Lerp(uxArrowStartingZ, uxArrowStartingZ - 0.2f, shootHoldTime /1));
        }
    }
    private void OnDisable()
    {
        input.OnPlayerStartShoot.RemoveListener(OnShoot);
        input.OnPlayerReleaseShoot.RemoveListener(OnRelease);
    }
    private void OnDrawGizmos()  
    {
        //if(Application.isPlaying)
        //Gizmos.DrawRay(UXArrow.position, ShootDirection().normalized * arrowForce);
    }
}
