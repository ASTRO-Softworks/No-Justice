using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float starttime = 5;
    private float time = 1;
    public float timeattack = 1;
    public Vector3 direction;
    public RaycastHit2D HITHIT;
    public LayerMask LayerMask = ~(1 << 2);
    //public float rotation; //поворот лазера Vector3.Lerp(this.gameObject.transform.position, direction, rotation);
    
    void Start()

    {
    }

    void Update()
    {
    if (time < timeattack)
        {
        HITHIT = Physics2D.Raycast(this.gameObject.transform.position, direction, Mathf.Infinity, LayerMask);
        Debug.Log(0);
        }

    Debug.DrawLine(this.gameObject.transform.position, HITHIT.point );
        if (time < timeattack && HITHIT && HITHIT.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log(HITHIT.collider);
        }
    }
    void FixedUpdate()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = starttime;
        }
    }
}