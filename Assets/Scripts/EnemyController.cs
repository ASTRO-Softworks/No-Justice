﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour {
    
    public CharacterController2D controller;
    public Animator animator;
    public Transform Scope;
    private Vector3 resp;

    public float runSpeed = 40f;
    public float climbSpeed = 20f;
    public float diveSpeed = 100;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    float mouseX = 0f;
    float mousey = 0f;

    bool jump = false;
    bool crouch = false;
    bool nearladder = false;
    bool onladder = false;
    bool swimming = false;
    bool invisible = false;
    bool dirRight = false;
    //зона внимания врага
    public float farSight = 2;


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            nearladder = true;

            //Debug.Log("Ladder");
        }
        else if (collider.CompareTag("Water"))
        {
            swimming = true;
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            nearladder = false;
            //Onladder = false;
            //Debug.Log("NotLadder");

        }
        else if (collider.CompareTag("Water"))
        {
            swimming = false;
        }

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        onladder = false;
        //Debug.Log("NOT ON LADDER 'COS LANDED!!!");
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    // Use this for initialization
    void Start () {

        //Что блин такое Aimer?!
        //Да и Scope тоже?
        Scope = transform.Find("Aimer");
        //A transform вообще законно без объекта использовать?
        resp = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Массив всех объектов в радиусе видимости от врага
        Collider2D[] cld = Physics2D.OverlapCircleAll(transform.position, farSight);
        //Перебираем эти объекты
        /*я думаю лучше так
        foreach(Collider2D cldi in cld)
        {
            //если среди них есть "Player" то...

            if (cldi.gameObject.CompareTag("Player"))
            {
                //выбираем его целью? передаем его координаты вектором?
                Scope.gameObject.GetComponent<Scope>().takeAim(cldi.transform.position);
                Debug.Log(cldi.transform.position);

            }
        }*/
        for (int i = 0; i < cld.Length; i++)
        {
            //если среди них есть "Player" то...
            if (cld[i].gameObject.CompareTag("Player"))
            {
                //выбираем его целью? передаем его координаты вектором?
                Scope.gameObject.GetComponent<Scope>().takeAim(cld[i].transform.position);
                Debug.Log(cld[i].transform.position);
            }
        }
	}

    void FixedUpdate()
    {
        dirRight = (Scope.position.x - transform.localPosition.x > 0);
        //Debug.Log(dirRight);
        //Debug.Log("NearLadder " + nearladder.ToString() + "\nOnladder " + onladder.ToString());
        //оно ходит
        controller.Move(new Vector2( (float)Math.Sin(Time.time)/3, 0), dirRight, crouch, Math.Abs(Math.Sin(Time.time)) < 0.1, nearladder && onladder, swimming);
        
        jump = false;
    }
}
