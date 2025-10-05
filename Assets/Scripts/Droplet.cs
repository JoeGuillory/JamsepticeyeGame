using UnityEngine;

public class Droplet : MonoBehaviour
{
    PotionType DropletType;

    public PotionType DripType { get { return DropletType; } set { DropletType = value; } }


    private void FixedUpdate()
    {
        if(transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }
}
