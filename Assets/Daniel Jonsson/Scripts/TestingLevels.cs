using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingLevels : MonoBehaviour
{
    public static TestingLevels globalInstance;


    private void Awake()
    {
        if (globalInstance == null)
        {
            globalInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (globalInstance != this)
        {
            Destroy(gameObject);
        }
    }
}
