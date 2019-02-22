using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float health = 100f;
    private float maxHealth;
    public float defence = 100f;

    void Start()
    {
        maxHealth = health;
    }



    void Update()
    {
        
        /*
        if (transform.position.y <= fallBoundary)
            Damage (999999);
        */
    }

    public void Damage (float damage)
        {
        Debug.Log("HP " + health.ToString());
        health -= damage;
        if (health <= 0)
            {
            Debug.Log("KILL "+gameObject.name);
            Destroy(gameObject);
            }
	    }
	
	// Update is called once per frame
	
}
