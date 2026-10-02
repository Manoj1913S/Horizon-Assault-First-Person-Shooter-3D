using UnityEngine;

[CreateAssetMenu(fileName ="weaponSo",menuName ="Scriptable Objects/WeaponSo")]
public class WeaponSo : ScriptableObject 
{
  public GameObject weaponPrefab;
  public  int Damage = 2;
  public float FireRate = 0.5f;
  public GameObject HitParticlePrefab;
  public bool IsAutomatic = false;
  public bool CanZoom = false;
  public float ZoomAmount = 10f;
  public float ZoomRotationSpeed = 0.05f;
  public int  MagazineSize = 12;

  //Audio
  public AudioClip ShootSound;
 

   
}
