using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 myTouchStart;

    [SerializeField]
    float myZoomOutMin = 1;
    [SerializeField]
    float myZoomOutMax = 8;

    //[SerializeField]
    //Transform[] myTargets;

    public Renderer[] myTargets;

    [SerializeField]
    float myZoomPaddingPortrait = -20f;
    [SerializeField]
    float myZoomPaddingLandscape = 2f;

    private bool myMultiTouch = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void LateUpdate()
    {               
        if(Screen.orientation == ScreenOrientation.LandscapeLeft||Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            Camera.main.orthographicSize = ((myTargets[0].bounds.size.z / Camera.main.aspect) + myZoomPaddingLandscape);
            Debug.Log("In landscape mode");
        }
        else if (Screen.orientation == ScreenOrientation.Portrait)
        {
            Camera.main.orthographicSize = ((myTargets[0].bounds.size.z / Camera.main.aspect) - myZoomPaddingPortrait);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myMultiTouch = false;
            myTouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        else if(Input.GetMouseButton(0) && myMultiTouch == false)
        {
            Vector3 direction = myTouchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
    }
}
