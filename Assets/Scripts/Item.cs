using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] PotionType CurrentItemType;

    public PotionType ItemType { get { return CurrentItemType; } set { CurrentItemType = value; } }

}
