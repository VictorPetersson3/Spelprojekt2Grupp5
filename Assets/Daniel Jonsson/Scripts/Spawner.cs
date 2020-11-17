using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    BuildManager myBuildManager;

    private void Start()
    {
        myBuildManager = BuildManager.globalInstance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            myBuildManager.ResetTiles();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out hit))
        {
            Transform objectHit = hit.transform;


            if (objectHit != null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameObject gameObj = myBuildManager.SpawnFromPool("Sphere", Quaternion.identity);
                    gameObj.transform.position = objectHit.position;
                }
                else if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    GameObject gameObj2 = myBuildManager.SpawnFromPool("Cube", Quaternion.identity);
                    gameObj2.transform.position = objectHit.position;

                }

            }
            else
            {
                Debug.Log("NO OBJECT FOUND, OBJECT IS: " + objectHit);
            }
           
        }

    }
}
