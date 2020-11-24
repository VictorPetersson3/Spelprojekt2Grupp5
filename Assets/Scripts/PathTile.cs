using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    [SerializeField]
    bool isEndTile = false;

    Vector3 myPosition;
    public Vector3 GetPathTilePosition { get { return myPosition; } set { myPosition = value; } }
    public bool IsEndTile { get { return isEndTile; } }

    void Start()
    {
        myPosition = new Vector3(Mathf.FloorToInt(transform.position.x), 0, Mathf.FloorToInt(transform.position.z));
        transform.position = myPosition;
    }

}
 