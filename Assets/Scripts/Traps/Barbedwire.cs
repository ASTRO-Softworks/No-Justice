﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbedwire : MonoBehaviour
{
    private CharacterController2D _ac;
    private float WS;
    private List<GameObject> Objls = new List<GameObject>();
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (_ac = Player.gameObject.GetComponent<CharacterController2D>())
        {
            if (!Objls.Contains(Player.gameObject))
            {
                Objls.Add(Player.gameObject);
                WS = _ac.m_WalkingSpeed;
                _ac.m_WalkingSpeed = 1;
            }
        }
    }

    void OnTriggerExit2D(Collider2D Player)
    {
        if (_ac = Player.gameObject.GetComponent<CharacterController2D>())
        {
            if (Objls.Contains(Player.gameObject))
            {
                _ac.m_WalkingSpeed = WS;
                Objls.Remove(Player.gameObject);
            }
          
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}