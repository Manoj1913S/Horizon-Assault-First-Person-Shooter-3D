using System.Collections;
using UnityEngine;

public class EnemySpawnGate : MonoBehaviour
{
   [SerializeField] GameObject robotPrefab;
   [SerializeField] float enemySpawnTime = 5f;
   [SerializeField] Transform spawnPoint;

   PlayerHealth player;


    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(EnemySpawnRoutine());
    }
    IEnumerator EnemySpawnRoutine()
    {
        //instantiate enemy only our player alive
        while(player)
        {
            Instantiate(robotPrefab, spawnPoint.position, transform.rotation);
        yield return new WaitForSeconds(enemySpawnTime);

        }
        

    }
}
