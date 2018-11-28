using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject shooter;
    bool b = false;



    // Use this for initialization
    void Start () {
        rb.velocity = transform.right * speed;
	}

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        
        if (!hitInfo.collider.isTrigger && hitInfo.gameObject!=shooter) {
            Destroy(gameObject);
            Debug.Log(hitInfo.gameObject.name);
        }
    }

}
