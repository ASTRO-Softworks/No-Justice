using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject Anchor;//Object? to which camera attached
                             //    const float cFOWmul = 5.0f;
                             //    public GameObject FOWManage;
    Vector3 modifyVector = new Vector3(0, 0, -10);
    // Update is called once per frame
    void Update()
    {
        if (Anchor != null)
        {
            transform.localPosition = Anchor.transform.localPosition + modifyVector;
        }
    }
}