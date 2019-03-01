using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon {

    public Transform firePoint;
    public GameObject bulletPrefab;
    //public LayerMask notToHit;
    public LayerMask whatToHit;
    public GameObject pew;
    private GameObject prevBullet;


    // Update is called once per frame
    void Update () {
		if (Input.GetButtonDown("Fire1"))
        {
            //Shoot();
        }
        //Shoot();
    }

    void Start()
    {
        firePoint = transform.Find("FirePoint");
    }

    override public void Shoot(bool flag)
    {
        // shooting logic
        
        Transform tr = transform; 
        
        float rotConst = (float)((((transform.rotation.eulerAngles.y / 180 - 1) * 2) + 1) * -1);

        Vector3 pos = gameObject.transform.position;
        Vector3 dir= Vector3.Scale(transform.parent.localPosition.normalized, new Vector3(rotConst, 1f, 1f));
        /*
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, 10000, whatToHit);
        Debug.DrawLine(pos, dir * 100, Color.cyan);

        if (hit.collider != null)
        {
            Debug.DrawLine(pos, hit.point, Color.red);
            //Debug.Log("We hit " + hit.collider.name );
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Stats>().Damage(26f);
            }
        }
        */

        prevBullet=Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(pew, firePoint.position, firePoint.rotation);
        //prevBullet.GetComponent<Bullet>().shooter=gameObject;
        Vector3 speed = dir.normalized;
        prevBullet.gameObject.GetComponent<Rigidbody2D>().velocity = speed*2;
        //Debug.Log(transform.parent.localPosition);
    }
}
