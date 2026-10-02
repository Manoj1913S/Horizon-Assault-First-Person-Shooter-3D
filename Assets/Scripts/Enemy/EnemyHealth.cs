using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionParticle;
    [SerializeField] int startHealth = 3;
    [SerializeField] int currentHealth;

    GameManager gameManager; //how many enemies left show ko lagi get gareko 


   
    void Awake()
    {
        currentHealth = startHealth;
    }

     void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjustEnemiesLeft(1);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            gameManager.AdjustEnemiesLeft(-1);
            SelfDestroy();
        }
    }
    public void SelfDestroy()
    {
        Instantiate(robotExplosionParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject); //jun game object ma yo script attached gareko ho tyo destroy gara 
    }
}
