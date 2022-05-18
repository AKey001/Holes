using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightToBall : MonoBehaviour
{
    public GameObject ball;

    private Vector3 offset; 
    
    void Start()
    {
        offset = new Vector3(0, (float) 0.17, 0);
    }
    
    void Update()
    {
        transform.position = ball.transform.position + offset;
    }
}
