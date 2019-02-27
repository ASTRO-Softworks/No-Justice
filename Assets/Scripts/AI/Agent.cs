/*
 * Developer: Sergeev Sergey
 * Version:1.0.0
 * class for AI main
 */
using UnityEngine;
using System.Collections;

public class Agent:MonoBehaviour
{
    public Vector3 velocity;
    protected Steering steering;
    void Start()
    {
        velocity = Vector3.zero;
        steering = new Steering();
    }
    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }
}