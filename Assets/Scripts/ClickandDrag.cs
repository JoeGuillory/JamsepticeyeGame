using UnityEngine;
using UnityEngine.InputSystem;


public class ClickandDrag : MonoBehaviour
{
    InputAction Select;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Select = InputSystem.actions.FindAction("Select");
    }

    // Update is called once per frame
    void Update()
    {
       
        

    }
}
