using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRolling : MonoBehaviour
{
    public int level;

    private Rigidbody rigidbody; 
    private AudioManager audioManager;
    private bool grounded;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioManager = FindObjectOfType<AudioManager>();
        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Base"))
        {
            grounded = true;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Base"))
        {
            grounded = false;
        }
    }

    void Update()
    {
//     if (grounded)
//     {
//         float velocity = rigidbody.velocity.magnitude;
//         float volume = Mathf.Clamp01(velocity / 0.25f);
//         
//         if (velocity > 0.03)
//         {
//             audioManager.PlayIfNotPlaying(Keystore.audioRolling(level), volume);
//         }
//         else
//         {
//             audioManager.Stop(Keystore.audioRolling(level));
//         }
//     }
//     else
//     {
//         audioManager.Stop(Keystore.audioRolling(level));
//     }
    }
}
