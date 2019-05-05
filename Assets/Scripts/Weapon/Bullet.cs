using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour {

    private Vector2 startPosition;

    private float me_x;
    private float me_y;
    private float me_X;
    private float me_Y;
    void Start()
    {
        startPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position, 0.1f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Player"))
            {
                me_x = collider.gameObject.transform.position.x - collider.gameObject.GetComponent<BoxCollider2D>().size.x / 2 ;
                me_y = collider.gameObject.transform.position.y - collider.gameObject.GetComponent<BoxCollider2D>().size.y / 2 ;
                me_X = collider.gameObject.transform.position.x + collider.gameObject.GetComponent<BoxCollider2D>().size.x / 2 ;
                me_Y = collider.gameObject.transform.position.y + collider.gameObject.GetComponent<BoxCollider2D>().size.y / 2 ;
                
            }
        }
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
        if (hitInfo.isTrigger) return;
        if (
            transform.position.x > me_x &&
            transform.position.y > me_y &&
            transform.position.x < me_X &&
            transform.position.y < me_Y
            ) return;
        Debug.Log(hitInfo.gameObject.tag);
        Destroy(gameObject);
        if (!hitInfo.gameObject.CompareTag("Enemy") && !hitInfo.gameObject.CompareTag("Player")) return;
        hitInfo.gameObject.GetComponent<Stats>().Damage(1);
        hitInfo.gameObject.GetComponent<Stats>().SetMemory(startPosition);
    }


}
