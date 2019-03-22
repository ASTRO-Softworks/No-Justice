using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour {

    public List<GameObject> weaponPull;
    //public Transform scope;
//    private int weaponIx;
    private GameObject curWeapon;


	// Use this for initialization
	void Start () {
        curWeapon = weaponPull[0];

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject ChangeWeapon(int i)//NOT DONE!!!!
    {
        weaponPull[i] = curWeapon;
       // weaponIx = i;
        //curWeapon = weaponPull[i];
        curWeapon = Instantiate(weaponPull[i],gameObject.transform);
        return curWeapon;
    }

    public GameObject GetWeapon()
    {
        return curWeapon;
    }

}
