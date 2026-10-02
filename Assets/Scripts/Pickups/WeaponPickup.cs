using UnityEngine;

public class WeaponPickup : BasePickup
{
  [SerializeField] WeaponSo weaponSo;

   //implement abstract class
    protected override void OnPickup(CurrentActiveWeapon activeWeapon)
    {
        activeWeapon.SwitchWeapon(weaponSo);
    }


   
}
