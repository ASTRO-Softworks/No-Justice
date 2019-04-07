<<<<<<< HEAD
using System.Collections;
=======
﻿using System.Collections;
>>>>>>> 99f83ab94ab751d498903c6f14641d9c0e303d15
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private Vector2 speed;
    [SerializeField] private float distance;
    private float modspeed;
    private float _dist = 0;
    private bool dir = false;

    // Start is called before the first frame update
    void Start()
    {
        modspeed = speed.magnitude;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
<<<<<<< HEAD
        AltAbstractCharacter Char = col.gameObject.GetComponent<AltAbstractCharacter>();
=======
        AbstractCharacter Char = col.gameObject.GetComponent<AbstractCharacter>();
>>>>>>> 99f83ab94ab751d498903c6f14641d9c0e303d15
        if (Char) Char.Die();
    }

    // Update is called once per frame
    void Update()
    {
        if (dir)
        {
            transform.position += (Vector3)speed * Time.deltaTime;
            _dist += modspeed * Time.deltaTime;
            if (_dist >= distance) dir = false;
        }
        else
        {
            transform.position -= (Vector3)speed * Time.deltaTime;
            _dist -= modspeed * Time.deltaTime;
            if (_dist <= 0) dir = true;
        }
        
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 99f83ab94ab751d498903c6f14641d9c0e303d15
