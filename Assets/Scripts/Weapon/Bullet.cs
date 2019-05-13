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
    private bool cankill = false;
    void Start()
    {
        
        startPosition = transform.position;
        /*var colliders = Physics2D.OverlapCircleAll((Vector2)transform.position, 0.5f);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Player"))
            {
                Debug.Log(collider.gameObject.GetComponent<BoxCollider2D>().size);
                me_x = collider.gameObject.transform.position.x - collider.gameObject.GetComponent<BoxCollider2D>().size.x / 1.5f ;
                me_y = collider.gameObject.transform.position.y - collider.gameObject.GetComponent<BoxCollider2D>().size.y ;
                me_X = collider.gameObject.transform.position.x + collider.gameObject.GetComponent<BoxCollider2D>().size.x / 1.5f ;
                me_Y = collider.gameObject.transform.position.y + collider.gameObject.GetComponent<BoxCollider2D>().size.y;
                
            }
        }*/
    }

    public string startTeg;
    
    private bool isStay = true;
/*    private void OnCollisionStay2D(Collision2D other)
    {

        if (!other.gameObject.CompareTag(startTeg))
        {
            cankill = true;
            Debug.Log("cankill");
        }

    }
*/
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(startTeg))
            cankill = true;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
 
        
        if (hitInfo.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
        if (hitInfo.isTrigger) return;
        Destroy(gameObject);
        if (!hitInfo.CompareTag(startTeg)) cankill = true;
        if (!cankill)return;

        Debug.Log(hitInfo.gameObject.tag);
        if (!hitInfo.gameObject.CompareTag("Enemy") && !hitInfo.gameObject.CompareTag("Player")) return;
        hitInfo.gameObject.GetComponent<Character.Stats>().Damage(1);
        hitInfo.gameObject.GetComponent<Character.Stats>().SetMemory(startPosition);
    }


}
