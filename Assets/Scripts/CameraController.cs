using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float targetCameraWidth = 35f;
    public Text debugText;

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

    Plane[] frustumPlanes;

    float startingCameraWidth;

    float currentPadding;

    private bool myMultiTouch = false;

    bool shouldMoveToTopDownView = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        targetCameraWidth = (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(myTargets[0].bounds.size.x, 2) + Mathf.Pow(myTargets[0].bounds.size.z, 2))) / 1.86f);
        startingCameraWidth = targetCameraWidth;

        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);


        // Debug.developerConsoleVisible = true;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.matrix = Camera.main.transform.localToWorldMatrix;
        //Gizmos.DrawFrustum(new Vector3(0,0,Camera.main.nearClipPlane), Camera.main.)
    }

    private void LateUpdate()
    {

        bool Intersects = GeometryUtility.TestPlanesAABB(frustumPlanes, myTargets[0].bounds);

        Debug.Log(Intersects);


        if (Screen.orientation == ScreenOrientation.LandscapeLeft||Screen.orientation == ScreenOrientation.LandscapeRight && !shouldMoveToTopDownView)
        {
            if (myTargets[0] != null && !shouldMoveToTopDownView)
            {
                targetCameraWidth = (Mathf.Abs(Mathf.Sqrt(Mathf.Pow(myTargets[0].bounds.size.x, 2) + Mathf.Pow(myTargets[0].bounds.size.z, 2))) / 1.86f);

                Camera.main.orthographicSize = targetCameraWidth / Camera.main.aspect + myZoomPaddingLandscape;

                Debug.Log(targetCameraWidth);
            }
           
        }
        else if (Screen.orientation == ScreenOrientation.Portrait && !shouldMoveToTopDownView)
        {
            if (myTargets[0] != null)
            {
                Debug.Log(targetCameraWidth);

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
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * transitionSpeed);                              
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

        if (Input.GetKeyDown(KeyCode.Space) && Input.touchCount>1)
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
