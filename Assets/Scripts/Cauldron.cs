using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cauldron : MonoBehaviour
{
    [SerializeField] GameObject[] PotionPrefabs;
    [SerializeField] Transform PotionSpawnPoint;
    [SerializeField] Transform Slot1Position;
    [SerializeField] Transform Slot2Position;
    [SerializeField] Transform Slot3Position;
    [SerializeField] GameObject IdleAnim;
    [SerializeField] GameObject PourAnim;
    [SerializeField] GameObject StirAnim;
    PotionType Slot1 = 0;
    GameObject Slot1ObjectTracker;
    PotionType Slot2 = 0;
    GameObject Slot2ObjectTracker;
    PotionType Slot3 = 0;
    GameObject Slot3ObjectTracker;
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
        {
            Slot1 = drip;
            Slot1ObjectTracker = Instantiate(PotionPrefabs[(int)drip], Slot1Position.position, Quaternion.identity);
            Slot1ObjectTracker.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Mouse";
            if (Slot1ObjectTracker.TryGetComponent<CircleCollider2D>(out CircleCollider2D CircleCollider2D))
            {
                CircleCollider2D.enabled = false;
            }
            if (Slot1ObjectTracker.TryGetComponent<CapsuleCollider2D>(out CapsuleCollider2D CapsuleCollider2D))
            {
                CapsuleCollider2D.enabled = false;
            }
            
        }
        else if(slot == 2)
        {
            Slot2 = drip;
            Slot2ObjectTracker = Instantiate(PotionPrefabs[(int)drip], Slot2Position.position, Quaternion.identity);
            Slot1ObjectTracker.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Mouse";
            if (Slot2ObjectTracker.TryGetComponent<CircleCollider2D>(out CircleCollider2D CircleCollider2D))
            {
                Slot2ObjectTracker.GetComponent<CircleCollider2D>().enabled = false;
            }
            if (Slot2ObjectTracker.TryGetComponent<CapsuleCollider2D>(out CapsuleCollider2D CapsuleCollider2D))
            {
                Slot2ObjectTracker.GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
        else if(slot == 3)
        {
            Slot3 = drip;
            Slot3ObjectTracker = Instantiate(PotionPrefabs[(int)drip], Slot3Position.position, Quaternion.identity);
            Slot1ObjectTracker.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Mouse";
            if (Slot3ObjectTracker.TryGetComponent<CircleCollider2D>(out CircleCollider2D CircleCollider2D))
            {
                Slot3ObjectTracker.GetComponent<CircleCollider2D>().enabled = false;
            }
            if (Slot3ObjectTracker.TryGetComponent<CapsuleCollider2D>(out CapsuleCollider2D CapsuleCollider2D))
            {
                Slot3ObjectTracker.GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
    }

    void ResetSlots()
    {
        Slot1 = 0;
        Slot2 = 0;
        Slot3 = 0;
        currentslot = 1;

        Destroy(Slot1ObjectTracker);
        Destroy(Slot2ObjectTracker);
        Destroy(Slot3ObjectTracker);
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
        IdleAnim.SetActive(false);
        PourAnim.SetActive(false);
        StirAnim.SetActive(true);

        Invoke("CreatePotion", 2.0f);
    }

    public void CreatePotion()
    {
        PotionType potion = GetRecipe(Slot1, Slot2, Slot3);
        if (potion != PotionType.Empty)
            Instantiate(PotionPrefabs[(int)potion], PotionSpawnPoint.position, Quaternion.identity);

        StirAnim.SetActive(false);
        IdleAnim.SetActive(true);

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
        { new RecipeKey(PotionType.Life, PotionType.Death, PotionType.Empty), PotionType.Mortality },
        {new RecipeKey(PotionType.Ruby, PotionType.Ruby,PotionType.Ruby), PotionType.Blood }
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
