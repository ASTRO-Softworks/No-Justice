using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.Networking;

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
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
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
        if (hitInfo.isTrigger) return;
        if (!cankill) return;
        Destroy(gameObject);
        if (!hitInfo.gameObject.CompareTag("Enemy") && !hitInfo.gameObject.CompareTag("Player")) return;
        hitInfo.gameObject.GetComponent<Stats>().Damage(1);
        hitInfo.gameObject.GetComponent<Stats>().SetMemory(startPosition);
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
        }

    }

}
