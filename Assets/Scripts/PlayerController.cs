using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    public Tile GetCurrectTile { get { return myCurrentTile; } }

    public List<PathTile> myMovementList;
    [SerializeField]
    ParticleSystem myDeathEffect;
    int step = 1;
    [Range(1, 8)]
    [SerializeField]
    float myMovementSpeed = 8;
    void Start()
    {
        myMovementList = new List<PathTile>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (myMovementList[step].IsEndTile)
            {
                Debug.Log("You win");

            }

            transform.position = Vector3.MoveTowards(transform.position, myMovementList[step].GetPathTilePosition, myMovementSpeed * Time.deltaTime);
            Vector3 distanceToNextPos = myMovementList[step].GetPathTilePosition - transform.position; 
            
            if (distanceToNextPos.magnitude < 0.05f)
            {
                if (step < myMovementList.Count - 1)
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
