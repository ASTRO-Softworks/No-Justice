using UnityEngine;

public class Weapon : MonoBehaviour {
    public float fireRate;
    public int Damage;
    public float bulletSpeed;
    

    public virtual void Shoot()
    {
        Debug.Log("PEW PEW PEW!!!");
    }


}
