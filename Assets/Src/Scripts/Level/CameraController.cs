
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;

    private Vector3 offset;
    private Vector3 originalPostion;

    private bool fall;
    // Start is called before the first frame update
    void Start()
    {
        originalPostion = transform.position;
        offset = new Vector3(0, (float) 0.5, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (fall) 
        {
            transform.position = ball.transform.position + offset;   
        }
    }

    public void setFalling(bool falling)
    {
        fall = falling;
    }

    public void Reset()
    {
        transform.position = originalPostion;
        fall = false;
    }
}
