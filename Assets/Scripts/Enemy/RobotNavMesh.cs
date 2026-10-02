using NUnit;
using UnityEngine;
using UnityEngine.AI;

public class RobotNavMesh : MonoBehaviour
{
    //INFO: We want to move Robot From Point A to point B Using NavMesh Agent so
    // [SerializeField] Transform target; //target assign fro robot movement(cylinder add garera target banayeko chha haii)
    FPSMovement player;
    NavMeshAgent agentRobotMove;

    const string PLAYER_STRING = "Player";

    void Awake()
    {
        agentRobotMove = GetComponent<NavMeshAgent>();

    }

    void Start()
    {
        player = FindFirstObjectByType<FPSMovement>();
        //Set destination for our agent to move using built in unity Navmesh unity Method called SetDestination

    }


    void Update()
    {
        if(!player) return;
        agentRobotMove.SetDestination(player.transform.position);
    }

  void OnTriggerEnter(Collider other)
{
     if(other.CompareTag(PLAYER_STRING))
        {
             Debug.Log("PLAYER DETECTED!");
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
          
            //ADDED code 

            
            enemyHealth.SelfDestroy();
        }

    }

}
