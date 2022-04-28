
using System;
using GooglePlayGames;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    private Vector3 _originalPosition;
    public GameObject platform;
    public GameObject ground;
    public CameraController cameraController;
    public TimeManager timeManager;
    
    void Start()
    {
        _originalPosition = transform.position;
    }

    // TODO reset platform position to position, not to zero
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            print("Respawn");
            
            timeManager.AddAttempt();
            gameObject.transform.position = _originalPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            platform.transform.rotation = Quaternion.Euler(Vector3.zero);
            ground.GetComponent<Collider>().enabled = true;
            cameraController.setFalling(false);
            cameraController.Reset();
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            print("Finish");
            
            TriggeredStar(other);
            
            gameObject.transform.position = _originalPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            platform.transform.rotation = Quaternion.Euler(Vector3.zero);
            timeManager.Finish();
        }
        if (other.gameObject.CompareTag("Fall"))
        {
            print("Fall");
            ground.GetComponent<Collider>().enabled = false;
            cameraController.setFalling(true);
            timeManager.AddFall();
        }
        if (other.gameObject.CompareTag("Star"))
        {
            TriggeredStar(other);
        }
    }

    private void TriggeredStar(Collider other)
    {
        print("Star");
        FindObjectOfType<AudioManager>().Play("Star");
        timeManager.AddStar();
        other.gameObject.SetActive(false);
    }
    
}