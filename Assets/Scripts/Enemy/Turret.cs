using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform turretHead; //position component get like rotation/scale/position
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;
    PlayerHealth playerAlive;
    [SerializeField]float  turretFireRate = 3f;
    [SerializeField] int projectileDamage = 2;


    void Start()
    {
        playerAlive = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(TurretFireRoutine());
    }
    void Update()
    {
        turretHead.LookAt(playerTargetPoint);
    }

    IEnumerator TurretFireRoutine()
    {
        while(playerAlive)
        {
            
            yield return new WaitForSeconds(turretFireRate);
            ProjectileTurretBullet newProjectile = Instantiate(projectilePrefab,projectileSpawnPoint.position,Quaternion.identity).GetComponent<ProjectileTurretBullet>();
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(projectileDamage);
        }
        
    }
}
