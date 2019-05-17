using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactive : MonoBehaviour
{
    /*
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    */
    [Header("Events")]

    [Space]
    
    public UnityEvent OnInteract;



    
    // Start is called before the first frame update
    void Start()
    {
        if (OnInteract == null) OnInteract = new UnityEvent();
    }
    public void Interact()
    {
        OnInteract.Invoke();
        Debug.Log("Tap");
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
