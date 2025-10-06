using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Recipes : MonoBehaviour
{
    [Header("Recipes Menu")]
    [SerializeField] private GameObject _recipesMenu;

    [Header("Death Screen")]
    [SerializeField] private GameObject _deathMenu;

    public void DisplayRecipes()
    {
        _recipesMenu.SetActive(true);
    }

    public void ExitRecipes()
    {
        _recipesMenu.SetActive(false);
    }

    public void DisplayDeathScreen()
    {
        _deathMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        _deathMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
