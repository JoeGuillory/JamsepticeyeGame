using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class Selectable : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    public void Attach(Transform othertransform)
    {
        transform.SetParent(othertransform);
        spriteRenderer.sortingLayerName = "Selected";
    }


    public void Release()
    {
        transform.parent = null;
        spriteRenderer.sortingLayerName = "Unselected";
    }

}
