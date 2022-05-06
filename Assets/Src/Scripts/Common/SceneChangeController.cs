using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoBehaviour
{
    public void ChangeScene(string scene) {  
        SceneManager.LoadScene(scene);  
    }
}
