﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class LevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject myMainUI;
    [Header("Linjen mellan levlarnas tjocklek")]
    [SerializeField] private GameObject myLine;
    [SerializeField] private Vector2 myLineThickness = new Vector2(0.1f, 0.1f);

    private GameObject[] myLineList;
    private GameObject mySelectedLevel;
    private GameObject mySelectedUI;
    private int myCurrentStars;
    [SerializeField] private int[] myStarRequirement;
    [SerializeField] private GameObject myBackground;
    [SerializeField] private GameObject myLines;

    private void Start()
    {
        Unlock(GameManager.globalInstance.GetTotalStars());
        GenerateLines();
        mySelectedUI = myMainUI;
    }
    void Update()
    {

        if (Input.GetMouseButtonUp(0) && Input.touchCount <= 1)
        {

            mySelectedLevel = WhatDidIHit();
            if (mySelectedLevel != myBackground)
            {
                Invoke("DeactivateLines", 0.1f);
                myMainUI.SetActive(false);
                mySelectedUI = mySelectedLevel.transform.parent.GetChild(0).gameObject;
                mySelectedUI.SetActive(true);
                Camera.main.gameObject.GetComponent<LevelSelectCamera>().SetFocus(mySelectedLevel);
            }
            else
            {
                Invoke("ActivateLines", 0.1f);
                mySelectedUI.SetActive(false);
                myMainUI.SetActive(true);
                StartCoroutine(CoRoutineLoad());
            }
        }
    }
    IEnumerator CoRoutineLoad()
    {
        yield return new WaitForSeconds(0.1f);

        Camera.main.gameObject.GetComponent<LevelSelectCamera>().SetFocus(null);
    }
    public GameObject WhatDidIHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
    private void GenerateLines()
    {
        Transform[] levelTransforms = new Transform[transform.childCount];
        Vector3 linePos;
        Vector3 lineScale = Vector3.zero;
        Quaternion lineRot;
        myLineList = new GameObject[transform.childCount];

        levelTransforms[0] = transform.GetChild(0).GetChild(2);

        for (int i = 1; i < transform.childCount; i++)
        {
            levelTransforms[i] = transform.GetChild(i).GetChild(2);

            linePos = levelTransforms[i - 1].position + (levelTransforms[i].position - levelTransforms[i - 1].position) / 2;


            lineScale.x = Mathf.Sqrt
            (
               Mathf.Pow
               (
                  levelTransforms[i].position.x - levelTransforms[i - 1].position.x,
                  2
               ) +
               Mathf.Pow
               (
                  levelTransforms[i].position.y - levelTransforms[i - 1].position.y,
                  2
               ) +
               Mathf.Pow
               (
                  levelTransforms[i].position.z - levelTransforms[i - 1].position.z,
                  2
               )
            );
            lineScale.y = myLineThickness.x;
            lineScale.z = myLineThickness.y;

            if ((levelTransforms[i].position.x - levelTransforms[i - 1].position.x) != 0)
            {
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
            }
            else
            {
                lineRot = Quaternion.Euler(0, 90, 0);
            }
            myLineList[i - 1] = Instantiate(myLine, linePos, lineRot, levelTransforms[i - 1]);
            myLineList[i - 1].transform.localScale = lineScale;
            myLineList[i - 1].SetActive(true);
        }
    }
    private void Unlock(int anAmountOfStars)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetChild(1).gameObject.activeSelf)
            {
                if (anAmountOfStars >= myStarRequirement[i])
                {
                    transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }
    public int[] GetStarRequirements()
    {
        return myStarRequirement;
    }
    private void DeactivateLines()
    {
        myLines.SetActive(false);
    }
    private void ActivateLines()
    {
        myLines.SetActive(true);
    }
}
