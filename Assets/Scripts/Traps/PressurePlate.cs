using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Transform sprite;
    [SerializeField] private bool active;
    public bool toggle = false;
    public float duration;
    private bool pressed;
    private float counter;
    private float offset = 0.2f;
    private int objCnt;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    [Header("Events")]

    [Space]

    //public UnityEvent OnLandEvent;
    public BoolEvent OnActivate;




    // Start is called before the first frame update
    void Start()
    {
        if (OnActivate == null) OnActivate = new BoolEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (!toggle && objCnt == 0)
        {
            if (counter > 0) counter -= Time.deltaTime;
            else
            {
                if(active) OnActivate.Invoke(!active);
                active = false;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            objCnt++;
            if (objCnt > 0 && !pressed)
            {
                sprite.position += Vector3.down * offset;
                pressed = true;



                if (toggle)
                {
                    active = !active;
                    Debug.Log(active);
                    OnActivate.Invoke(active);
                }
                else
                {
                    active = true;
                    counter = duration;
                    OnActivate.Invoke(active);
                }


            }
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            objCnt--;
            if (objCnt == 0 && pressed)
            {
                sprite.position += Vector3.up * offset;
                pressed = false;
            }
        }
    }
}
