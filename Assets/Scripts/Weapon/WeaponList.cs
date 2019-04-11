using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour {

    [SerializeField]private List<Weapon> weaponPull;
    //public Transform scope;
//    private int weaponIx;
    private Weapon curWeapon;
    public int Count()
    {
        return weaponPull.Count;
    }

	// Use this for initialization
	void Start () {
        if(Count() != 0)
            curWeapon = Instantiate(weaponPull[0], gameObject.transform);
    }
	
    public Weapon ChangeWeapon(int i)
    {
        Destroy(curWeapon.gameObject);
        curWeapon = Instantiate(weaponPull[i], gameObject.transform);
        return curWeapon;
    }

    public Weapon GetWeapon()
    {
        return curWeapon;
    }

}
