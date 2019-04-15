using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyController : AbstractCharacter {
    private Memory _memory = new Memory();
    public Vector3 SpawnPoint;
    int layerMask = 1 << 2;
    private float _seenDistanse = 10.0f;
    private Vector2 direction = new Vector2(0, 0);

    //private string ourTeam = gameObject.GetComponent<Stats>().ourTeam;
    //private string enemyTeam = gameObject.GetComponent<Stats>().enemyTeam;

    
    // Use this for initialization
    private void Start () {
        transform.gameObject.GetComponent<Stats>().ourTeam = "Enemy";
        transform.gameObject.GetComponent<Stats>().enemyTeam = "Player";
        runSpeed = 0.3f;
        _memory.setVelocity(runSpeed);
        _memory.setStartLastSeenPosition(transform.position);
// This would cast rays only against colliders in layer 8, so we just inverse the mask.
        layerMask = ~layerMask;
	}
	
	// Update is called once per frame
    public RaycastHit2D hit;
   // public Transform Scope;// = GameObject.Find("Aimer");
    private int counterReverse=0;
    // Bit shift the index of the layer (8) to get a bit mask

    public void Hack()
    {
        string tmp = transform.gameObject.GetComponent<Stats>().ourTeam;
        transform.gameObject.GetComponent<Stats>().ourTeam = transform.gameObject.GetComponent<Stats>().enemyTeam;
        transform.gameObject.GetComponent<Stats>().enemyTeam = tmp;
    }

    public override void _Die()
    {
        transform.position = SpawnPoint;
    }

    protected override void _FixedUpdate()
    {
        Walk();        
        
        //if we seen player somewhere, 
        //but we far from this place and go to other side =>
        // => go to another side
        if(_memory.isSeen)
            if (_memory.getDiference(transform.position) > _seenDistanse*1.2f &&( _memory.getXDir(transform.position) * runSpeed < 0)) runSpeed = - runSpeed;
        //Смотрим туда, куда идем    
        if (runSpeed != 0)
            direction = new Vector2(runSpeed, 0);
        //Стрельба
        for(float i = -3; i < 3; i += 0.1f)
        {
            Vector2 directionAngle = new Vector2(direction.x * 10, i);
            Vector2 pos = new Vector2(transform.position.x + 0.5f * direction.x/Math.Abs(direction.x), 
                                        transform.position.y - 0.5f);
            

            hit = Physics2D.Raycast(pos, directionAngle, _seenDistanse, layerMask);
            //if Enemy can see an object
            if (hit)
            {
                //if it is a player
                if (hit.collider.gameObject.CompareTag(transform.gameObject.GetComponent<Stats>().enemyTeam))
                {
                    //and it is in seen distanse
                    if (Math.Abs(hit.collider.gameObject.transform.position.x - transform.position.x) < _seenDistanse)
                    {
                        //save Player position to AI memory
                        _memory.setLastSeenPosition(hit.collider.gameObject.transform.position);
                        //set vector of shooting
                        if (Math.Abs(hit.collider.gameObject.transform.position.x - transform.position.x) 
                            < transform.Find("Aimer").gameObject.GetComponent<Scope>().GetCurWeapon().distance)
                        {
                            transform.Find("Aimer").gameObject.GetComponent<Scope>().takeAim(hit.collider.gameObject.transform.position);
                            //Shoot
                            transform.Find("Aimer").gameObject.GetComponent<Scope>().Shoot();

                        }
                        
                    }
                }
            }
        }

        //если перед нами не земля то туда не стоит идти
        Collider2D[] points = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + direction.x * 2.5f, transform.position.y - 0.8f), new Vector2(0.3f,0.3f), 0.0f);
        if (points.Length > 0)
        foreach(Collider2D point in points)
        {
            
            if(!point.gameObject.CompareTag("Ground") && !point.gameObject.CompareTag("Ladder"))
            {
                    if (runSpeed * _memory.getXDir(transform.position) <= 0)
                        runSpeed = -runSpeed;
                    else
                    if (counterReverse == 0)
                    {
                        _memory.setVelocity(-runSpeed);
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
        //если перед нами стена или другой враг то разворачиваемся
        points = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + direction.x * 2f, transform.position.y), new Vector2(0.1f,0.3f), 0.0f);
        foreach(Collider2D point in points)
        {
            if(point.gameObject.CompareTag(transform.gameObject.GetComponent<Stats>().ourTeam) || point.gameObject.CompareTag("Ground"))
            {
                runSpeed = -runSpeed;
                break;
            }
        }
        if (counterReverse > 0)
        {
            counterReverse--;
            if (counterReverse == 0)
                runSpeed = _memory.getVelocity();
        }
        if (runSpeed != 0)
            direction = new Vector2(runSpeed, 0);
        //движение  
        if(transform.Find("Aimer").gameObject.GetComponent<Scope>().getTimeToFire())
            controller.Move(new Vector2(runSpeed, 0), direction);
    }
}