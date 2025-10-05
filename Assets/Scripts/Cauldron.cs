using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cauldron : MonoBehaviour
{
    [SerializeField] GameObject[] PotionPrefabs;
    [SerializeField] Transform PotionSpawnPoint;
    PotionType Slot1 = 0;
    PotionType Slot2 = 0;
    PotionType Slot3 = 0;
    int currentslot = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Droplet>(out Droplet droplet))
        {
            AddDrip(droplet.DripType);
            Destroy(droplet.gameObject);
        }
        else if(collision.TryGetComponent<Item>(out Item item))
        {
            AddDrip(item.ItemType);
            Destroy(item.gameObject);
        }

        if (currentslot == 4)
            MakePotion();
    }

    void SetSlot(int slot, PotionType drip)
    {
        if (slot == 1)
            Slot1 = drip;
        else if(slot == 2)
            Slot2 = drip;
        else if(slot == 3)
            Slot3 = drip;
    }

    void ResetSlots()
    {
        Slot1 = 0;
        Slot2 = 0;
        Slot3 = 0;
        currentslot = 1;
    }
    void AddDrip(PotionType drip)
    {
        if (currentslot == 4)
            return;
        SetSlot(currentslot,drip);
        currentslot++; 
    }

    public void MakePotion()
    {
        PotionType potion = GetRecipe(Slot1, Slot2, Slot3);
        if(potion != PotionType.Empty)
            Instantiate(PotionPrefabs[(int)potion], PotionSpawnPoint.position, Quaternion.identity);

        ResetSlots();
    }

    PotionType GetRecipe(PotionType first , PotionType second, PotionType third)
    {
        if(first == PotionType.Emerald && second == PotionType.Emerald && third == PotionType.Emerald)
        {
            return PotionType.Sapphire;
        }







        return PotionType.Empty;
    }

}
