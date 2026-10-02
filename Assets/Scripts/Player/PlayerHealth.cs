//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [Range(1,10)]
     [SerializeField] int startHealth = 10;
    [SerializeField] int currentHealth;
    [SerializeField] Image[]  healthBars;//we use array of all those images specially the health bar
    [SerializeField] GameObject gameOverContainer;


    void Awake()
    {
        currentHealth = startHealth;
        AdjustHealthUI();
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        AdjustHealthUI();
        Debug.Log(amount + " Damage Taken");
        if (currentHealth <= 0)
        {
            gameOverContainer.SetActive(true);  //playerGameover
            FPSMovement fPSMovement = FindFirstObjectByType<FPSMovement>();
            fPSMovement.SetCurserState(false); //gameover curser has doesnot show
            Destroy(this.gameObject);  //player destroyed
            
        }
    }
    void AdjustHealthUI()
    {
        for (int i = 0; i < healthBars.Length; i++)
        {
            if(i<currentHealth)
            {
                healthBars[i].gameObject.SetActive(true);
            }
            else
            {
                healthBars[i].gameObject.SetActive(false); //code through on-off gareko healthbargameobject lai 
            }
        }

    }
}
