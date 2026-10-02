using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject youWinTextGameobject;
    int enemiesLeft = 0;
    const string ENEMIES_LEFT_STRING = "Enemies Left: ";

 public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft+= amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();
        if(enemiesLeft<=0)
        {
            //you win
            youWinTextGameobject.SetActive(true);
        }
    }
   public void RestartButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
         Debug.Log("Game has Quit but unity Editor Doesnot work!");
        Application.Quit();
       
    }

}
