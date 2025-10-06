
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;

    private Button startbutton;
    private Button exitbutton;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        startbutton = document.rootVisualElement.Q("StartButton") as Button;
        exitbutton = document.rootVisualElement.Q("ExitButton") as Button;
        startbutton.RegisterCallback<ClickEvent>(OnPlayGameClick);
        exitbutton.RegisterCallback<ClickEvent>(OnExitClicked);

    }

    private void OnExitClicked(ClickEvent evt)
    {
       Application.Quit();
    }
    private void OnPlayGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene("MainLevel");
        


    }
}
