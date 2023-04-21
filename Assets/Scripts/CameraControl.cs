using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform JimmyBoi;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = JimmyBoi.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = JimmyBoi.position - offset;
        transform.LookAt(JimmyBoi);
        //print(JimmyBoi.position - offset);

    }
}
