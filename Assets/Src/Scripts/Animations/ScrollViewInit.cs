using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollViewInit : MonoBehaviour
{
    public Scrollbar scrollBar;
    
    void OnEnable()
    {
        StartCoroutine(ResetScrollView());
    }

    IEnumerator ResetScrollView()
    {
        yield return new WaitForSeconds(0.1f);

        scrollBar.value = 1;
    }
    
}
