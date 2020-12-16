using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    Vector3 myTouchStart;
    Vector3 originalPosition;
    Quaternion originalRotation;

    public Renderer[] myTargets;

    public float targetCameraWidth = 35f;



    [SerializeField]
    float myZoomPaddingPortrait = -20f;
    [SerializeField]
    float myZoomPaddingLandscape = 2f;
    [SerializeField]
    float myZoomPaddingTopDownPortrait = 22f;
    [SerializeField]
    float myZoomPaddingTopDownLandscape = 22f;
    [SerializeField]
    float transitionSpeed = 5f;
    [SerializeField]
    Vector3 myWorldCenterPostion = Vector3.zero;

    [SerializeField]
    float myCameraYRotation = 0;

    float startingCameraWidth;

    float currentPadding;

    private bool myMultiTouch = false;

    bool shouldMoveToTopDownView = true;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = Camera.main.transform.position;
        originalRotation = transform.rotation;

        targetCameraWidth = (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(myTargets[0].bounds.size.x, 2) + Mathf.Pow(myTargets[0].bounds.size.z, 2))) / 1.86f);
        startingCameraWidth = targetCameraWidth;
    }

    private void LateUpdate()
    {    
        if (Screen.orientation == ScreenOrientation.LandscapeLeft||Screen.orientation == ScreenOrientation.LandscapeRight && !shouldMoveToTopDownView)
        {
            if (myTargets[0] != null && !shouldMoveToTopDownView)
            {
                targetCameraWidth = (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(myTargets[0].bounds.size.x, 2) + Mathf.Pow(myTargets[0].bounds.size.z, 2))) / 1.86f);

                Camera.main.orthographicSize = targetCameraWidth / Camera.main.aspect + myZoomPaddingLandscape;

            }
           
        }
        else if (Screen.orientation == ScreenOrientation.Portrait && !shouldMoveToTopDownView)
        {
            if (myTargets[0] != null)
            {

                targetCameraWidth = (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(myTargets[0].bounds.size.x, 2) + Mathf.Pow(myTargets[0].bounds.size.z, 2)))/1.86f);


                Camera.main.orthographicSize = targetCameraWidth / Camera.main.aspect + myZoomPaddingPortrait;
            }            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldMoveToTopDownView)
        {
            if (!Input.GetMouseButtonDown(0))
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(myWorldCenterPostion.x, transform.position.y, myWorldCenterPostion.z), Time.deltaTime * transitionSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(65, myCameraYRotation, 0), Time.deltaTime * transitionSpeed);     
                switch(Screen.orientation)
                {
                    case ScreenOrientation.Portrait:
                        Camera.main.orthographicSize = targetCameraWidth / Camera.main.aspect + myZoomPaddingTopDownPortrait;
                        break;
                    case ScreenOrientation.LandscapeLeft:
                        Camera.main.orthographicSize = targetCameraWidth / Camera.main.aspect + myZoomPaddingTopDownLandscape;
                        break;
                    case ScreenOrientation.LandscapeRight:
                        Camera.main.orthographicSize = targetCameraWidth / Camera.main.aspect + myZoomPaddingTopDownLandscape;
                        break;
                }
            }
        }

        if (!shouldMoveToTopDownView)
        {
            if(!Input.GetMouseButtonDown(0))
            {
                transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * transitionSpeed);

                transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * transitionSpeed);             
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveToTopDownView();
        }



        //if(Input.GetMouseButtonDown(0))
        //{
        //    myTouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}
        //
        //else if(Input.GetMouseButton(0))
        //{
        //    Vector3 direction = myTouchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    transform.position += direction;
        //}
    }

    public void MoveToTopDownView()
    {
        shouldMoveToTopDownView = !shouldMoveToTopDownView;
    }

    public void StopMovingToTopDownView()
    {
        shouldMoveToTopDownView = false;
    }
}
