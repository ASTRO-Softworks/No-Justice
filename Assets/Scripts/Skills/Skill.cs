using UnityEngine;

public class Skill : MonoBehaviour
{
    public float skillRate;
    public virtual void Active()
    {
        Debug.Log("KAVABANGA!!!");
    }

}
