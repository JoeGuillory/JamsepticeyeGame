using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cauldron : MonoBehaviour
{
    [SerializeField] Potion PotionPrefab;

    PotionType Slot1 = 0;
    PotionType Slot2 = 0;
    PotionType Slot3 = 0;

    private void FixedUpdate()
    {
        //Check if all slots are filled
        if (Slot3 == 0)
            return;

        //Add slots together
        int s1 = (int)Slot1;
        int s2 = (int)Slot2;
        int s3 = (int)Slot3;
        int potionResult = s1 + s2 + s3;

        //Make a potion based of the added amount
        Potion newPotion = Instantiate(PotionPrefab, transform.position, Quaternion.identity);
        newPotion.ChangePotionType(potionResult);

        //Reset Slots
        Slot1 = PotionType.Empty;
        Slot2 = PotionType.Empty;
        Slot3 = PotionType.Empty;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.GetComponent("Droplet") || collision.GetComponent("Item")))
            return;

        int typeOfItem = 0;

        if (collision.gameObject.TryGetComponent(out Droplet isDroplet))
        {
            typeOfItem = 1; //Droplet
        }
        if (collision.gameObject.TryGetComponent(out Item isItem))
        {
            typeOfItem = 2; //Item
        }

        if (Slot1 == 0)
        {
            if (typeOfItem == 1) //Droplet
            {
                Slot1 = collision.gameObject.GetComponent<Droplet>().DripType;
                Destroy(collision.gameObject);
                return;
            }
            if (typeOfItem == 2) //Item
            {
                Slot1 = collision.gameObject.GetComponent<Item>().ItemType;
                Destroy(collision.gameObject);
                return;
            }
        }

        if (Slot2 == 0)
        {
            if (typeOfItem == 1) //Droplet
            {
                Slot2 = collision.gameObject.GetComponent<Droplet>().DripType;
                Destroy(collision.gameObject);
                return;
            }
            if (typeOfItem == 2) //Item
            {
                Slot2 = collision.gameObject.GetComponent<Item>().ItemType;
                Destroy(collision.gameObject);
                return;
            }
        }

        if (Slot3 == 0)
        {
            if (typeOfItem == 1) //Droplet
            {
                Slot3 = collision.gameObject.GetComponent<Droplet>().DripType;
                Destroy(collision.gameObject);
                return;
            }
            if (typeOfItem == 2) //Item
            {
                Slot3 = collision.gameObject.GetComponent<Item>().ItemType;
                Destroy(collision.gameObject);
                return;
            }
        }

    }
}
