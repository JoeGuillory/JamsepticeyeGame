using System;
using System.Collections.Generic;
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
        if (potion != PotionType.Empty)
            Instantiate(PotionPrefabs[(int)potion], PotionSpawnPoint.position, Quaternion.identity);

        ResetSlots();
    }

    Dictionary<RecipeKey, PotionType> recipes = new Dictionary<RecipeKey, PotionType>
    {
        //Ruby + Emerald + Sapphire = Celestial
        { new RecipeKey(PotionType.Ruby, PotionType.Emerald, PotionType.Sapphire), PotionType.Celestial }, 
        // Celestial + Ruby = Sun
        { new RecipeKey(PotionType.Celestial, PotionType.Ruby, PotionType.Empty), PotionType.Sun },
        // Celestial + Sapphire = Moon
        { new RecipeKey(PotionType.Celestial, PotionType.Sapphire, PotionType.Empty), PotionType.Moon },
        // Ruby + Sapphire = Nectar
        { new RecipeKey(PotionType.Ruby, PotionType.Sapphire, PotionType.Empty), PotionType.Nectar },
        // Blood + Nectar = Ichor
        { new RecipeKey(PotionType.Blood, PotionType.Nectar, PotionType.Empty), PotionType.Ichor },
        // Emerald + Sapphire = Blessing
        { new RecipeKey(PotionType.Emerald, PotionType.Sapphire, PotionType.Empty), PotionType.Blessing },
        // Ruby + Emerald = Plague
        { new RecipeKey(PotionType.Ruby, PotionType.Emerald, PotionType.Empty), PotionType.Plague },
        // Blessing + Ichor = HolyIchor
        { new RecipeKey(PotionType.Blessing, PotionType.Ichor, PotionType.Empty), PotionType.HolyIchor }, 
        // Plague + Ichor = DarkIchor
        { new RecipeKey(PotionType.Plague, PotionType.Ichor, PotionType.Empty), PotionType.DarkIchor }, 
        // Sun + HolyIchor + Soul = Life
        { new RecipeKey(PotionType.Sun, PotionType.HolyIchor, PotionType.Soul), PotionType.Life }, 
        // Moon + DarkIchor + Soul = Death
        { new RecipeKey(PotionType.Moon, PotionType.DarkIchor, PotionType.Soul), PotionType.Death }, 
        // Life + Death = Mortality
        { new RecipeKey(PotionType.Life, PotionType.Death, PotionType.Empty), PotionType.Mortality }
    // Add more recipes here
    };


    PotionType GetRecipe(PotionType first , PotionType second, PotionType third)
    {
        var key = new RecipeKey(first, second, third);
        if (recipes.TryGetValue(key, out var result))
        {
            return result;
        }
        return PotionType.Empty;
    }
}
