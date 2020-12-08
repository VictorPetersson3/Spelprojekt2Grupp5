using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCoin : MonoBehaviour
{

    [SerializeField]
    private float myRotatingSpeed;

    void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360, myRotatingSpeed).setLoopClamp();
    }
}
