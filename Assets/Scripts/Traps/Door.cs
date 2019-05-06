using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Collider2D collider;
    [SerializeField] SpriteRenderer sprite;
    bool open = false;
    int cnt = 0;

    /*
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    */
    public void Toggle()
    {
        if (cnt > 0)
        {
            cnt = 0;
            collider.isTrigger = true;
            sprite.color = new Color(1, 1, 1, 0);
        }
        else
        {
            cnt = 1;
            collider.isTrigger = false;
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    public void Open(bool flag)
    {
        cnt += flag?1:-1;
        Debug.Log("Door " + flag);
        open = cnt > 0;
        if (open)
        {
            collider.isTrigger = true;
            sprite.color = new Color(1, 1, 1, 0);
        }
        else
        {
            collider.isTrigger = false;
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    
}
