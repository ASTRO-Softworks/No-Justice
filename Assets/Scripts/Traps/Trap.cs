using UnityEngine;

public class TRAP : MonoBehaviour
{
    public Vector2 teleportPoint;
    void OnTriggerEnter2D(Collider2D Robot)
    {
        if (Robot.gameObject.CompareTag("Player") || Robot.gameObject.CompareTag("Enemy"))
            Robot.gameObject.GetComponent<Rigidbody2D>().AddForce(teleportPoint);
        //Rigidbody2D.MovePosition(transform.position + transform.forward * Time.deltaTime);
    }
}