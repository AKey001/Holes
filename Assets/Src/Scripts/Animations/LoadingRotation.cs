using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotation : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        
        gameObject.LeanRotateAroundLocal(Vector3.forward, -360f, 2f).setLoopType(LeanTweenType.linear);
    }
    
    private void OnDisable()
    {
        gameObject.LeanCancel();
    }
}
