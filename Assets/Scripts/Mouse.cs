using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Mouse : MonoBehaviour
{
    InputActionAsset Controls;
    InputAction MouseAction;
    InputAction UseAction;
    InputAction SelectAction;
    InputAction DrinkAction;
    Selectable SelectableObject;
    [SerializeField] UnityEvent SelectedDeathPotion;
    [SerializeField] UnityEvent UnselectedDeathPotion;
    [SerializeField] UnityEvent DrinkDeathPotion;

    [SerializeField] UnityEvent Win;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Controls = GetComponent<PlayerInput>().actions;
        if(Controls)
        {
            MouseAction = Controls.FindAction("MousePosition");
            UseAction = Controls.FindAction("Use");
            SelectAction = Controls.FindAction("Select");
            DrinkAction = Controls.FindAction("Interact");


            MouseAction.performed += MoveMouse;
            UseAction.started += UseItem;

            SelectAction.canceled += ReleaseObject;
            DrinkAction.started += DrinkItem;
        }
    }


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SelectableObject != null)
            return;
        collision.gameObject.TryGetComponent<Selectable>(out SelectableObject);

        if(SelectableObject.TryGetComponent<Potion>(out Potion item))
        {
            if (item.Potionstatus == PotionType.Death || item.Potionstatus == PotionType.DarkIchor || item.Potionstatus == PotionType.Plague || item.Potionstatus == PotionType.DarkIchor || item.Potionstatus == PotionType.Mortality)
            {
                SelectedDeathPotion.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (SelectableObject.TryGetComponent<Potion>(out Potion item))
        {
            if (item.Potionstatus == PotionType.Death || item.Potionstatus == PotionType.DarkIchor || item.Potionstatus == PotionType.Plague || item.Potionstatus == PotionType.Sapphire || item.Potionstatus == PotionType.DarkIchor || item.Potionstatus == PotionType.Mortality)
            {
                UnselectedDeathPotion.Invoke();
            }
        }

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
    void SelectObject(InputAction.CallbackContext context)
    {
        if (!SelectableObject)
            return;
      
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

    void DrinkItem(InputAction.CallbackContext context)
    {
        if (!SelectableObject)
            return;

        if(SelectableObject.TryGetComponent<Potion>(out Potion item))
        {
            if(item.Potionstatus == PotionType.Death || item.Potionstatus == PotionType.DarkIchor || item.Potionstatus == PotionType.Plague || item.Potionstatus == PotionType.DarkIchor)
            {
                Destroy(item.gameObject);
                DrinkDeathPotion.Invoke();
            }
            else if(item.Potionstatus == PotionType.Mortality)
            {
                Win.Invoke();
            }

        }

    }
}
