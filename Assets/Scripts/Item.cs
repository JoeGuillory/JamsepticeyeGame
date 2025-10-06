using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    [SerializeField] PotionType CurrentItemType;
    Selectable isSelectable;
    public PotionType ItemType { get { return CurrentItemType; } set { CurrentItemType = value; } }

    private void Start()
    {
        //GameObject mouse = GameObject.Find("Mouse");
        //transform.position = mouse.transform.position;
        //isSelectable = GetComponentInChildren<Selectable>();
        //isSelectable.MoveSelected(mouse.transform.position);
    }

    public void ChangeItemType(PotionType newItemStatus)
    {
        ItemType = newItemStatus;
        return;
    }
    public void ChangeItemType(int newItemStatus)
    {
        ItemType = (PotionType)(newItemStatus);
        return;
    }

    private void FixedUpdate()
    {

    }
}