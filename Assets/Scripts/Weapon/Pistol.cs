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
        float rotConst = transform.parent.parent.GetComponent<CharacterController2D>().FacingRight ? 1 : -1;//transform.parent.parent.localScale.x > 0 ? 1 : -1;//(float)((((transform.rotation.eulerAngles.y / 180 - 1) * 2) + 1) * -1);
        /*
        Vector2 dir = new Vector2( rotConst * (float)Math.Cos(transform.parent.localRotation.z),
                                    (float)Math.Sin(transform.parent.localRotation.z));
        Vector2 speed = dir.normalized;
        */
        
        //Vector2 dir = new Vector2(1, Mathf.Tan(transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad) * rotConst) * rotConst;
        Vector2 dir = Vector2.right*rotConst;
        Vector2 speed = dir.normalized;
        
        GameObject bull = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            //.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(speed.x * bulletSpeed, speed.y * bulletSpeed, 0);

            bull.GetComponent<Bullet>().startTeg = gameObject.transform.parent.transform.parent.tag;
            bull.GetComponent<Rigidbody2D>().velocity = new Vector3(speed.x * bulletSpeed , speed.y * bulletSpeed , 0);

    }
}