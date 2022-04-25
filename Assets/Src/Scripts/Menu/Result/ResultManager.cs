using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public List<Result> _results;
    // Start is called before the first frame update
    void Start()
    {
        Result result = _results[0];
        result.star1.SetActive(true);
        result.star2.SetActive(false);
        result.star3.SetActive(false);
    }
}
