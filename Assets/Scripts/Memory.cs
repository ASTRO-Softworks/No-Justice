using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class Memory
{
	private Vector2 lastSeenPosition = Vector2.zero;
    private float velocity;
    public bool isSeen = false;

    public void setVelocity(float velocity)
    {
        this.velocity = velocity;
    }
    public float getVelocity()
    {
        return velocity;
    }
    public void setLastSeenPosition(Vector2 vector)
    {
        Debug.Log("Seen " + vector);
        isSeen = true;
        this.lastSeenPosition = vector;
    }
    public void setStartLastSeenPosition(Vector2 vector)
    {
       this.lastSeenPosition = vector;
    }
    public int getXDir(Vector2 position)
    {
        return position.x > lastSeenPosition.x ? -1 : 1;
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