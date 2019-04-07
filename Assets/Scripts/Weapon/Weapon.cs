using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float fireRate;
    public float Damage;
    public float bulletSpeed;
    

    virtual public void Shoot()
    {
        Debug.Log("PEW PEW PEW!!!");
    }


}
