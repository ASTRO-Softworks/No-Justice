/*
 * @author: Sergeev Sergey
 * @date: 4.03.19 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public abstract class AbstractCharacter : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    //public Transform Scope;
    /* Возможно понадобится
    public float climbSpeed = 20f;
    public float diveSpeed = 100;*/
    protected float runSpeed;

    /*Надеюсь не понадобится
     
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
    bool dirRight = false;*/

    void Start()
    {
      //  Scope.gameObject.GetComponent<Scope>().takeAim(Vector3.zero);
    }
    void Update () {          	}
    void FixedUpdate()     {
        //оно ходит
        /* if (Math.Abs(Math.Sin(Time.time)) < 0.1)         {             controller.Jump();         }*/
        
        //Массив всех объектов в радиусе видимости
        //Collider2D[] cld = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        
        /*Не знаю что пока с этим делать
        foreach(Collider2D item in cld)
        {
            if(item.tag == "Ladder")
            Debug.Log(item.tag );
        

            if (item.CompareTag("Ladder"))
                controller.Climb();
        } */           }     void OnTriggerEnter2D(Collider2D collider)
    {/*
        if (collider.CompareTag("Ladder"))
        {
            nearladder = true;

            //Debug.Log("Ladder");
        }
        else if (collider.CompareTag("Water"))
        {
            swimming = true;
        }
        */
    }     protected bool Climb()
    {
        controller.Toggle_Climb();
        animator.SetBool("IsClimbing", true);

        return true;
    }     protected bool Swim()
    {
        controller.Toggle_Swim();
        animator.SetBool("IsSwimming", true);

        return true;
    }     protected bool Fly()
    {
        controller.Toggle_Fly();
        animator.SetBool("IsFlying", true);

        return true;
    }     protected bool Crouch()
    {
        controller.Toggle_Crouch();
        animator.SetBool("IsCrouching", true);

        return true;
    }     protected bool Walk()
    {

        controller.Toggle_Walk();
        animator.SetBool("IsCrouching", false);
        animator.SetFloat("Speed", runSpeed);

        return true;
    }     protected bool Jump()
    {
        controller.Jump();
        animator.SetBool("IsJumping", true);

        return true;
    } }