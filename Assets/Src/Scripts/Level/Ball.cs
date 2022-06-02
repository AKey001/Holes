
using System;
using GooglePlayGames;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    private Vector3 _originalPosition;
    public GameObject platform;
    public CameraController cameraController;
    public TimeManager timeManager;
    
    void Start()
    {
        _originalPosition = transform.position;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            timeManager.AddAttempt();
            gameObject.transform.position = _originalPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            platform.transform.rotation = Quaternion.Euler(Vector3.zero);
            cameraController.setFalling(false);
            cameraController.Reset();
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            TriggeredStar(other);
            
            gameObject.transform.position = _originalPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            platform.transform.rotation = Quaternion.Euler(Vector3.zero);
            timeManager.Finish();
        }
        if (other.gameObject.CompareTag("Fall"))
        {
            FindObjectOfType<AudioManager>().Play("Fall");    
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
        FindObjectOfType<AudioManager>().Play("Star");
        timeManager.AddStar();
        other.gameObject.SetActive(false);
    }
    
}