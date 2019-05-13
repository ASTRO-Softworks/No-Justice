using UnityEngine;
using System;
using UnityEngine.Serialization;

namespace AI
{
    public class Drone : AbstractCharacter
    {
        private readonly Memory _memory = new Memory();
        [FormerlySerializedAs("SpawnPoint")] public Vector3 spawnPoint;
        int _layerMask = 1 << 2;
        private float _seenDistanse = 10.0f;
        private Vector2 _direction = new Vector2(0, 0);

        // Use this for initialization
        private void Start()
        {
            transform.gameObject.GetComponent<Character.Stats>().ourTeam = "Enemy";
            transform.gameObject.GetComponent<Character.Stats>().enemyTeam = "Player";
            runSpeed = 0.3f;
            runSpeed_Y = runSpeed;
            _memory.setVelocity(runSpeed);
            _memory.setVelocityY(runSpeed_Y);
            _memory.setStartLastSeenPosition(transform.position);
// This would cast rays only against colliders in layer 8, so we just inverse the mask.
            _layerMask = ~_layerMask;
            Fly();
        }

        // Update is called once per frame
        public RaycastHit2D hit;

        // public Transform Scope;// = GameObject.Find("Aimer");
        private int _counterReverse = 0;

        // Bit shift the index of the layer (8) to get a bit mask
        public void SetToMemory(Vector2 vector2)
        {
            _memory.setLastSeenPosition(vector2);
        }

        public void Hack()
        {
            string tmp = transform.gameObject.GetComponent<Character.Stats>().ourTeam;
            transform.gameObject.GetComponent<Character.Stats>().ourTeam = transform.gameObject.GetComponent<Character.Stats>().enemyTeam;
            transform.gameObject.GetComponent<Character.Stats>().enemyTeam = tmp;
        }

        public override void _Die()
        {
            transform.position = spawnPoint;
        }

        private bool _walking;

        private float runSpeed_Y;

        protected override void _FixedUpdate()
        {
            
            //Fly();
            //if we seen player somewhere, 
            //but we far from this place and go to other side =>
            // => go to another side
            if (_memory.isSeen)
            {
                if (_memory.getDiference(transform.position) > _seenDistanse * 0.32f)
                {
                    if (_memory.getXDir(transform.position) * runSpeed < 0)
//                        runSpeed = _memory.changeMoveDirection(transform.position).x;
                        runSpeed = -runSpeed;

                    if (_memory.getYDir(transform.position) * runSpeed_Y < 0){
                        //runSpeed_Y = _memory.changeMoveDirection(transform.position).y;
                        runSpeed_Y = -_memory.getVelocityY();

                    _memory.setVelocityY(runSpeed_Y);
                }
            }
                else if (_memory.getDiference(transform.position) < _seenDistanse * 0.2f)
                {
                    runSpeed_Y = 0;
                }
            }
            //Смотрим туда, куда идем    
            if (runSpeed != 0)
                _direction = new Vector2(runSpeed, 0);
            //Стрельба
            for (int i = -30; i < 30; i ++)
            {
//            Vector2 directionAngle = new Vector2(direction.x * 10, i);
                //   Vector2 pos = new Vector2(transform.position.x + 0.5f * direction.x / Math.Abs(direction.x), transform.position.y - 0.5f);


                var position = transform.position;
                hit = Physics2D.Raycast(new Vector2(position.x + 0.5f * _direction.x / Math.Abs(_direction.x),
                        position.y - 0.5f)
                    , new Vector2(_direction.x * 10, i), _seenDistanse, _layerMask);
                //if Enemy can see an object
                if (hit)
                {
                    //if it is a player
                    if (hit.collider.gameObject.CompareTag(transform.gameObject.GetComponent<Character.Stats>().enemyTeam))
                    {
                        //and it is in seen distanse
                        if (Math.Abs(hit.collider.gameObject.transform.position.x - transform.position.x) <
                            _seenDistanse)
                        {
                            //save Player position to AI memory
                            _memory.setLastSeenPosition(hit.collider.gameObject.transform.position);
                            //set vector of shooting
                            if (Math.Abs(hit.collider.gameObject.transform.position.x - transform.position.x)
                                < transform.Find("Aimer").gameObject.GetComponent<Scope>().GetCurWeapon().distance)
                            {
                                transform.Find("Aimer").gameObject.GetComponent<Scope>()
                                    .takeAim(hit.collider.gameObject.transform.position);
                                //Shoot
                                transform.Find("Aimer").gameObject.GetComponent<Scope>().Shoot();
                            }
                        }

                    }
                    else if (i == 0)
                    {
                        if (
                            hit.distance <= gameObject.GetComponent<BoxCollider2D>().size.x / 2
                            &&
                            (
                                hit.collider.gameObject.CompareTag(transform.gameObject.GetComponent<Character.Stats>().ourTeam)
                                ||
                                hit.collider.gameObject.CompareTag("Ground")
                            )
                        )
                            runSpeed = -runSpeed;
                    }
                }
              
            }

            
            if (!_memory.isSeen)            
                runSpeed_Y = (float) Math.Sin( Time.time) * _memory.getVelocityY()* _memory.getVelocityY();
            else
            {
                
                _memory.Forget();
            }

            Debug.Log(runSpeed_Y + "    " + _memory.getVelocityY());
            if (transform.Find("Aimer").gameObject.GetComponent<Scope>().getTimeToFire())
                    controller.Move(new Vector2(runSpeed, runSpeed_Y), _direction);
           
        }
    }
}