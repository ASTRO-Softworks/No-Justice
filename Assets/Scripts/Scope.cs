using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scope : MonoBehaviour
{

    public float fireRate = 0;
    public float Damage = 10;
    public float bulletSpeed = 10f;

    //public LayerMask notToHit;
    //public LayerMask whatToHit;
    public Vector3 aimPoint;
    public Transform firePoint;
    public float distance = 5f;
    public GameObject weapon;
    //public GameObject bulletPrefab;
    //private GameObject bullet;

    float timeToFire = 0;
    

    public void takeAim(Vector3 vec)
    {
        aimPoint = vec;
    }
    //public Camera cam;
    //public NavMeshAgent agent;

    // Update is called once per frame
    void Awake () 
    {
        firePoint = gameObject.transform;
        /*firePoint = transform.Find ("FirePoint");
        if (firePoint == null) 
        {
            Debug.LogError ("No firePoint? WHAT?!");
        }
    */
    }

    void Start()
    {
        weapon = gameObject.GetComponent<WeaponList>().ChangeWeapon(0);
    }
        
    void Update()
    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;
//
//            if (Physics.Raycast(ray, out hit))
//            {
//                agent.SetDestination(hit.point);
//            }
//        }
        //Vector2 characterPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.localPosition;
        if(aimPoint == Vector3.zero)aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);//transform.position + new Vector3(-1,0);//
        Vector2 weaponPosition = aimPoint - transform.parent.localPosition;
        weaponPosition.Normalize();
        weaponPosition.Scale(new Vector2(distance, distance));
        float rotConst = (float)((((transform.rotation.eulerAngles.y / 180 - 1 )*2)+1)*-1);
        //transform.localPosition = new Vector3(rotConst * weaponPosition.x, 0, 0f);

        //Debug.Log(rotConst);
        //transform.localPosition = new Vector3( rotConst * characterPosition.x, characterPosition.y, 0f);
        transform.localPosition = new Vector3(rotConst * weaponPosition.x, weaponPosition.y, 0f);
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(transform.localPosition.y, transform.localPosition.x)* Mathf.Rad2Deg);
        
        /*
        if (fireRate == 0)
        {
            if (Input.GetButtonDown ("Fire1"))
            {
                Debug.Log("@"+weapon.name);
                weapon.GetComponents<Weapon>()[0].Shoot(true);//.Shoot();
                //(cm).Shoot();
                /*if (cm[0]!=null)
                {
                    cm[0].Shoot();
                    //Debug.Log("#"+cm[int].name);
                }*//*
                
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                weapon.GetComponents<Weapon>()[0].Shoot(true);
            }
        }
        */
    }

    public void Shoot (bool flag) {
        if (fireRate == 0)
        {
            //Debug.Log("@"+weapon.name);
            weapon.GetComponents<Weapon>()[0].Shoot(flag);//.Shoot();
        }
        else
        {
            if (Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                weapon.GetComponents<Weapon>()[0].Shoot(flag);
            }
        }


        /*
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 10000, whatToHit);
        Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);
        if (hit.collider != null) {
            Debug.DrawLine (firePointPosition, hit.point, Color.red);
            Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
        }
        float rotZ = Mathf.Atan2(transform.localPosition.y, transform.localPosition.x) * Mathf.Rad2Deg;
        Quaternion rotationZ = Quaternion.Euler (0f, 0f, rotZ - 90);
        //Instantiate(bulletPrefab, transform.localPosition, rotationZ);
        Vector2 normSpeed = transform.localPosition;
        normSpeed.Normalize ();
        bullet = Instantiate(bulletPrefab, transform.parent.localPosition + transform.localPosition, rotationZ);
        bullet.GetComponent<Bullet>().shooter = gameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.localPosition * bulletSpeed;
        */
    }
}
