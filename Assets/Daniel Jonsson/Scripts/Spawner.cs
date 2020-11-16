using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    BuildManager myBuildManager;
    [SerializeField]
    LayerMask layerMask;

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
                    myBuildManager.SpawnFromPool("Sphere", objectHit.position + new Vector3(0 , (objectHit.localScale.y / 2), 0 ), Quaternion.identity);
                }
                
            }
            else
            {
                Debug.Log("NO OBJECT FOUND, OBJECT IS: " + objectHit);
            }
           
        }

    }
}
