using UnityEngine;

public class Ammopickup : BasePickup
{
    [SerializeField] int ammoAmount = 100;
    protected override void OnPickup(CurrentActiveWeapon activeWeapon)
    {
       activeWeapon.IncreaseDecreaseAmmo(ammoAmount);
    }

   
}
