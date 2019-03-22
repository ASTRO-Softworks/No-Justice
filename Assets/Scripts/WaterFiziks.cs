using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFiziks : MonoBehaviour {

    public Rigidbody2D rb;
    public float volume=0;
    float defaultGravity=1;
    float defaultMass;
    
	// Use this for initialization
	void Start () {
        defaultGravity = rb.gravityScale;
        defaultMass = rb.mass;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
        {
            rb.gravityScale = defaultGravity * (1 - volume / defaultMass);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
        {
            rb.gravityScale = defaultGravity;
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
