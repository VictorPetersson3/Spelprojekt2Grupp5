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

    private void FixedUpdate()
    {
        myBuildManager.SpawnFromPool("Sphere", transform.position, Quaternion.identity);
    }
}
