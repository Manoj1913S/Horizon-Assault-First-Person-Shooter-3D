using System;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    int damage = 3;

  

    void Start()
    {
        Explode();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position ,radius);
    }

   public  void Explode()
    {
        //to damage player when enemy explode 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position,radius);

        foreach (Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth  = hitCollider.GetComponentInParent<PlayerHealth>();//player health script get gariyo 

            //NOTE:Step 1 
            // if(playerHealth)
            // {
            //     playerHealth.TakeDamage(damage); //player healt script ko take damage call 
            // }
            //NOTE:Step 2:use null operator
            // playerHealth?.TakeDamage(damage);

            //NOTE:Step 3:Use not+Continue
            if(!playerHealth) continue;

            playerHealth.TakeDamage(damage);

            break;


            
        }
    }
}