using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFG : Weapon
{

    private Transform firePoint;
    public GameObject bulletPrefab;
    public LayerMask whatToHit;
    public GameObject pew;
    public float distance = 10;
    //int layerMask = whatToHit;//~(1 << 2);

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
        /*
        Vector2 dir = new Vector2(rotConst * (float)Math.Cos(transform.parent.localRotation.z),
                                    (float)Math.Sin(transform.parent.localRotation.z));
                                    */
        Vector2 dir = new Vector2(1, Mathf.Tan(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * rotConst) * rotConst;
        Vector2 speed = dir.normalized;
        //Mathf.Tan(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        
        RaycastHit2D hit  = Physics2D.Raycast(firePoint.position, dir, distance,whatToHit);
        if (hit)
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);
            Debug.DrawLine(firePoint.position, hit.point, Color.red);
        }
        Debug.DrawRay(firePoint.position, dir,Color.blue);

        /*
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).
            gameObject.GetComponent<Rigidbody2D>().velocity
            = new Vector3(speed.x * bulletSpeed, speed.y * bulletSpeed, 0);
         */

    }
}