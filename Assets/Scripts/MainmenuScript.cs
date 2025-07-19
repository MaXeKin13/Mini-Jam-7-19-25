using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuScript : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void Authors()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
