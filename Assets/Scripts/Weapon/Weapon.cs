using UnityEngine;

public class Weapon : MonoBehaviour {
    public float fireRate;
    public float Damage;
    public float bulletSpeed;
    

    public virtual void Shoot()
    {
        Debug.Log("PEW PEW PEW!!!");
    }


}
