using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CurrentActiveWeapon : MonoBehaviour
{

    [SerializeField] WeaponSo startigWeapon;

    [SerializeField] Camera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject ZoomGunScope;
    [SerializeField] TMP_Text ammoCountText;
    [SerializeField] AudioSource audioSource;


    WeaponSo currentWeaponSo; //access scriptableobject 
    Animator animatorGun;
    FPSMovement firstPersonController; //starterassetsinput
    RaycastWeapon currentActiveWeapon;

    const string Shoot_Gun = "Shoot";

    float ammoCooldowntime = 0f;
    float defaultFOV;
    float defaultRotationSpeed; //scope on rotation
    int currentAmmo; //track current amount of ammo

    void Awake()
    {
        firstPersonController = GetComponentInParent<FPSMovement>();
        animatorGun = GetComponent<Animator>();
        defaultFOV = playerFollowCamera.fieldOfView;
        defaultRotationSpeed = firstPersonController.mouseSensitivity;


    }
    void Start()
    {
        SwitchWeapon(startigWeapon);
        IncreaseDecreaseAmmo(currentWeaponSo.MagazineSize);
        currentActiveWeapon = GetComponentInChildren<RaycastWeapon>();

    }

    void Update()
    {

        Handle_Shoot_DamageLogic();
        ZoomInOutGunScope();
    }

    public void IncreaseDecreaseAmmo(int amount) //adjustammo
    {
        currentAmmo += amount;
        if (currentAmmo > currentWeaponSo.MagazineSize)
        {
            currentAmmo = currentWeaponSo.MagazineSize;
        }
        ammoCountText.text = currentAmmo.ToString("D2");
    }
    void Handle_Shoot_DamageLogic()
    {
        ammoCooldowntime += Time.deltaTime;
        if (!firstPersonController.isShoot) return;

        if (ammoCooldowntime >= currentWeaponSo.FireRate && currentAmmo > 0)
        {
            currentActiveWeapon.Shoot_DamageLogic(currentWeaponSo);
            animatorGun.Play(Shoot_Gun, 0, 0f);
            // Gun sound
            if (currentWeaponSo.ShootSound != null)
            {
                audioSource.PlayOneShot(currentWeaponSo.ShootSound);
            }

            ammoCooldowntime = 0f;//set back to zero
            IncreaseDecreaseAmmo(-1);
        }

        if (!currentWeaponSo.IsAutomatic)
        {
            firstPersonController.isShoot = false;
        }


    }
    public void SwitchWeapon(WeaponSo weaponSo)
    {
        if (currentActiveWeapon) //destroy current weapon and instantiate new weapon gameobject
        {
            Destroy(currentActiveWeapon.gameObject);
        }
        RaycastWeapon newSwitchedWeapon = Instantiate(weaponSo.weaponPrefab, transform).GetComponent<RaycastWeapon>();




        currentActiveWeapon = newSwitchedWeapon;
        this.currentWeaponSo = weaponSo;
        IncreaseDecreaseAmmo(currentWeaponSo.MagazineSize);
        Debug.Log("you Pickup Weapon Sucessfully" + weaponSo.name);
    }



    public void ZoomInOutGunScope()
    {
        if (!currentWeaponSo.CanZoom) return;

        if (firstPersonController.zoom)
        {
            playerFollowCamera.fieldOfView = currentWeaponSo.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSo.ZoomAmount;
            ZoomGunScope.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSo.ZoomRotationSpeed);

            Debug.Log("Sniper has Zoom");
        }
        else
        {
            playerFollowCamera.fieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            ZoomGunScope.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
            Debug.Log("Not zooming in");
        }
    }
}
