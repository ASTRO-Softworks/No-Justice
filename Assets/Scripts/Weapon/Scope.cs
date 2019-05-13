/*
 * Developer:    Anton Larin && Sergeev Sergey
 * Contact:      https://vk.com/sergeev_1999
 * Version:      2.3.?
 * Date:         ??.0?.18
 * Last Update:  13.05.19
 * class for Players hand, weapon and smth
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Scope : MonoBehaviour
{
    private Vector3 aimPoint;
    public float distance = 1f;
//    weaponPull weaponPull;
    [SerializeField] private List<Weapon> weaponPull;
    [SerializeField] private List<Skill> skillPull;

    private Weapon curWeapon;
    private Skill curSkill;

    float timeToFire = 0.0f;
    float timeToSkill = 0.0f;

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
        }else
        {
            Debug.Log("" + gameObject.name + ": WHERE IS MY WEAPON LIST?!");   
        }


        if (skillPull.Count != 0)
        { 
            curSkill = skillPull[0];
        }else
        {
            Debug.Log("" + gameObject.name + ": WHERE IS MY Skill LIST?!");   
        }
        

    }

    public void ChangeWeapon(int i)
    {
        Destroy(curWeapon.gameObject);
        curWeapon = Instantiate(weaponPull[i], gameObject.transform);
        timeToFire = Time.time;
    }
    public void ChangeSkill(int i)
    {
        //Destroy(curWeapon.gameObject);
        //curWeapon = Instantiate(weaponPull[i], gameObject.transform);
        curSkill = skillPull[i];
        timeToSkill = Time.time;
    }

    public Weapon GetCurWeapon()
    {
        return curWeapon;
    }
    public Skill GetCurSkill()
    {
        return curSkill;
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

    public void SetTimeToFire(float waitTime)
    {
        timeToFire = Time.time + waitTime;
    }

    public bool getTimeToSkill()
    {
        return  Time.time > timeToSkill;
    }

    public void Shoot () {
        if (getTimeToFire() && (weaponPull.Count != 0))
        {
            curWeapon.Shoot();
            timeToFire = Time.time + curWeapon.fireRate;
        }
     
    }
    public void Active() {
        if (getTimeToSkill() && (skillPull.Count != 0))
        {
            curSkill.Active();
            timeToSkill = Time.time + curSkill.skillRate;
        }
     
    }
}