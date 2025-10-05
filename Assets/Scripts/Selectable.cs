using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class Selectable : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void MoveSelected(Vector3 position)
    {
        transform.position = position;
        spriteRenderer.sortingLayerName = "Selected";
    }


    public void Release()
    {
        spriteRenderer.sortingLayerName = "Unselected";
    }

}
