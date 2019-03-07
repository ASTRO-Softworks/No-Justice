using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : AbstractCharacter {
    /* Видимо не нужно уже
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
    */
   //Пусть пока побудет
   // bool jump = false;
    //bool crouch = false;
    //bool nearladder = false;
    //bool onladder = false;
    //bool swimming = false;
    //bool invisible = false;
    //bool dirRight = false;
  //  public EnemyAI script;//пока не используется
    
    void OnTriggerEnter2D(Collider2D collider)
    {
       

    }
    void OnTriggerExit2D(Collider2D collider)
    {

    }
    
    // Use this for initialization
    void Start () {

       // Scope = transform.Find("Aimer");
     //   resp = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Collider2D[] cld = Physics2D.OverlapCircleAll(transform.position, 10);
        Walk();        
       /* for(int i = 0; i < cld.Length; i++)
        {
            if (cld[i].gameObject.CompareTag("Player"))
            {
                if (cld[i].gameObject.transform.position.y > transform.position.y + 2)
                    Jump();
              //  else
                //    Crouch();
                //Scope.gameObject.GetComponent<Scope>().takeAim(cld[i].transform.position);
            }
             
        }*/
	}
    public RaycastHit2D hit;
    public Transform Scope;// = GameObject.Find("Aimer");
    private int counter=0;
    void FixedUpdate()
    {   
        //if (counter)
runSpeed = (float)Math.Sin(Time.time)/3;

        Vector2 direction = new Vector2(runSpeed, 0);
        Vector2 pos = new Vector2(transform.position.x+0.5f * runSpeed/Math.Abs(runSpeed), transform.position.y-0.5f);
//        Ray ray = new Ray(transform.position, direction);
        hit = Physics2D.Raycast(pos, direction, 7.0f);
        //Debug.DrawRay(transform.position, direction, Color.blue);
        if (hit)
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Scope.gameObject.GetComponent<Scope>().takeAim(hit.collider.gameObject.transform.position);
                if (counter == 0)
                {
                    Scope.gameObject.GetComponent<Scope>().Shoot(false);
                    Scope.gameObject.GetComponent<Scope>().Shoot(true);
                    Scope.gameObject.GetComponent<Scope>().Shoot(false);
                    counter = 30;
                }
                if (counter > 0) 
                    counter--;
                Debug.Log(hit.collider.gameObject.tag);
                runSpeed = 0;
            }
        //    Physics2D.Raycast( new Vector2(transform.position.x-1.0f, transform.position.y), Vector2.left, , hit, 2.0f);
        /*if(hit.collider != null)
        {
//            Debug.Log(hit.collider.tag);
            if(hit.collider.tag == "Player") 
                Crouch();
        }*/
        controller.Move(new Vector2(runSpeed, 0), direction);
    }
}