using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Anchor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v;
        v.x = 0;
        v.y = 0;
        v.z = -10;
        transform.localPosition = Anchor.transform.localPosition+v;
        /*
        Vector3 dest = Anchor.transform.position-Self.transform.position;
        Self.*/
        
	}
}
