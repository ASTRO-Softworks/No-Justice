using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFly_test : MonoBehaviour
{
    public float cameraSpeedMovement = 1f;

    void Update()
    {
        Vector2 flyMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 flyDirection = flyMovement.normalized;
        Vector2 flyForce = flyDirection * cameraSpeedMovement;
        transform.Translate(flyForce * Time.deltaTime);


    }
}
