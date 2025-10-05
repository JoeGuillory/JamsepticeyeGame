using Unity.VisualScripting;
using UnityEngine;

public class BaseItemGivers : MonoBehaviour
{
    [SerializeField] PotionType CurrentItemType;
    [SerializeField] Item ItemPrefab;

    public PotionType ItemType { get { return CurrentItemType; } set { CurrentItemType = value; } }

    public void SpawnItem(Vector3 position)
    {
        Item newItem = Instantiate(ItemPrefab, position, Quaternion.identity);
        newItem.ChangeItemType(CurrentItemType);
    }

    private void FixedUpdate()
    {

    }

}
