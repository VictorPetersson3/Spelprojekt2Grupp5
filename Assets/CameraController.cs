using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 myTouchStart;
    Vector3 originalPosition;
    Quaternion originalRotation;

    //[SerializeField]
    //float myZoomOutMin = 1;
    //[SerializeField]
    //float myZoomOutMax = 8;

    public Renderer[] myTargets;

    [SerializeField]
    float myZoomPaddingPortrait = -20f;
    [SerializeField]
    float myZoomPaddingLandscape = 2f;
    [SerializeField]
    float myZoomPaddingTopDown = 22f;
    [SerializeField]
    float transitionSpeed = 5f;
    [SerializeField]
    Vector3 myWorldCenterPostion = Vector3.zero;


    float currentPadding;

    private bool myMultiTouch = false;

    bool shouldMoveToTopDownView = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void LateUpdate()
    {               
        if(Screen.orientation == ScreenOrientation.LandscapeLeft||Screen.orientation == ScreenOrientation.LandscapeRight && !shouldMoveToTopDownView)
        {
            Camera.main.orthographicSize = ((myTargets[0].bounds.size.z / Camera.main.aspect) + myZoomPaddingLandscape);
            Debug.Log("In landscape mode");
        }
        else if (Screen.orientation == ScreenOrientation.Portrait && !shouldMoveToTopDownView)
        {
            Camera.main.orthographicSize = ((myTargets[0].bounds.size.z / Camera.main.aspect) - myZoomPaddingPortrait);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldMoveToTopDownView)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(myWorldCenterPostion.x, transform.position.y, myWorldCenterPostion.z), Time.deltaTime* transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 0, 0), Time.deltaTime* transitionSpeed);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, myZoomPaddingTopDown, Time.deltaTime * transitionSpeed);
        }

        if (!shouldMoveToTopDownView)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * transitionSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * transitionSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveToTopDownView();
        }

        if(Input.GetMouseButtonDown(0))
        {
            myTouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        else if(Input.GetMouseButton(0))
        {
            Vector3 direction = myTouchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight && !shouldMoveToTopDownView)
        {
            //Camera.main.orthographicSize = ((myTargets[0].bounds.size.z / Camera.main.aspect) + myZoomPaddingLandscape);
            //Debug.Log("In landscape mode");

            currentPadding = myZoomPaddingLandscape;
        }
        else if (Screen.orientation == ScreenOrientation.Portrait && !shouldMoveToTopDownView)
        {
            //Camera.main.orthographicSize = ((myTargets[0].bounds.size.z / Camera.main.aspect) - myZoomPaddingPortrait);
            currentPadding = myZoomPaddingPortrait;
        }
    }

    public void MoveToTopDownView()
    {
        shouldMoveToTopDownView = !shouldMoveToTopDownView;
    }
}
