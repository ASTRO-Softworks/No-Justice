using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEditor.VersionControl;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // |  Interval |
    //         |Dur|
    // |--------***|
    // 5       1   0
    
    
    [SerializeField] private LayerMask WhatToHit = ~(1 << 2); 
    private Vector3 direction;

    public float interval = 5;
    private float time = 1;
    public float duration = 1;
    
    public int damage;
    public float rayWidth = 13.0f;
    
    [SerializeField] private bool debug;
    
   
    private RaycastHit2D hit;
    private Character.Stats goodHit;

    private Transform ray;
    private Transform head;

    private SpriteRenderer rayRenderer;
    // Start is called before the first frame update
    void Start()
    {
        direction = -transform.up;
        ray = transform.Find("Ray");
        if (!ray)
        {
            Debug.Log("Laser: !!!Where is my Ray!!!");
            gameObject.SetActive(false);
        }
        else
        {
            rayRenderer = ray.gameObject.GetComponent<SpriteRenderer>();
            if (!rayRenderer) Debug.Log("Laser: This is the wrong ray!\nAnd it has wrong renderer!!!");
        }

        //Vector3 Vec1 = new Vector3(0, 5, 0);
        //direction = GameObject.FindWithTag("TRAPLASER").transform.localPosition+Vec1;
        //Debug.Log(GameObject.FindWithTag("TRAPLASER").transform.localPosition + Vec1);
    }

    // Update is called once per frame
    void Update()
    {
        //Cast a ray in the direction specified in the inspector.

        if (time < duration)//If time has come
        {
            Color _c = rayRenderer.color;_c.a = 1.0f;rayRenderer.color = _c;//Set ray visible
            
            if (hit = Physics2D.Raycast(this.gameObject.transform.position, direction, Mathf.Infinity, WhatToHit /*this.gameObject.layer*/))// If raycast succeded
            {
                //head.rotation = Quaternion.Euler(0,0, Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg+90);
                float len = ((Vector3) (hit.point) - transform.position).magnitude; //Distance to target
                ray.position = transform.position + ((Vector3) (hit.point) - transform.position) / 2; // Ray sat inbetween...
                //ray.localPosition = Vector3.down * (len / 2 / head.localScale.y);
                ray.localScale = new Vector3(len, rayWidth, 0);// ...And scaled to match

                if (debug) Debug.DrawLine(transform.transform.position, hit.point, Color.green, .1f);

                if (goodHit = hit.transform.GetComponent<Character.Stats>())
                {
                    goodHit.Damage(damage);
                    if (debug) Debug.Log("Laser: hit " + goodHit.name + " with " + damage.ToString() + " damage");
                }
            }
        }
        else
        {
            Color _c = rayRenderer.color;_c.a = 0.0f;rayRenderer.color = _c;//Set ray invisible
        }
        
        if(debug)Debug.DrawRay(this.gameObject.transform.position, direction);

        //RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, layerMask);
        
        //If something was hit.
        //Debug.Log(hit.collider);
        //Debug.Log(layerMask);
        //if (hit.collider.gameObject.CompareTag ("Untagged")) //!= null)
        //{
        //    //If the object hit is less than or equal to 6 units away from this object.
        //    if (hit.distance <= 5.0f)
        //    {
        //        Debug.Log(hit.collider);
        //        Debug.Log(direction);
        //    }
        //}
    }
    
    void FixedUpdate()
    {
        if(time > 0)
        {
            
            time -= Time.fixedDeltaTime;
        }
        else
        {
            time = interval;
        }
    }
    
}