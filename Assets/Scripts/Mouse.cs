using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    InputActionAsset Controls;
    InputAction MouseAction;
    InputAction UseAction;
    InputAction SelectAction;
    Selectable SelectableObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Controls = GetComponent<PlayerInput>().actions;
        if(Controls)
        {
            MouseAction = Controls.FindAction("MousePosition");
            UseAction = Controls.FindAction("Use");
            SelectAction = Controls.FindAction("Select");

            MouseAction.performed += MoveMouse;
            UseAction.started += UseItem;
            SelectAction.canceled += ReleaseObject;         
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SelectableObject != null)
            return;
        collision.gameObject.TryGetComponent<Selectable>(out SelectableObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if(SelectableObject.GameObject() == collision.gameObject)
       {
            SelectableObject = null;
       }
    }

    // Update is called once per frame
    void Update()
    {
        if(SelectAction.triggered)
        {
            if (!SelectableObject)
                return;
            SelectableObject.GrabItem(transform.position);
            SelectableObject.MakeAPotion();
        }

        if(SelectAction.inProgress)
        {
            if (!SelectableObject)
                return;
            SelectableObject.MoveSelected(transform.position);
        }
    }

    void ReleaseObject(InputAction.CallbackContext context)
    {
        if (!SelectableObject)
            return;
        SelectableObject.Release();
    }

    void MoveMouse(InputAction.CallbackContext context)
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        transform.position = position;
    }

    void UseItem(InputAction.CallbackContext context)
    {
        if (!SelectableObject)
            return;

        if(SelectableObject.TryGetComponent<Potion>(out Potion potion))
        {
            potion.PourPotion();
        }
        else if(SelectableObject.TryGetComponent<Item>(out Item item))
        {

        }
    }
}
