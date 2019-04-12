using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scope : MonoBehaviour
{
    private Vector3 aimPoint;
    public float distance = 5f;
//    weaponPull weaponPull;
    [SerializeField] private List<Weapon> weaponPull;

    private Weapon curWeapon;

  //  private Weapon weapon;



    float timeToFire = 0.0f;

    public void takeAim(Vector3 vec)
    {
        aimPoint = vec;
    }
    void Start()
    {

        //weaponPull = gameObject.GetComponent<weaponPull>();
        if (weaponPull.Count != 0)
        { 
            curWeapon = Instantiate(weaponPull[0], gameObject.transform);
        }

        /*if (weaponPull.Count() != 0)
        {
            weapon = weaponPull.GetWeapon();
        }*/
        else
        {
            Debug.Log("" + gameObject.name + ": WHERE IS MY WEAPON LIST?!");
            
        }
        

    }

    public void ChangeWeapon(int i)
    {
        Destroy(curWeapon.gameObject);
        curWeapon = Instantiate(weaponPull[i], gameObject.transform);
        timeToFire = Time.time;
    }
        
    void Update()
    {
        if(aimPoint == Vector3.zero)aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);//transform.position + new Vector3(-1,0);//IF zero vector - aim at mouse
        Vector2 weaponPosition = aimPoint - transform.parent.localPosition;
        weaponPosition.Normalize();
        weaponPosition.Scale(new Vector2(distance, distance));
        float rotConst = (float)((((transform.rotation.eulerAngles.y / 180 - 1 )*2)+1)*-1);
        
        transform.localPosition = new Vector3(rotConst * weaponPosition.x, weaponPosition.y, 0f);
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(transform.localPosition.y, transform.localPosition.x)* Mathf.Rad2Deg);
    }
    public bool getTimeToFire()
    {
        return  Time.time > timeToFire;
    }

    public void Shoot () {
        if (getTimeToFire() && (weaponPull.Count != 0))
        {
            curWeapon.Shoot();
            timeToFire = Time.time + curWeapon.fireRate;
        }
     
    }
}