using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
   [Header("Parent GameObject till alla level-objekt in här")]
   [SerializeField] private GameObject myLevels;

   [Header("Linjen mellan levlarnas tjocklek")]
   [SerializeField] private GameObject myLine;
   [SerializeField] private Vector2 myLineThickness = new Vector2(0.1f, 0.1f);

   private GameObject[] myLineList;
   private GameObject mySelectedLevel;

   private void Start()
   {
      GenerateLines();
   }
   void Update()
   {
      if (Input.GetMouseButtonDown(0) && Input.touchCount <= 1)
      {
         mySelectedLevel = WhatDidIHit();
         Camera.main.gameObject.GetComponent<LevelSelectCamera>().SetFocus(mySelectedLevel);
      }
   }
   public GameObject WhatDidIHit()
   {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit))
      {
         Debug.Log(hit.collider.gameObject);
         return hit.collider.gameObject;
      }
      return null;
   }
   private void GenerateLines()
   {
      Transform[] levelTransforms = new Transform[myLevels.gameObject.transform.childCount];
      Vector3 linePos;
      Vector3 lineScale = Vector3.zero;
      Quaternion lineRot = Quaternion.identity;
      myLineList = new GameObject[myLevels.gameObject.transform.childCount];

      Debug.Log(myLevels.gameObject.transform.GetChild(0));
      Debug.Log(myLevels.gameObject.transform.GetChild(1));

      levelTransforms[0] = myLevels.gameObject.transform.GetChild(0);

      for (int i = 1; i < myLevels.gameObject.transform.childCount; i++)
      {
         levelTransforms[i] = myLevels.gameObject.transform.GetChild(i);

         linePos = levelTransforms[i - 1].position + (levelTransforms[i].position - levelTransforms[i - 1].position) / 2;


         lineScale.x = Mathf.Sqrt
         (
            Mathf.Pow
            (
               levelTransforms[i].position.x - levelTransforms[i - 1].position.x, 
               2
            )+
            Mathf.Pow
            (
               levelTransforms[i].position.y - levelTransforms[i - 1].position.y, 
               2
            )+ 
            Mathf.Pow
            (
               levelTransforms[i].position.z - levelTransforms[i - 1].position.z, 
               2
            ) 
         );
         lineScale.y = myLineThickness.x;
         lineScale.z = myLineThickness.y;

         lineRot = Quaternion.Euler
         (
            0, 
            -Mathf.Atan
            (
               (levelTransforms[i].position.z - levelTransforms[i - 1].position.z) / (levelTransforms[i].position.x - levelTransforms[i - 1].position.x)
            ) * 180 / Mathf.PI, 
            -Mathf.Atan
            (
               (levelTransforms[i].position.y - levelTransforms[i - 1].position.y) / (levelTransforms[i].position.x - levelTransforms[i - 1].position.x)
            ) * 180 / Mathf.PI
         );

         myLineList[i - 1] = Instantiate(myLine, linePos, lineRot, levelTransforms[i - 1]);
         myLineList[i - 1].transform.localScale = lineScale;
         myLineList[i - 1].SetActive(true);
      }
   }
}
