using System.Numerics;
using UnityEngine;

public class Placement
{
   UnityEngine.Vector3 myInputCoordinate;
   Touch myTouch;
   void PlacementLoop(bool aLoopState)
   {
      while (aLoopState)
      {
         GetFlooredInput();
         
      }

   }

   void GetFlooredInput()
   {
      myTouch = Input.GetTouch(0);
      myInputCoordinate = Camera.main.ScreenToWorldPoint(myTouch.position);
      myInputCoordinate.z = 0f;
      UnityEngine.Vector3Int.FloorToInt(myInputCoordinate);
   }
   void ExitLoop()
   {

   }
}
