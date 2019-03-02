using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject Anchor;//Object? to which camera attached
    const float cFOWmul = 5.0f;
    public GameObject FOWManage;
    // Use this for initialization
    void Start()
    {
        FOWManage.transform.localScale = new Vector3(Camera.main.orthographicSize * cFOWmul, Camera.main.orthographicSize * cFOWmul / 2);
        //FOW.bounds.size.x = Camera.main.orthographicSize * 5.0;
        //gameObject.GetComponent<Camera>().orthographicSize
        //

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v;
        v.x = 0;
        v.y = 0;
        v.z = -10;
        transform.localPosition = Anchor.transform.localPosition + v;
        /*
        Vector3 dest = Anchor.transform.position-Self.transform.position;
        Self.*/

    }
}