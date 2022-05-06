using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeLoader : MonoBehaviour
{
    private Animator transition;
    
    public float transitionTime;

    void Start()
    {
        transition = GetComponent<Animator>();
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(Load(scene));
    }

    IEnumerator Load(string scene)
    {
        transition.SetTrigger("Switch");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene);
    }
    
}
