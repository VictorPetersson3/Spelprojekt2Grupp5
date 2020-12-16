using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FpsCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI myText;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float avgFrameRate = Time.frameCount / Time.time;

        myText.SetText(avgFrameRate.ToString("0.0"));


    }
}
