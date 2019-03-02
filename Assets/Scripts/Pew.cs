using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pew : MonoBehaviour {
    bool b = false;
    float timeTolive = 2;
    float createdTime;

	// Use this for initialization
	void Start () {
        createdTime = Time.time;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Time.time - createdTime > timeTolive)
            Destroy(gameObject);
	}
}