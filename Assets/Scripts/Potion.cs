using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Potion : MonoBehaviour
{
    [SerializeField] PotionType PotionStatus;
    [SerializeField] Transform SpriteTransform;
    [SerializeField] Transform DropletOffset;
    [SerializeField] Droplet DropletPrefab;
    bool ResetRotationInvoked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Vector2 GetDropletOffset()
    {
        return DropletOffset.position;
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
}
