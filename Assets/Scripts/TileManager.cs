using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> myTargetGoals = new List<GameObject>();
    [SerializeField]
    World myWorld;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddTargetGoals(GameObject aTargetGoal)
    {
        myTargetGoals.Add(aTargetGoal);
    }
}
