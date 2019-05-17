using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEditor.VersionControl;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LayerMask WhatToHit = ~(1 << 2); 
    public Vector3 direction;

    public int damage;
    public float rayWidth = 0.2f;
    
    [SerializeField] private bool debug;
    
   
    private RaycastHit2D hit;
    private Character.Stats goodHit;

    private Transform ray;
    private Transform head;
    // Start is called before the first frame update
    void Start()
    {
        head = transform.Find("Head");
        if (!head) Debug.Log("Laser: !!!Where is my Head!!!");
        else
        {
            ray = head.Find("Ray");
            if (!ray) Debug.Log("Laser: !!!Where is my Ray!!!");
        }

        //Vector3 Vec1 = new Vector3(0, 5, 0);
        //direction = GameObject.FindWithTag("TRAPLASER").transform.localPosition+Vec1;
        //Debug.Log(GameObject.FindWithTag("TRAPLASER").transform.localPosition + Vec1);
    }

    // Update is called once per frame
    void Update()
    {
        //Cast a ray in the direction specified in the inspector.
        
        
        if(hit = Physics2D.Raycast(this.gameObject.transform.position, direction, Mathf.Infinity, WhatToHit/*this.gameObject.layer*/))
        {
            
            head.rotation = Quaternion.Euler(0,0, Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg+90);
            float len = ((Vector3)(hit.point) - head.transform.position).magnitude;
            ray.position = head.position+((Vector3)(hit.point) - head.transform.position)/2;
            //ray.localPosition = Vector3.down * (len / 2 / head.localScale.y);
            ray.localScale = new Vector3(len,rayWidth,0);
            
            if(debug)Debug.DrawLine(head.transform.position, hit.point, Color.green, .1f);
            
            if (goodHit = hit.transform.GetComponent<Character.Stats>())
            {
                goodHit.Damage(damage);
                if(debug)Debug.Log("Laser: hit "+goodHit.name+" with "+damage.ToString()+" damage");
            }
        }

            //RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction, layerMask);
        if(debug)Debug.DrawRay(this.gameObject.transform.position, direction);
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
}