using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCamera : MonoBehaviour
{
   [SerializeField] private float transitionSpeed = 5f;

   private float myZoomPaddingTopDown = 22f;
   private float myCurrentZoom = 10f;

   private Vector3 myWorldCenterPostion = Vector3.zero;
   private Vector3 myTouchStart;
   private Vector3 myFocusOffset = Vector3.zero;
   [SerializeField] private Vector3 myFocusPosition = Vector3.zero;
   [SerializeField] private Quaternion myFocusRotation = Quaternion.identity;

   private GameObject myObjectInFocus = null;

   private void Start()
   {
      myWorldCenterPostion = transform.position;
      myFocusOffset = myFocusPosition;
   }
   void Update()
   {
      if (myObjectInFocus != null)
      {
         myFocusPosition = myObjectInFocus.transform.position + myFocusOffset;
         transform.position = Vector3.Lerp(transform.position, myFocusPosition, Time.deltaTime * transitionSpeed);
         transform.rotation = Quaternion.Lerp(transform.rotation, myFocusRotation, Time.deltaTime * transitionSpeed);
      }
      else
      {
         myFocusPosition = myFocusOffset;
         transform.position = Vector3.Lerp(transform.position, new Vector3(myWorldCenterPostion.x, myCurrentZoom, myWorldCenterPostion.z), Time.deltaTime * transitionSpeed);
         transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(75, 0, 0), Time.deltaTime * transitionSpeed);
         Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, myZoomPaddingTopDown, Time.deltaTime * transitionSpeed);
      }
      //if (Input.GetMouseButtonDown(0))
      //{
      //   myTouchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      //}

      //else if (Input.GetMouseButton(0))
      //{
      //   Vector3 direction = myTouchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
      //   myWorldCenterPostion += direction;
      //}
   }
   public void SetFocus(GameObject aGameObject)
   {
      myObjectInFocus = aGameObject;
   }
   public void Pan(Vector3 aPosition)
   {
      myWorldCenterPostion += aPosition;
   }
   public void Zoom()
   {

   }
}
