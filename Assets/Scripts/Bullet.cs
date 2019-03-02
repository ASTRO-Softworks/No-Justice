using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject shooter;
    //bool b = false;



    // Use this for initialization
    void Start () {
        //rb.velocity = transform.right * speed;
	}

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //CharacterController2D ctr = hitInfo.gameObject.GetComponent<CharacterController2D>();
        //FizzCtr.Damage(10);
        //ctr
        if ((!hitInfo.isTrigger && hitInfo.gameObject!=shooter) || hitInfo.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
            if (hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("Player"))
            {

                hitInfo.gameObject.GetComponent<Stats>().Damage(26);
            }
            //hitInfo.gameObject.GetComponent<Stats>().Damage(26);
    //        Debug.Log(hitInfo.gameObject.name);

            //gameObject.transform.parent.loca
        }
    }

    void OnTriggerExit2D(Collider2D colider)
    {
        if (colider.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
            //~Bullet();
        }

    }

}