using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public int level;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bounce"))
        {
            float force = collision.relativeVelocity.magnitude;
            float volume = 1;
            if (force <= 0.3)
            {
                volume = force * 3;
            } else if (force <= 1)
            {
                volume = force;
            }

            print("FORCE: " + force);
            print("VOLUME: " + volume);
            
            FindObjectOfType<AudioManager>().Play(Keystore.audioBounce(level), volume);    
        }
    }
}
