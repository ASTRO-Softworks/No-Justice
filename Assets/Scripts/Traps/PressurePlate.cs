using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    public bool toggle = false;
    public float duration;
    private bool pressed;
    private float counter;
    private float offset = 0.2f;
    private int objCnt;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!toggle)
        {
            if(counter > 0)counter -= Time.deltaTime;
            else pressed = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        objCnt++;
        if (objCnt == 1)
        {
            sprite.position += Vector3.down * offset;
        }
        if (col.CompareTag("Player"))
        {
            if (toggle) pressed = !pressed;
            else
            {
                pressed = true;
                counter = duration;
            }
        }
    }

    void OnTriggerexit2D(Collider2D col)
    {
        objCnt--;
        if (objCnt == 0)
        {
            sprite.position -= Vector3.down * offset;
        }
        if (col.CompareTag("Player"))
        {
            if (toggle) pressed = !pressed;
            else
            {
                pressed = true;
                counter = duration;
            }
        }
    }
}
