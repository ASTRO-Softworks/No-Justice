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
    private Memory memory = new Memory();
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
        memory.setVelocity(runSpeed);
        memory.setStartLastSeenPosition(transform.position);
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
    private float seenDistanse = 10.0f;
    private Vector2 direction = new Vector2(0, 0);

    void FixedUpdate()
    {   
        //if we seen player somewhere, but we far from this place and go to other side => go to another side
        if(memory.isSeen)
            if (memory.getDiference(transform.position) > seenDistanse*1.2f &&( memory.getXDir(transform.position) * runSpeed < 0)) runSpeed = - runSpeed;
        if (runSpeed != 0)
            direction = new Vector2(runSpeed, 0);
        //shotting
        for(float i = -3; i < 3; i += 0.1f)
        {
            Vector2 directionAngle = new Vector2(direction.x * 10, i);
            Vector2 pos = new Vector2(transform.position.x + 0.5f * direction.x/Math.Abs(direction.x), 
                                        transform.position.y - 0.5f);
            hit = Physics2D.Raycast(pos, directionAngle, seenDistanse, layerMask);
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    if (Math.Abs(hit.collider.gameObject.transform.position.x - transform.position.x) < seenDistanse)
                    {
                        memory.setLastSeenPosition(hit.collider.gameObject.transform.position);
                        Scope.gameObject.GetComponent<Scope>().takeAim(hit.collider.gameObject.transform.position);
                        if (counterGun == 0)
                        {

//                            Scope.gameObject.GetComponent<Scope>().Shoot(false);
                            Scope.gameObject.GetComponent<Scope>().Shoot(true);
                            Scope.gameObject.GetComponent<Scope>().Shoot(false);
                            counterGun = 50;
                        }
                    }
                }
            }
        }

        Collider2D[] points = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + direction.x * 2.5f, transform.position.y - 0.8f), new Vector2(0.3f,0.3f), 0.0f);
        if (points.Length > 0)
        foreach(Collider2D point in points)
        {
            
            if(!point.gameObject.CompareTag("Ground") && !point.gameObject.CompareTag("Ladder"))
            {
                    if (runSpeed * memory.getXDir(transform.position) <= 0)
                        runSpeed = -runSpeed;
                    else
 //TODO:
                    if (counterReverse == 0)
                    {
                        memory.setVelocity(-runSpeed);
                        counterReverse = 100;
                        runSpeed = 0;
                    }
            }
        }
        else 
        {
        //    counterReverse = 50;
            runSpeed = -runSpeed;
        }
        points = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + direction.x * 2f, transform.position.y), new Vector2(0.1f,0.3f), 0.0f);
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
        {
            counterReverse--;
            if (counterReverse == 0)
                runSpeed = memory.getVelocity();
        }
        if (runSpeed != 0)
            direction = new Vector2(runSpeed, 0);
          
        if(counterGun == 0 && counterReverse == 0)
            controller.Move(new Vector2(runSpeed, 0), direction);
    }
}