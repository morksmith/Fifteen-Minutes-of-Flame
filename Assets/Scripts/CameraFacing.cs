using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacing : MonoBehaviour
{

    private Transform cam;
    void Start()
    {
        cam = GameObject.Find("Main Camera").transform;

    }

    void Update()
    {
        transform.forward = cam.forward;

    }
}
