using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class Bullet : MonoBehaviour {

    //    public float speed = 20f;
    //private Rigidbody2D rb;
    //public GameObject shooter;
    //bool b = false;

    

    // Use this for initialization
    //  void Start () {
    //     rb = gameObject.GetComponent<Rigidbody2D>();
    //rb.velocity = transform.right * speed;
    //}
    private bool cankill = true;

    void Start() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position, 0.1f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Player"))
            {
                cankill = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.isTrigger)//hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("Player"))
        {
            if (cankill)
            {
                Destroy(gameObject);

                if (hitInfo.gameObject.CompareTag("Enemy") || hitInfo.gameObject.CompareTag("Player"))
                {
                    hitInfo.gameObject.GetComponent<Stats>().Damage(1);
                }
            }
         }
    }

    void OnTriggerExit2D(Collider2D colider)
    {
        if (colider.CompareTag("Player") || colider.CompareTag("Enemy"))
        {
            cankill = true;
        }
            if (colider.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
            //~Bullet();
        }

    }

}
