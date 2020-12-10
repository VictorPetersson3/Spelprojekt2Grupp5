using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCamera : MonoBehaviour
{

   private float myTopDownZoom = 10f;

   private Vector3 myTopDownFocus = Vector3.zero;
   private Vector3 myFocusOffset = Vector3.zero;
   private Vector3 myTopDownOffset = Vector3.zero;
   private Vector3 myTouchStart = Vector3.zero;


   [Header("Speed Variables")]
   [SerializeField] private float myCameraSpeed = 1f;
   [SerializeField] private float myTransitionSpeed = 5f;

   [Header("Top down camera zoom range")]
   [SerializeField] private float myMinZoom = 3f;
   [SerializeField] private float myMaxZoom = 10f;

   [Header("Top down camera pan range")]
   [SerializeField] private Vector3 myMinPan = new Vector3(-25f, 0, -25f);
   [SerializeField] private Vector3 myMaxPan = new Vector3(25f, 0, 25f);

   [Header("Default zoom of both cameras")]
   [SerializeField] private float myOrtographicSizeTopDown = 5f;
   [SerializeField] private float myOrtographicSizeFocus = 2f;

   [Header("Default offset for focus camera")]
   [SerializeField] private Vector3 myFocusPosition = new Vector3(0, 0.92f, -1.82f);
   [SerializeField] private Quaternion myFocusRotation = Quaternion.Euler(26, 0, 0);

   [Header("Default offset for top down camera")]
   [SerializeField] private Vector3 myTopDownPosition = new Vector3(0, 10, -3);
   [SerializeField] private Quaternion myTopDownRotation = Quaternion.Euler(75, 0, 0);
    [Header("UI Camera")]
    [SerializeField] private Camera myUICamera;


    private GameObject myObjectInFocus = null;

   private void Start()
   {
      myTopDownFocus = transform.position;
      myFocusOffset = myFocusPosition;
      myTopDownOffset = myTopDownPosition;
   }
   void Update()
   {
        myUICamera.orthographicSize = Camera.main.orthographicSize;
      if (myObjectInFocus != null)
      {
         myTopDownFocus = myObjectInFocus.transform.position + myTopDownOffset;
         myFocusPosition = myObjectInFocus.transform.position + myFocusOffset;

         transform.position = Vector3.Lerp(transform.position, myFocusPosition, Time.deltaTime * myTransitionSpeed);
         transform.rotation = Quaternion.Lerp(transform.rotation, myFocusRotation, Time.deltaTime * myTransitionSpeed);
         Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, myOrtographicSizeFocus, Time.deltaTime * myTransitionSpeed);
      }
      else
      {
         transform.position = Vector3.Lerp(transform.position, new Vector3(myTopDownFocus.x, myTopDownZoom, myTopDownFocus.z), Time.deltaTime * myTransitionSpeed);
         transform.rotation = Quaternion.Lerp(transform.rotation, myTopDownRotation, Time.deltaTime * myTransitionSpeed);
         Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, myOrtographicSizeTopDown, Time.deltaTime * myTransitionSpeed);
      }
      if (Input.GetMouseButtonDown(0))
      {
         myTouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      }
      if (Input.touchCount == 2)
      {
         Touch touchZero = Input.GetTouch(0);
         Touch touchOne = Input.GetTouch(1);

         Vector2 touchZPrevPos = touchZero.position - touchZero.deltaPosition;
         Vector2 touchOPrevPos = touchOne.position - touchOne.deltaPosition;

         float prevMagnitude = (touchZPrevPos - touchOPrevPos).magnitude;
         float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

         float diff = currentMagnitude - prevMagnitude;
         Zoom(diff * 0.1f);
      }
      else if (Input.GetMouseButton(0))
      {
         Vector3 direction = myTouchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
         direction.y = 0;
         myTopDownFocus += direction * myCameraSpeed;
         transform.position += direction;  //gör kameran lite mer statisk i sitt följande
      }

      if (Camera.main.transform.position.x > myMaxPan.x)
      {
         myTopDownFocus.x = myMaxPan.x;
      }
      else if (Camera.main.transform.position.x < myMinPan.x)
      {
         myTopDownFocus.x = myMinPan.x;

      }
      if (Camera.main.transform.position.z > myMaxPan.z)
      {
         myTopDownFocus.z = myMaxPan.z;

      }
      else if (Camera.main.transform.position.z < myMinPan.z)
      {
         myTopDownFocus.z = myMinPan.z;

      }
   }
   public void SetFocus(GameObject aGameObject)
   {
      myObjectInFocus = aGameObject;
   }
   void Zoom(float increment)
   {
      myOrtographicSizeTopDown = Mathf.Clamp(Camera.main.orthographicSize - increment, myMinZoom, myMaxZoom);
   }
}
