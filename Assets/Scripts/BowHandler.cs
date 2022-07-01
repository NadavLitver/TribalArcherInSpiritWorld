using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class BowHandler : MonoBehaviour
{
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private InputManager input;
    [SerializeField, FoldoutGroup("Refrences")] private BowString bowString;
    [SerializeField, FoldoutGroup("Refrences")] private QuickStunAbility QuickShotAbiliyRef;
    [SerializeField, FoldoutGroup("Refrences")] private ScatterArrowAbility ScatterArrowAbilityRef;
    [SerializeField, FoldoutGroup("Refrences")] private LightingBoltAOEAbility LightingBoltAbility;
    [SerializeField, FoldoutGroup("Refrences")] private Animator m_animator;
    [SerializeField, FoldoutGroup("Refrences")] private ObjectPool NormalArrowPool;
    [SerializeField, FoldoutGroup("Refrences")] private AudioSource m_audioSource;
    [SerializeField, FoldoutGroup("Refrences")] private Transform ArrowPos;
    [SerializeField, FoldoutGroup("Refrences"), ReadOnly] private Camera cam;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] public bool isShooting;
    [SerializeField, FoldoutGroup("Properties"), ReadOnly] public bool isLoaded;

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
        isLoaded = true;
        //input.OnPlayerFinishCharge.AddListener(OnChargeMaxed);
        maxHoldTime = input.shootHoldTime;
       

    }
    private void Update()
    {
        SetUXArrowPos();

    }

    private void OnRelease()
    {

        if (isShooting && isLoaded)
        {
            float soundModifier = Mathf.Clamp(shootHoldTime, 0.3f, 0.7f);
            SoundManager.Play(SoundManager.Sound.BowReleaseFull, transform.position,soundModifier);
            PostProccessManipulator.ResetLensDistortion();
            m_audioSource.Stop();
            if (LightingBoltAbility.AbilityToggle)
            {
                StartCoroutine(ReleaseLightingArrow());
                return;
            }
            if (ScatterArrowAbilityRef.AbilityToggle)
            {
                StartCoroutine(ReleaseScatterArrow());
            }
            else
            {
                StartCoroutine(ReleaseNormalArrow());
            }
          

        }


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
        if (isLoaded)
        {
            if (QuickShotAbiliyRef.AbilityToggle)
            {
                QuickShot();
               
                SoundManager.Play(SoundManager.Sound.ElectricShotRelease, m_audioSource, 0.25f);
                return;
            }
            if (ScatterArrowAbilityRef.AbilityToggle)
            {
                m_animator.Play("ScatterPull");

            }
            else
            {
             m_animator.Play("Pull");

            }
         

            PostProccessManipulator.LensDistortionOnShoot();
            isShooting = true;
            m_audioSource.Play();
         //   StartCoroutine(shootCD());
        }
       

    }
    private void SetUXArrowPos()
    {
        if (isShooting && shootHoldTime < maxHoldTime)
        {
            shootHoldTime += Time.deltaTime;
            float lerpT = shootHoldTime / maxHoldTime;
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
        QuickShotAbiliyRef.ToggleAbility();
        AbilityStackHandler.instance.DecreaseStackCount(QuickShotAbiliyRef.stackCost);
    }
    public IEnumerator ReleaseNormalArrow()
    {
        
        yield return new WaitUntil(() => shootHoldTime > 0.2f);
        m_animator.Play("Release");
        var arrow = NormalArrowPool.GetPooledObject();
        arrow.transform.SetPositionAndRotation(ArrowPos.position, ArrowPos.rotation);
        var arrowProj = arrow.GetComponent<ArrowProjectile>();
        arrowProj.direction = ShootDirection().normalized;
        arrowProj.force = arrowForce * shootHoldTime;
        arrowProj.appliedDamage = Mathf.RoundToInt(GetCurrentDamage(arrowProj));
        shootHoldTime = 0;
        bowString.ResetBowStringPos();
        bowString.PlayStringVFX();
        CinemachineCameraShaker.instance.ShakeCamera(0.1f, 6f, 0.1f);
        isShooting = false;
        
        arrow.SetActive(true);
        yield return new WaitForSeconds(0.05f);

    }
    public IEnumerator ReleaseLightingArrow()
    {

        var arrow = LightingBoltAbility.LightingArrowPool.GetPooledObject();
        var arrowProj = arrow.GetComponent<ArrowProjectile>();
        arrow.transform.SetPositionAndRotation(ArrowPos.position, ArrowPos.rotation);
        arrowProj.direction = ShootDirection().normalized;
        arrowProj.force = arrowForce * shootHoldTime;
        arrowProj.appliedDamage = Mathf.RoundToInt(GetCurrentDamage(arrowProj));   
        shootHoldTime = 0;
        bowString.ResetBowStringPos();
        bowString.PlayStringVFX();
        CinemachineCameraShaker.instance.ShakeCamera(0.1f, 6f, 0.1f);
        AbilityStackHandler.instance.DecreaseStackCount(LightingBoltAbility.stackCost);
        arrow.SetActive(true);
        LightingBoltAbility.ToggleAbility();
        isShooting = false;
        yield return new WaitForSeconds(0.05f);


    }
    public IEnumerator ReleaseScatterArrow()
    {
        float arrowDirectionChanger =0;
       
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
            arrow.transform.SetPositionAndRotation(ArrowPos.position, ArrowPos.rotation);
            arrowProj.direction = dir +(Vector3.Cross(Vector3.up,dir) * arrowDirectionChanger);
            arrowProj.force = arrowForce * shootHoldTime;
            arrowProj.appliedDamage = Mathf.RoundToInt(GetCurrentDamage(arrowProj));
           
            arrow.SetActive(true);
        }
        shootHoldTime = 0;
        bowString.ResetBowStringPos();
        bowString.PlayStringVFX();
        CinemachineCameraShaker.instance.ShakeCamera(0.1f, 6f, 0.1f);
        AbilityStackHandler.instance.DecreaseStackCount();
        ScatterArrowAbilityRef.ToggleAbility();
        m_animator.Play("ScatterRelease");
        isShooting = false;
        yield return new WaitForSeconds(0.05f);

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
       
        var arrow = QuickShotAbiliyRef.ChainLightingArrowPool.GetPooledObject();
        arrow.transform.SetPositionAndRotation(ArrowPos.position, ArrowPos.rotation);
        var arrowProj = arrow.GetComponent<ArrowProjectile>();
        arrowProj.direction = ShootDirection().normalized;
        arrowProj.force = arrowForce * shootHoldTime;
        arrowProj.appliedDamage = Mathf.RoundToInt(GetCurrentDamage(arrowProj));
        shootHoldTime = 0;
        arrow.SetActive(true);
        CinemachineCameraShaker.instance.ShakeCamera(0.1f, 5f, 0.1f);
        yield return new WaitForSeconds(0.05f);

    }
    //IEnumerator shootCD()
    //{
    //    if (canShoot == false)
    //    {
    //        yield break;
    //    }
    //    canShoot = false;
    //    yield return new WaitForSeconds(0.5f);
    //    canShoot = true;
    //}
} 

