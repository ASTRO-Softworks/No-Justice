using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class Memory
{
	private Vector2 lastSeenPosition = Vector2.zero;
    private float velocity;
    private float velocityY;
    public bool isSeen = false;
    private double time;
    public void setVelocity(float velocity)
    {
        this.velocity = velocity;
    }
    public float getVelocity()
    {
        return velocity;
    } 
    public void setVelocityY(float velocity)
    {
        this.velocityY = velocity;
    }

    public void Forget()
    {
        Debug.Log(Time.time - time);
        if (Time.time - time > 5) isSeen = false;
        Debug.Log(isSeen);
    }

    public float getVelocityY()
    {
        return velocityY;
    }
    public void setLastSeenPosition(Vector2 vector)
    {
        isSeen = true;
        this.lastSeenPosition = vector;

        time = Time.time;
    }
    public void setStartLastSeenPosition(Vector2 vector)
    {
       this.lastSeenPosition = vector;
    }
    public int getXDir(Vector2 position)
    {
        return position.x > lastSeenPosition.x ? -1 : 1;
    }
    public int getYDir(Vector2 position)
    {
        return position.y > lastSeenPosition.y ? -1 : 1;
    }
    public double getDiference(Vector2 position)
    {
        return (Math.Sqrt((position.x - lastSeenPosition.x) * (position.x - lastSeenPosition.x) + (position.y - lastSeenPosition.y) * (position.y - lastSeenPosition.y)));
    }

    public Vector2 changeMoveDirection(Vector2 position)
    {
        Vector2 direction = new Vector2((position.x - lastSeenPosition.x) % velocity, (position.y - lastSeenPosition.y) % velocity);
		return direction;
    }
}