using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitandNextLevel : MonoBehaviour
{
    

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void LoadLevelWithSoul()
    {

        SceneManager.LoadScene("MainLevelWithSoul");
    }
    
}
