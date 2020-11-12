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

    private bool myMultiTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myMultiTouch = false;
            myTouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.touchCount == 2)
        {
            myMultiTouch = true;

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDistance = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentTouchDistance = (touchZero.position - touchOne.position).magnitude;

            float difference = currentTouchDistance - prevTouchDistance;

            Zoom(difference * 0.002f);
        }

        else if(Input.GetMouseButton(0) && myMultiTouch == false)
        {
            Vector3 direction = myTouchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void Zoom(float aZoomIncrement)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - aZoomIncrement, myZoomOutMin, myZoomOutMax);
    }
}
