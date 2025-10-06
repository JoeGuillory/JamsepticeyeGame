using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.UIElements;

public class Selectable : MonoBehaviour
{
    [SerializeField] bool moveable = true;

    SpriteRenderer spriteRenderer;
    BaseItemGivers ItemGiver;
    Cauldron TheCauldron;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        ItemGiver = GetComponentInChildren<BaseItemGivers>();
        TheCauldron = GetComponentInChildren<Cauldron>();
    }

    public void MakeAPotion()
    {
        if (TheCauldron != null)
            TheCauldron.MakePotion();
    }

    public void GrabItem(Vector3 position)
    {
        if (ItemGiver != null)
            ItemGiver.SpawnItem(position);
    }

    public void MoveSelected(Vector3 position)
    {
        if (moveable)
        {
            transform.position = position;
            spriteRenderer.sortingLayerName = "Selected";
        }   
    }

    public void Release()
    {
        spriteRenderer.sortingLayerName = "Unselected";
        if (this.GetComponent<Item>() != null)
        {
            Destroy(this.gameObject);
        }
    }

    public void MakeSelected(Vector3 position)
    {
        transform.position = position;
        spriteRenderer.sortingLayerName = "Selected";
    }

}
