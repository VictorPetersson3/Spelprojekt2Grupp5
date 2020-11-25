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
            

            myMovementList = value;
        }
    }

    List<Vector3> myMovementList;
    [SerializeField]
    ParticleSystem myDeathEffect;
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
                Vector3 distanceToNextPos = myMovementList[step] - transform.position; 
            
                if (distanceToNextPos.magnitude < 0.05f)
                {
                    if (step < myMovementList.Count)
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

        }
    }
}
