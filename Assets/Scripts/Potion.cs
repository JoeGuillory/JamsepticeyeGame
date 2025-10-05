using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Potion : MonoBehaviour
{
    [SerializeField] PotionType PotionStatus;
    [SerializeField] Transform SpriteTransform;
    [SerializeField] Transform DropletOffset;
    [SerializeField] Droplet DropletPrefab;
    [SerializeField] int MaxUses = 3;
    bool ResetRotationInvoked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Vector2 GetDropletOffset()
    {
        return DropletOffset.position;
    }

    private void Update()
    {
        if(MaxUses == 0)
        {
            Destroy(this.gameObject);
        }
    }
    bool IsRotated()
    {
        if (SpriteTransform.rotation == Quaternion.Euler(0, 0, 90))
            return true;
        else
            return false; 
    }

    void ResetSpriteRotation()
    {
        SpriteTransform.rotation = Quaternion.Euler(0, 0, 0);
        ResetRotationInvoked = false;
    }

    public void PourPotion()
    {
        MaxUses--;
        if (!IsRotated())
            SpriteTransform.rotation = Quaternion.Euler(0, 0, 90);

        SpawnDroplet();

        if(!ResetRotationInvoked)
        {
            ResetRotationInvoked = true;
            Invoke("ResetSpriteRotation", 0.5f);
        }
        
    }

    void SpawnDroplet()
    {
        Droplet drop = Instantiate(DropletPrefab,GetDropletOffset(),Quaternion.identity);
        drop.DripType = PotionStatus;
    }

    public void ChangePotionType(PotionType newPotionStatus)
    {
        PotionStatus = newPotionStatus;
        return;
    }
    public void ChangePotionType(int newPotionStatus)
    {
        PotionStatus = (PotionType)(newPotionStatus);
        return;
    }
}
