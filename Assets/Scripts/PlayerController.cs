using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    public Tile GetCurrectTile { get { return myCurrentTile; } }

    public List<Vector3> PlayerMoveList
    {
        get
        {
            return myMovementList;
        }
        set
        {
            step = 1;
            myMovementList = value;
        }
    }

    [SerializeField]
    PathManager myPathManager;
    List<Vector3> myMovementList;
    [SerializeField]
    ParticleSystem myDeathEffect;
    [SerializeField]
    int step = 1;
    [Range(1, 12)]
    [SerializeField]
    float myMovementSpeed = 12;

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
        float distance = Vector3.Distance(myPathManager.GetPortals[0].GetPos() + Vector3.right, transform.position);
        if (Input.GetKey(KeyCode.Space))
        {
            if (step == myMovementList.Count)
            {
                Debug.Log("You win");
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, myMovementList[step], myMovementSpeed * Time.deltaTime);
                Vector3 distanceToNextPos = myMovementList[step] - transform.position;

                if (distanceToNextPos.magnitude < 0.05f)
                {
                    if (step <= myMovementList.Count)
                    {
                        step++;

                    }
                    else
                    {
                        myDeathEffect.transform.position = transform.position;
                        gameObject.SetActive(false);
                        myDeathEffect.Play();
                    }
                }
            }
            if (distance < 0.1f)
            {
                transform.position = myPathManager.GetPortals[0].GetExit() + myPathManager.GetPortals[0].transform.position;

                myMovementList = myPathManager.GetPortals[0].GetMovementList();
                step = 1;
            }

        }
    }
}
