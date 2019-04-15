using UnityEngine;

public class Weapon : MonoBehaviour {
    public float fireRate;
    public int Damage;
    public float bulletSpeed;

    public float distance = 10;    

    public virtual void Shoot()
    {
        Debug.Log("PEW PEW PEW!!!");
    }


}
