

using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 _originalPosition;
    public GameObject platform;
    public CameraController cameraController;
    public LevelManager levelManager;
    
    void Start()
    {
        _originalPosition = transform.position;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            levelManager.AddAttempt();
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
            levelManager.Finish();
        }
        if (other.gameObject.CompareTag("Fall"))
        {
            FindObjectOfType<AudioManager>().Play("Fall");    
            cameraController.setFalling(true);
            levelManager.AddFall();
        }
        if (other.gameObject.CompareTag("Star"))
        {
            TriggeredStar(other);
        }
    }

    private void TriggeredStar(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Star");
        levelManager.AddStar();
        other.gameObject.SetActive(false);
    }
    
}