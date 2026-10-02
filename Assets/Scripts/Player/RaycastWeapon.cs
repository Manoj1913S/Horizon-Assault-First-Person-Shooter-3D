
using System;
using Unity.Mathematics;
using UnityEngine;


public class RaycastWeapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interatcionLayers;
     private CameraShake cameraShake;




    private void Awake()
    {
        cameraShake = FindFirstObjectByType<CameraShake>();
    }

    public void Shoot_DamageLogic(WeaponSo weaponSo)//public reason has current active gun jun chha tyo raycast+fire gara bhanera currentActiveWeapon.cs bata bhannu 
    {

        cameraShake.Shake(0.1f, 0.05f);
        muzzleFlash.Play();
        // StartCoroutine(CameraShake(0.1f, 0.05f));
        //current active ma Jun sukai gun bhaye pani tes bata raycast niskinu paryo ra fire hunu paryo so tesaile yo yeha chha
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interatcionLayers, QueryTriggerInteraction.Ignore))
        {

            Instantiate(weaponSo.HitParticlePrefab, hit.point, Quaternion.identity);

            EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSo.Damage);//it apply which scriptable object(pistol,sniper rifle and machine gun script drag and drop into this)
        }
    }
}

