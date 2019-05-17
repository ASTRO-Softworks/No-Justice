using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    
    [SerializeField] public float m_JumpForce = 1000f;                               // Force applied upwards when jumping
    //[SerializeField] private float m_JumpHieght = 25f;                           // Top jump speed
    //[SerializeField] private float m_JumpDuration = 0.5f;                        // Jump duration
    [SerializeField] public float m_FlyingSpeed = 50f;                            // Flying speed multiplier
    [SerializeField] public float m_WalkingSpeed = 20f;                           // Walking speed multiplier
    [SerializeField] public float m_ClimbingSpeed = 20f;                          // Climbing speed multiplier
    [SerializeField] public float m_SwimingForce = 600f;                           // Swimming force multiplier
    [SerializeField] public float m_CrouchSpeed = 10;                             // Crouching speed multiplier
    [SerializeField] public float m_GravityScale = 6f;                             // Default non-zero gravity
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;     // How much to smooth out the movement
    //[SerializeField] private bool m_AirControl = false;                          // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                             // A mask determining what is ground to the character
    private Vector2 m_GroundCheck;                                                 // A position marking where to check if the player is grounded.
    private Vector2 m_CeilingCheck;                                                // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                   // A collider that will be disabled when crouching
    private Rigidbody2D m_Rigidbody2D;                                             // Rigidbody attached to cheracter
    const float k_GroundedRadius = .2f;                                            // Radius of the overlap circle to determine if grounded
    [SerializeField] private bool m_Grounded;                                      // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f;                                             // Radius of the overlap circle to determine if the player can stand up
    private bool m_FacingRight = true;                                             // For determining which way the player is currently facing.

    //private float m_JumpCycle = 0;

    private Vector2 m_Velocity = Vector2.zero;
    public enum State
    {
        Walk,
        Crouch,
        Climb,
        Swim,
        Fly,
        LEN
    }
    [SerializeField] private State condition;


    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    public bool m_wasCrouching = false;//PRIVATE

    public State Condition
    {
        get
        {
            return condition;
        }
    }

    public Vector2 GroundCheck
    {
        get
        {
            return m_GroundCheck;
        }
    }

    public Vector2 CeilingCheck
    {
        get
        {
            return m_CeilingCheck;
        }
    }

   

    public LayerMask WhatIsGround
    {
        get
        {
            return m_WhatIsGround;
        }
    }

    private void Awake()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void Start()
    {
        m_GroundCheck = new Vector2(0 , - transform.localScale.y * (transform.GetComponent<BoxCollider2D>().size.y + transform.GetComponent<CircleCollider2D>().radius * 2) / 2);
        m_CeilingCheck = new Vector2(0 , transform.localScale.y * (transform.GetComponent<BoxCollider2D>().size.y + transform.GetComponent<CircleCollider2D>().radius * 2) / 2);
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        //Toggle_Walk();
    }

    private void FixedUpdate()
    {
        //Update Grounded
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        Collider2D[] Gcolliders = Physics2D.OverlapCircleAll(transform.position + (Vector3) m_GroundCheck, k_GroundedRadius, m_WhatIsGround);

        foreach (Collider2D collider in Gcolliders)
        {
            if (collider.gameObject != gameObject && !collider.isTrigger)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }
    public void Move(Vector2 moveDirection, Vector2 lookDirection)
    {
        if ((lookDirection.x > 0) ^ m_FacingRight && (lookDirection.x != 0))
        {
            // ... flip the player.
            Flip();
        }
        switch (condition)
        {
            case State.Fly: _Logic_Fly(moveDirection); break;
            case State.Walk: _Logic_Walk(moveDirection); break;
            case State.Crouch: _Logic_Crouch(moveDirection); break;
            case State.Climb: _Logic_Climb(moveDirection); break;
            case State.Swim: _Logic_Swim(moveDirection); break;
        }
    }

    public void Jump()
    {
        if (m_Grounded)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce);
        }

    }

    private void _Mechanics_Destruct_Manager()
    {
        switch (condition)
        {
            case State.Fly: return;
            case State.Climb: return;
            case State.Swim: return;
            case State.Crouch: _Destruct_Crouch(); break;
            case State.Walk: _Destruct_Walk(); break;
        }
        condition = State.Fly;
    }

    public void Toggle(State state)
    {
        switch (state)
        {
            case State.Fly: Toggle_Fly(); break;
            case State.Climb: Toggle_Climb(); break;
            case State.Swim: Toggle_Swim(); break;
            case State.Crouch: Toggle_Crouch(); break;
            case State.Walk: Toggle_Walk(); break;
        }
    }

    public void Toggle_Fly()
    {
        _Mechanics_Destruct_Manager();
        condition = State.Fly;
    }
    private void _Logic_Fly(Vector2 direction)
    {
        Vector2 targetVelocity = new Vector2(direction.x * m_FlyingSpeed, direction.y * m_FlyingSpeed);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    public void Toggle_Walk()
    {
        _Mechanics_Destruct_Manager();
        //Enabling gravity
        m_Rigidbody2D.gravityScale = m_GravityScale;
        condition = State.Walk;
    }
    private void _Logic_Walk(Vector2 direction)
    {
        Vector2 targetVelocity = new Vector2(direction.x * m_WalkingSpeed, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
    private void _Destruct_Walk()
    {
        m_Rigidbody2D.gravityScale = 0;
    }

    public void Toggle_Crouch()
    {
        _Mechanics_Destruct_Manager();
        //Enabling gravity
        m_Rigidbody2D.gravityScale = m_GravityScale;
        // Disable one of the colliders when crouching
        if (m_CrouchDisableCollider != null)
            m_CrouchDisableCollider.enabled = false;
        condition = State.Crouch;
    }
    private void _Logic_Crouch(Vector2 direction)
    {
        //Getting target velocity
        Vector2 targetVelocity = new Vector2(direction.x * m_CrouchSpeed, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
    private void _Destruct_Crouch()
    {
        m_Rigidbody2D.gravityScale = 0;
        // Enable the collider when not crouching
        if (m_CrouchDisableCollider != null)
            m_CrouchDisableCollider.enabled = true;
    }

    public void Toggle_Climb()
    {
        _Mechanics_Destruct_Manager();
        condition = State.Climb;
    }
    private void _Logic_Climb(Vector2 direction)
    {
        //Getting target velocity
        Vector2 targetVelocity = new Vector2(direction.x * m_ClimbingSpeed / 2, direction.y * m_ClimbingSpeed);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    public void Toggle_Swim()
    {
        _Mechanics_Destruct_Manager();
        condition = State.Swim;
    }
    public void _Logic_Swim(Vector2 direction)
    {
        m_Rigidbody2D.AddForce(Vector2.Scale(direction, new Vector2(m_SwimingForce, m_SwimingForce)));
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        // Multiply the player's x local scale by -1.
        //transform.localScale.Scale(new Vector3(-1,0,0));
        transform.Rotate(0f, 180f, 0f);
    }
}