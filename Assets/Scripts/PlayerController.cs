﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    public Tile GetCurrectTile { get { return myCurrentTile; } }
    bool t = false;
    public void PlayerMoveList(List<Vector3> aListToCopy, PathTileIntersection aPathTileIntersection)
    {

        for (int i = 0; i < aListToCopy.Count; i++)
        {
            Debug.Log(i + ". " + aListToCopy[i], gameObject);
        }
        if (!t)
        {
            step = 0;
            t = true;
        }
        else if (aPathTileIntersection != null)
        {
            step = 1;
            t = true;
        }

        myMovementList = aListToCopy;

    }

    List<Vector3> myMovementList;
    [SerializeField]
    ParticleSystem myDeathEffect;
    [SerializeField]
    int step = 1;
    [Range(1, 8)]
    [SerializeField]
    float myMovementSpeed = 8;

    public int SetPlayerStep
    {
        set
        {
            step = value;
        }
    }
    void Start()
    {
        myMovementList = new List<Vector3>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (step == myMovementList.Count)
            {
                Debug.Log("You win");
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, myMovementList[step], myMovementSpeed * Time.deltaTime);

                Quaternion lookAtRotation = Quaternion.LookRotation(myMovementList[step] - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime / 0.1f);

                Vector3 distanceToNextPos = myMovementList[step] - transform.position;

                if (distanceToNextPos.magnitude < 0.1f)
                {
                    if (step <= myMovementList.Count)
                    {
                        step++;
                        t = false;
                    }
                    else
                    {
                        myDeathEffect.transform.position = transform.position;
                        gameObject.SetActive(false);
                        myDeathEffect.Play();
                    }
                }
            }

        }
    }
}
