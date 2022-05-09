using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class BowHandler : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private InputManager input;
    [SerializeField, FoldoutGroup("Refrences")] private BowString bowString;
    [SerializeField, FoldoutGroup("Refrences")] private Transform UXArrow;
    [SerializeField, FoldoutGroup("Refrences")] private ChainLightingShot QuickShotAbiliyRef;
    [SerializeField, FoldoutGroup("Refrences")] private ScatterArrowAbility ScatterArrowAbilityRef;
    [SerializeField, FoldoutGroup("Refrences")] private ObjectPool NormalArrowPool;
    [SerializeField, FoldoutGroup("Refrences")] private AudioSource m_audioSource;

    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private Camera cam;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] public bool isShooting;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] private float UXArrowStartingZ;
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
        //input.OnPlayerFinishCharge.AddListener(OnChargeMaxed);
        maxHoldTime = input.shootHoldTime;
        UXArrowStartingZ = UXArrow.localPosition.z;

    }
    private void Update()
    {
        SetUXArrowPos();

    }

    private void OnRelease()
    {

        if (isShooting)
        {
            SoundManager.Play(SoundManager.Sound.BowReleaseFull, transform.position, 0.5f);
            isShooting = false;
            PostProccessManipulator.ResetLensDistortion();
            if (ScatterArrowAbilityRef.AbilityToggle)
            {
                StartCoroutine(ReleaseScatterArrow());
            }
            else
            {
                StartCoroutine(ReleaseNormalArrow());
            }
            m_audioSource.Stop();
        }

      
    }
    


    private void PlaceNewArrow()
    {
        UXArrow.localPosition = new Vector3(UXArrow.localPosition.x, UXArrow.localPosition.y, UXArrowStartingZ);
        UXArrow.gameObject.SetActive(true);
    }

    private Vector3 ShootDirection()
    {

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }
        Vector3 direction = (targetPoint - transform.position);
        return direction;
    }

    private void OnShoot()
    {
        if (QuickShotAbiliyRef.AbilityToggle)
        {
            QuickShot();
            SoundManager.Play(SoundManager.Sound.ElectricShotRelease, m_audioSource, 0.25f);
            return;
        }
        PostProccessManipulator.LensDistortionOnShoot();
        isShooting = true;
        m_audioSource.Play();

    }
    private void SetUXArrowPos()
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

  
    void QuickShot()
    {
        shootHoldTime = maxHoldTime - 0.1f;
        StartCoroutine(ReleaseChainLightingArrow());
        QuickShotAbiliyRef.AbilityToggle = false;
        QuickShotAbiliyRef.ResetArrowToSpin();
        AbilityStackHandler.instance.DecreaseStackCount(QuickShotAbiliyRef.stackCost);
    }
    public IEnumerator ReleaseNormalArrow()
    {
        
        var arrow = NormalArrowPool.GetPooledObject();
        var arrowProj = arrow.GetComponent<ArrowProjectile>();
        arrowProj.direction = ShootDirection().normalized;
        arrowProj.force = arrowForce * shootHoldTime;
        arrowProj.appliedDamage = Mathf.RoundToInt(GetCurrentDamage(arrowProj));
        arrow.transform.position = UXArrow.position;
        UXArrow.gameObject.SetActive(false);
        shootHoldTime = 0;
        bowString.ResetBowStringPos();
        bowString.PlayStringVFX();
        CinemachineCameraShaker.instance.ShakeCamera(0.1f, 6f, 0.1f);
        arrow.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        PlaceNewArrow();

    }
    public IEnumerator ReleaseScatterArrow()
    {
        float arrowDirectionChanger =0;
        UXArrow.gameObject.SetActive(false);
        Vector3 dir = ShootDirection().normalized;
        for (int i = 0; i < 3; i++)
        {
            if(i == 1)
            {
                arrowDirectionChanger = 0.1f;
            }
            else if(i == 2)
            {
                arrowDirectionChanger = -0.1f;
            }
            var arrow = NormalArrowPool.GetPooledObject();
            var arrowProj = arrow.GetComponent<ArrowProjectile>();
            arrowProj.direction = dir +(Vector3.Cross(Vector3.up,dir) * arrowDirectionChanger);
            arrowProj.force = arrowForce * shootHoldTime;
            arrowProj.appliedDamage = Mathf.RoundToInt(GetCurrentDamage(arrowProj));
            arrow.transform.position = UXArrow.position;
            arrow.SetActive(true);
        }
        shootHoldTime = 0;
        bowString.ResetBowStringPos();
        bowString.PlayStringVFX();
        CinemachineCameraShaker.instance.ShakeCamera(0.1f, 6f, 0.1f);
        AbilityStackHandler.instance.DecreaseStackCount();
        ScatterArrowAbilityRef.ToggleAbility();
       
        yield return new WaitForSeconds(0.05f);
        PlaceNewArrow();

    }
    public float GetCurrentDamage(ArrowProjectile currentArrow)
    {
        float holdPercent = ((shootHoldTime / maxHoldTime) * 100f);
        float ratio = ((currentArrow.maxDamageBody - currentArrow.minDamageBody) / 100f);
        float val = (holdPercent * ratio) + currentArrow.minDamageBody;
        return val;
    }

    public IEnumerator ReleaseChainLightingArrow()
    {
        UXArrow.gameObject.SetActive(false);
        var arrow = QuickShotAbiliyRef.ChainLightingArrowPool.GetPooledObject();
        arrow.transform.SetPositionAndRotation(UXArrow.position, UXArrow.rotation);
        var arrowProj = arrow.GetComponent<ArrowProjectile>();
        arrowProj.direction = ShootDirection().normalized;
        arrowProj.force = arrowForce * shootHoldTime;
        arrowProj.appliedDamage = Mathf.RoundToInt(GetCurrentDamage(arrowProj));
        shootHoldTime = 0;
        arrow.SetActive(true);
        CinemachineCameraShaker.instance.ShakeCamera(0.1f, 5f, 0.1f);
        yield return new WaitForSeconds(0.05f);
        PlaceNewArrow();

    }
} 

