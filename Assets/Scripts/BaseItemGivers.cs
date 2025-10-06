using Unity.VisualScripting;
using UnityEngine;

public class BaseItemGivers : MonoBehaviour
{
    [SerializeField] PotionType CurrentItemType;
    [SerializeField] Item ItemPrefab;
    GameObject trackedItem;

    public PotionType ItemType { get { return CurrentItemType; } set { CurrentItemType = value; } }

    private void Start()
    {
        SpawnItem(transform.position);
    }

    public void SpawnItem(Vector3 position)
    {
        Item newItem = Instantiate(ItemPrefab, position, Quaternion.identity);
        newItem.ChangeItemType(CurrentItemType);
        trackedItem = newItem.gameObject;
    }

    private void FixedUpdate()
    {
        if (trackedItem == null)
        {
            SpawnItem(transform.position);
        }
    }

}
