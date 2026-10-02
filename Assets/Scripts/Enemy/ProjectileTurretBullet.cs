
using UnityEngine;

public class ProjectileTurretBullet : MonoBehaviour
{
   // Move the turret projectile towards the player using a Rigidbody.
   //1.projectile prefab get 
   //2.rigidbody component add into turret prefab and get this property into script 
   //3.use RigidBody.LInearVelocity(Vector3)
   //4.Fire or get speed into Update() beacuse it fire every frame 
   //5.Destroy projectile when collide into environmemt we can use tag or layer

  // [SerializeField] GameObject projectilePrefab;
   [SerializeField] Rigidbody projectileRigidbody;
   [SerializeField] float projectileSpeed =30f;
   [SerializeField] ParticleSystem turretProjectileHit;



   int projectileDamage;



    void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        projectileRigidbody.linearVelocity = transform.forward*projectileSpeed;
    }

    //new way to pass variable through method
    public void Init(int projectileDamage) //this method fired from turret class 
    {
        this.projectileDamage = projectileDamage; 
    }
    void OnTriggerEnter(Collider other)
    {
        //check if we hit player health component or not script check 
       PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

       //if checked gameobject contain playerhealth component or script then do damage 
       playerHealth?.TakeDamage(projectileDamage);
       Instantiate(turretProjectileHit,transform.position,Quaternion.identity);
       Destroy(this.gameObject);
    }

}
