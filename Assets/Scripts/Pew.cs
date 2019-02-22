using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pew : MonoBehaviour {
    bool b = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!b) b = true;
        else Destroy(gameObject);
	}
}
