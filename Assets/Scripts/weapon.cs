using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPrefab;
    private GameObject prevBullet;
    // Update is called once per frame
    void Update () {
		if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}

    void Shoot()
    { 
        // shooting logic
        
        prevBullet=Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        prevBullet.GetComponent<Bullet>().shooter=gameObject;
        //Debug.Log(script.name);
    }
}
