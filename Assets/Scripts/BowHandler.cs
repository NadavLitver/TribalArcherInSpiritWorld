using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowHandler : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Refrences"),ReadOnly] private InputManager input;
    [SerializeField, FoldoutGroup("Refrences")] private BowString bowString;
    [SerializeField, FoldoutGroup("Refrences")] private Transform UXArrow;
    [SerializeField, FoldoutGroup("Refrences")] private ChainLightingShot QuickShotAbiliyRef;
    [SerializeField, FoldoutGroup("Refrences")] private ObjectPool NormalArrowPool;
    [SerializeField, FoldoutGroup("Refrences"),ReadOnly] private  Camera cam;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] public bool isShooting;
    [SerializeField,FoldoutGroup("Properties"), ReadOnly]  private float UXArrowStartingZ;
    [SerializeField, FoldoutGroup("Properties")] private float arrowForce;
    [FoldoutGroup("Properties"), ReadOnly] public float shootHoldTime;
    [FoldoutGroup("Properties"), ReadOnly] private float maxHoldTime;




    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        input = InputManager.Instance;
        input.OnPlayerStartShoot.AddListener(OnShoot);
        input.OnPlayerReleaseShoot.AddListener(OnRelease);
        input.OnPlayerFinishCharge.AddListener(OnChargeMaxed);
        maxHoldTime = input.shootHoldTime;
        UXArrowStartingZ = UXArrow.localPosition.z;

    }
    private void Update()
    {
        Shoot();

    }
    private void OnRelease()
    {

       isShooting = false;
       StartCoroutine(ReleaseNormalArrow());
    }
    private void OnChargeMaxed()
    {
        isShooting = false;
    }
  

    private void PlaceNewArrow()
    {
        UXArrow.localPosition = new Vector3(UXArrow.localPosition.x, UXArrow.localPosition.y, UXArrowStartingZ);
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
        if(QuickShotAbiliyRef.AbilityToggle)
        {
            QuickShot();
            return;
        }
        isShooting = true;
       

    }
    private void Shoot()
    {
        if (isShooting)
        {
            shootHoldTime += Time.deltaTime;
            float lerpT = shootHoldTime / maxHoldTime;
            UXArrow.localPosition = new Vector3(UXArrow.localPosition.x, UXArrow.localPosition.y, Mathf.Lerp(UXArrowStartingZ, UXArrowStartingZ + 0.1f, lerpT));
            bowString.SetBowStringPos(lerpT);
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
    void QuickShot()
    {
        shootHoldTime = maxHoldTime;
        StartCoroutine(ReleaseChainLightingArrow());
        QuickShotAbiliyRef.AbilityToggle = false;
        QuickShotAbiliyRef.ResetArrowToSpin();
        AbilityStackHandler.instance.DecreaseStackCount();
    }
    public IEnumerator ReleaseNormalArrow()
    {
        var arrow = NormalArrowPool.GetPooledObject();
        arrow.transform.position = UXArrow.position;
        arrow.transform.rotation = UXArrow.rotation;
        UXArrow.gameObject.SetActive(false);

        var arrowProj = arrow.GetComponent<ArrowProjectile>();
        arrowProj.direction = ShootDirection().normalized;
        arrowProj.force = arrowForce * shootHoldTime;
        arrowProj.appliedDamage = Mathf.RoundToInt(arrowProj.maxDamage * shootHoldTime);
        arrow.SetActive(true);
        shootHoldTime = 0;
        bowString.ResetBowStringPos();
        bowString.PlayStringVFX();
        yield return new WaitForSeconds(0.1f);
        PlaceNewArrow();

    }
    
    public IEnumerator ReleaseChainLightingArrow()
    {
        var arrow = QuickShotAbiliyRef.ChainLightingArrowPool.GetPooledObject();
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
        PlaceNewArrow();

    }
}
