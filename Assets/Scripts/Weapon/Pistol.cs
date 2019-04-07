using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

    private Transform firePoint;
    public GameObject bulletPrefab;
    public LayerMask whatToHit;
    public GameObject pew;

    void Start()
    {
//        fireRate = 1.5f;
//        Damage = 10.0f;
//        bulletSpeed = 1.0f;
        firePoint = transform.Find("FirePoint");
    }

    override public void Shoot()
    {
        float rotConst = (float)((((transform.rotation.eulerAngles.y / 180 - 1) * 2) + 1) * -1);

        Vector2 dir = new Vector2( rotConst * (float)Math.Cos(transform.parent.localRotation.z),
                                    (float)Math.Sin(transform.parent.localRotation.z));
        Vector2 speed = dir.normalized;

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).
            gameObject.GetComponent<Rigidbody2D>().velocity 
            = new Vector3(speed.x * bulletSpeed, speed.y * bulletSpeed, 0);
    }
}