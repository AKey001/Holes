using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RDG;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public int level;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bounce"))
        {
            float volume = Mathf.Clamp01(collision.relativeVelocity.magnitude / 0.25f);

            if (collision.relativeVelocity.magnitude < 0.05)
            {
                volume = 0.2f;
            }
            
            FindObjectOfType<AudioManager>().Play(Keystore.audioBounce(level), volume);    
        }
    }
}
