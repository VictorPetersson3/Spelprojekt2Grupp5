using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
   LevelSelectCamera levelSelectCamera;
   void Update()
   {
      if (Input.GetMouseButtonDown(0) || Input.touchCount == 1)
      {
         Debug.Log("Clicked");
         Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

         RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
         Debug.Log(hit.collider.gameObject.name);
         Camera.main.gameObject.GetComponent<LevelSelectCamera>().SetFocus(hit.collider.gameObject);

      }
   }
}
