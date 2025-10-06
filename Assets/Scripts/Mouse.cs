using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    InputActionAsset Controls;
    InputAction MouseAction;
    InputAction UseAction;
    InputAction SelectAction;
    InputAction DrinkAction;
    InputAction EscapeAction;
    Selectable SelectableObject;
    [SerializeField] UnityEvent SelectedDeathPotion;
    [SerializeField] UnityEvent UnselectedDeathPotion;
    [SerializeField] UnityEvent DrinkDeathPotion;
    [SerializeField] GameObject BloodPotion;
    //[SerializeField] Transform BloodSpawn;
    int Health = 5;

    [SerializeField] UnityEvent Win;

    [SerializeField] GameObject IdleAnim;
    [SerializeField] GameObject HappyAnim;
    [SerializeField] GameObject ScaredAnim;
    [SerializeField] GameObject SkeletonAnim;
    [SerializeField] GameObject PourAnim;
    [SerializeField] GameObject StirAnim;

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
            EscapeAction = Controls.FindAction("Escape");


            MouseAction.performed += MoveMouse;
            UseAction.started += UseItem;

            SelectAction.canceled += ReleaseObject;
            DrinkAction.started += DrinkItem;

            EscapeAction.performed += EscapeProgram;
        }
    }


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SelectableObject != null)
            return;
        collision.gameObject.TryGetComponent<Selectable>(out SelectableObject);

        if(SelectableObject.TryGetComponent<Potion>(out Potion item))
        {
            if (item.Potionstatus == PotionType.Death || item.Potionstatus == PotionType.DarkIchor || item.Potionstatus == PotionType.Plague || item.Potionstatus == PotionType.DarkIchor || item.Potionstatus == PotionType.Mortality || item.Potionstatus == PotionType.Empty)
            {
                SelectedDeathPotion.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (SelectableObject.TryGetComponent<Potion>(out Potion item))
        {
            if (item.Potionstatus == PotionType.Death || item.Potionstatus == PotionType.DarkIchor || item.Potionstatus == PotionType.Plague || item.Potionstatus == PotionType.DarkIchor || item.Potionstatus == PotionType.Mortality || item.Potionstatus == PotionType.Empty)
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

        if (Health <= 0)
        {
            DrinkDeathPotion.Invoke();
            IdleAnim.SetActive(false);
            SkeletonAnim.SetActive(true);
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
            IdleAnim.SetActive(false);
            PourAnim.SetActive(true);
            Invoke("BackToIdle", 1.0f);
        }
        else if(SelectableObject.TryGetComponent<Item>(out Item item))
        {

        }
    }

    void BackToIdle()
    {
        if (PourAnim.activeInHierarchy)
        {
            IdleAnim.SetActive(true);
            PourAnim.SetActive(false);
        }

        //HappyAnim.SetActive(false);
        //SkeletonAnim.SetActive(false);
        //ScaredAnim.SetActive(false);
        //StirAnim.SetActive(false);
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
                IdleAnim.SetActive(false);
                SkeletonAnim.SetActive(true);
            }
            if(item.Potionstatus == PotionType.Empty)
            {
                //BloodSpawn.position = new Vector3(0, 0, 0);
                Instantiate(BloodPotion, new Vector3(0,0,0), Quaternion.identity);
                Health--;
            }
            else if(item.Potionstatus == PotionType.Mortality)
            {
                Win.Invoke();
            }

        }

    }

    void EscapeProgram(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
