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
    
    void OnCollisionEnter2D(Collision2D collision)
    {
       /*
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.transform.position.y > transform.position.y - 0.5f)
                runSpeed = runSpeed * (((transform.position.x - collision.gameObject.transform.position.x) * runSpeed) > 0 ? 1: -1);
           
        }*/    
    }
   /* override void OnTriggerEnter2D(Collider2D collider)
    {
    }*/
    void OnCollisionExit2D(Collision2D collision)
    {
    }
    // Use this for initialization
    void Start () {
        runSpeed = 0.3f;
        
// This would cast rays only against colliders in layer 8, so we just inverse the mask.
    layerMask = ~layerMask;
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
    private int counterGun=0;
    private int counterReverse=0;
    // Bit shift the index of the layer (8) to get a bit mask
    int layerMask = 1 << 2;

   
    void FixedUpdate()
    {   
        //if (counter)
//        runSpeed = (float)Math.Sin(Time.time)/3;
        
        Vector2 direction = new Vector2(runSpeed, 0);
        for(float i = -3; i < 3; i += 0.05f)
        {
            direction = new Vector2(runSpeed, i);
            Vector2 pos = new Vector2(transform.position.x+0.5f * runSpeed/Math.Abs(runSpeed), transform.position.y - 0.5f);
    //        Ray ray = new Ray(transform.position, direction);
            hit = Physics2D.Raycast(pos, direction, 7.0f, layerMask);
            //Debug.DrawRay(transform.position, direction, Color.blue);
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Scope.gameObject.GetComponent<Scope>().takeAim(hit.collider.gameObject.transform.position);
                    if (counterGun == 0)
                    {
                        Scope.gameObject.GetComponent<Scope>().Shoot(false);
                        Scope.gameObject.GetComponent<Scope>().Shoot(true);
                        Scope.gameObject.GetComponent<Scope>().Shoot(false);
                        counterGun = 50;
                    }
                }
            }
        }
        Collider2D[] points = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + runSpeed * 2.5f, transform.position.y - 0.8f), new Vector2(0.3f,0.3f), 0.0f);
        //Debug.Log(points[0]);
        if (points.Length > 0)
        foreach(Collider2D point in points)
        {
            
            if(!point.gameObject.CompareTag("Ground") && !point.gameObject.CompareTag("Ladder"))
            {
          //      counterReverse = 50;
                runSpeed = -runSpeed;
            }
        }
        else 
        {
        //    counterReverse = 50;
            runSpeed = -runSpeed;
        }
        points = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + runSpeed * 2f, transform.position.y), new Vector2(0.1f,0.3f), 0.0f);
        foreach(Collider2D point in points)
        {
            if(point.gameObject.CompareTag("Enemy") || point.gameObject.CompareTag("Ground"))
            {
              
                //counterReverse = 50;
                runSpeed = -runSpeed;
                break;
            }
        }
        if (counterGun > 0) 
            counterGun--;
        if (counterReverse > 0) 
            counterReverse--;
        direction = new Vector2(runSpeed, 0);
          
        if(counterGun == 0 && counterReverse == 0)
            controller.Move(new Vector2(runSpeed, 0), direction);
    }
}