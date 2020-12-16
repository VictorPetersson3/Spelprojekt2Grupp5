using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    [SerializeField]
    //bool isEndTile = false;
    PathManager myPathManager;
    Vector3 myPosition;
    public PathManager SetPathManager { set { myPathManager = value; } }
    public Placement myPlacement;
    public Vector3 GetPathTilePosition { get { return myPosition; } set { myPosition = value; } }
    //public bool IsEndTile { get { return isEndTile; } }
    public Neighbor GetNeighbor { get { return myNeigbor; } set { myNeigbor = value; } }


    public GameObject myTurnRoad;
    public GameObject myStraightRoad;

    float mySpeed = 3f;
    float myCurrentSpeed = 0;
    bool myTemp = false;
    bool myCheckForPortal = false;

    [SerializeField]
    ParticleSystem myPlacementEffect;
    PathTile myPathTileNeighbors;

    [SerializeField]
    LayerMask myPortalMask;

    [SerializeField]
    PlacementIndicator myPlacementIndicator;

    RaycastHit myPortalHit;

    


    Neighbor myNeigbor = Neighbor.none;
    public enum Neighbor
    {
        none,
        left,
        right,
        up,
        down
    }


    public virtual void Start()
    {

        CheckForPortal();

        myPosition = new Vector3(Mathf.FloorToInt(transform.position.x), 0, Mathf.FloorToInt(transform.position.z));
        transform.position = myPosition;

        myPlacementEffect.transform.position = new Vector3(myPlacementEffect.transform.position.x, 0.1f, myPlacementEffect.transform.position.z);
        //myPlacementEffect.Play();
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }
    private void Update()
    {
        MoveObjectToPlaceDown(transform);
    }

    public void CheckNeighbors()
    {
        myTemp = false;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

        int x = Mathf.FloorToInt(transform.position.x);
        int z = Mathf.FloorToInt(transform.position.z);
        if (myPathManager.GetPortals.Count != 0)
        {

            for (int i = 0; i < myPathManager.GetPortals.Count; i++)
            {
                if (x - 1 >= 0)
                {
                    if (myPathManager.GetPathTileMap[x - 1, z] != null)
                    {
                        //Debug.Log("Last placed tile pos: " + myPathManager.GetLastPlacedTile.transform.position, myPathManager.GetLastPlacedTile.gameObject);

                        //Debug.Log("Found Normal Tile");
                        if (myPathManager.GetPathTileMap[x - 1, z] == myPathManager.GetLastPlacedTile)
                        {
                            myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                            myPathManager.GetLastPlacedTile = this;
                            myNeigbor = Neighbor.left;
                            CheckOldNeighborLeft();
                        }
                        if (myPathManager.GetPathTileMap[x - 1, z] == myPathManager.GetPortals[i].myStartTile)
                        {
                            //Debug.Log("Found Start Tile");
                            myPathTileNeighbors = myPathManager.GetPortals[i].myStartTile;
                            myPathManager.GetLastPlacedTile = this;
                            myNeigbor = Neighbor.left;
                            CheckOldNeighborLeft();
                        }
                    }
                }
                if (x + 1 < WorldController.Instance.GetWorldWidth)
                {
                    if (myPathManager.GetPathTileMap[x + 1, z] != null)
                    {
                        //Debug.Log("Last placed tile pos: " + myPathManager.GetLastPlacedTile.transform.position, myPathManager.GetLastPlacedTile.gameObject) ;
                        if (myPathManager.GetPathTileMap[x + 1, z] == myPathManager.GetLastPlacedTile)
                        {
                            myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                            myPathManager.GetLastPlacedTile = this;
                            myNeigbor = Neighbor.right;
                            CheckOldNeighborRight();
                        }
                        if (myPathManager.GetPathTileMap[x + 1, z] == myPathManager.GetPortals[i].myStartTile)
                        {
                            //Debug.Log("Found Start Tile");

                            myPathTileNeighbors = myPathManager.GetPortals[i].myStartTile;
                            myPathManager.GetLastPlacedTile = this;
                            myNeigbor = Neighbor.right;
                            CheckOldNeighborRight();
                        }
                    }
                }
                if (z - 1 >= 0)
                {

                    if (myPathManager.GetPathTileMap[x, z - 1] != null)
                    {
                      
                        //Debug.Log("Last placed tile pos: " + myPathManager.GetLastPlacedTile.transform.position, myPathManager.GetLastPlacedTile.gameObject);

                        if (myPathManager.GetPathTileMap[x, z - 1] == myPathManager.GetLastPlacedTile)
                        {
                            myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                            myPathManager.GetLastPlacedTile = this;
                            myNeigbor = Neighbor.down;
                            CheckOldNeighborDown();

                        }
                        if (myPathManager.GetPathTileMap[x, z - 1] == myPathManager.GetPortals[i].myStartTile)
                        {
                            //Debug.Log("Found Start Tile");

                            myPathTileNeighbors = myPathManager.GetPortals[i].myStartTile;
                            myPathManager.GetLastPlacedTile = this;
                            myNeigbor = Neighbor.down;
                            CheckOldNeighborDown();
                        }
                    }
                }
                if (z + 1 < WorldController.Instance.GetWorldDepth)
                {
                    if (myPathManager.GetPathTileMap[x, z + 1] != null)
                    {
                        //Debug.Log("Last placed tile pos: " + myPathManager.GetLastPlacedTile.transform.position, myPathManager.GetLastPlacedTile.gameObject);

                        //Debug.Log("Found Normal Tile");
                        if (myPathManager.GetPathTileMap[x, z + 1] == myPathManager.GetLastPlacedTile)
                        {
                            myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                            myPathManager.GetLastPlacedTile = this;
                            myNeigbor = Neighbor.up;
                            CheckOldNeighborUp();
                        }
                        if (myPathManager.GetPathTileMap[x, z + 1] == myPathManager.GetPortals[i].myStartTile)
                        {
                            myPathTileNeighbors = myPathManager.GetPortals[i].myStartTile;
                            myPathManager.GetLastPlacedTile = this;
                            myNeigbor = Neighbor.up;
                            CheckOldNeighborUp();
                        }
                    }
                }
            }
        }
        else
        {
            if (x - 1 >= 0)
            {
                if (myPathManager.GetPathTileMap[x - 1, z] != null)
                {
                    if (myPathManager.GetPathTileMap[x - 1, z] == myPathManager.GetLastPlacedTile)
                    {
                        myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                        myNeigbor = Neighbor.left;
                        CheckOldNeighborLeft();
                        myPathManager.GetLastPlacedTile = this;
                    }
                    //if (myPathManager.GetPathTileMap[x - 1, z] == myPathManager.GetPortals[i].myStartTile)
                    //{
                    //    Debug.Log("Found Start Tile");
                    //    myPathTileNeighbors = myPathManager.GetPortals[i].myStartTile;
                    //    myNeigbor = Neighbor.left;
                    //    CheckOldNeighborLeft();
                    //    myPathManager.GetLastPlacedTile = this;
                    //}
                }
            }
            if (x + 1 < WorldController.Instance.GetWorldWidth)
            {
                if (myPathManager.GetPathTileMap[x + 1, z] != null)
                {
                    if (myPathManager.GetPathTileMap[x + 1, z] == myPathManager.GetLastPlacedTile)
                    {
                        myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                        myNeigbor = Neighbor.right;
                        CheckOldNeighborRight();
                        myPathManager.GetLastPlacedTile = this;
                    }
                    //if (myPathManager.GetPathTileMap[x + 1, z] == myPathManager.GetPortals[i].myStartTile)
                    //{
                    //    Debug.Log("Found Start Tile");

                    //    myPathTileNeighbors = myPathManager.GetPortals[i].myStartTile;
                    //    myNeigbor = Neighbor.right;
                    //    CheckOldNeighborRight();
                    //    myPathManager.GetLastPlacedTile = this;
                    //}
                }
            }
            if (z - 1 >= 0)
            {

                if (myPathManager.GetPathTileMap[x, z - 1] != null)
                {

                    if (myPathManager.GetPathTileMap[x, z - 1] == myPathManager.GetLastPlacedTile)
                    {
                        myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                        myNeigbor = Neighbor.down;
                        CheckOldNeighborDown();

                        myPathManager.GetLastPlacedTile = this;
                    }
                    //if (myPathManager.GetPathTileMap[x, z - 1] == myPathManager.GetPortals[i].myStartTile)
                    //{
                    //    Debug.Log("Found Start Tile");

                    //    myPathTileNeighbors = myPathManager.GetPortals[i].myStartTile;
                    //    myNeigbor = Neighbor.down;
                    //    CheckOldNeighborDown();
                    //    myPathManager.GetLastPlacedTile = this;
                    //}
                }
            }
            if (z + 1 < WorldController.Instance.GetWorldDepth)
            {
                if (myPathManager.GetPathTileMap[x, z + 1] != null)
                {
                    if (myPathManager.GetPathTileMap[x, z + 1] == myPathManager.GetLastPlacedTile)
                    {
                        myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                        myNeigbor = Neighbor.up;
                        CheckOldNeighborUp();
                        myPathManager.GetLastPlacedTile = this;
                    }
                    //if (myPathManager.GetPathTileMap[x, z + 1] == myPathManager.GetPortals[i].myStartTile)
                    //{
                    //    myPathTileNeighbors = myPathManager.GetPortals[i].myStartTile;
                    //    myNeigbor = Neighbor.up;
                    //    CheckOldNeighborUp();
                    //    myPathManager.GetLastPlacedTile = this;
                    //}
                }
            }
        }
        myPlacementEffect.Play();
        AudioManager.ourInstance.PlayEffect(AudioManager.EEffects.PLACE);
    }
    void CheckOldNeighborDown()
    {
        if (myPathTileNeighbors.GetNeighbor == Neighbor.left)
        {
            // left down
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 0, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.right)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
            // right down
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.down)
        {
            myPathTileNeighbors.myStraightRoad.SetActive(true);
            myPathTileNeighbors.myTurnRoad.SetActive(false);
            // straight vertical
        }
        myStraightRoad.SetActive(true);
        myTurnRoad.SetActive(false);
    }
    void CheckOldNeighborUp()
    {
        if (myPathTileNeighbors.GetNeighbor == Neighbor.left)
        {
            // left down
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, -90, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.right)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, -180, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
            // right down
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.up)
        {
            myPathTileNeighbors.myStraightRoad.SetActive(true);
            myPathTileNeighbors.myTurnRoad.SetActive(false);
            // straight vertical
        }
        myStraightRoad.SetActive(true);
        myTurnRoad.SetActive(false);
    }
    void CheckOldNeighborLeft()
    {
        if (myPathTileNeighbors.GetNeighbor == Neighbor.up)
        {
            // left down
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.down)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 180, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
            // right down
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.left)
        {
            myPathTileNeighbors.myStraightRoad.SetActive(true);
            myPathTileNeighbors.myTurnRoad.SetActive(false);
            // straight vertical
        }
        myStraightRoad.SetActive(true);
        myTurnRoad.SetActive(false);
        myStraightRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    void CheckOldNeighborRight()
    {
        if (myPathTileNeighbors.GetNeighbor == Neighbor.up)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 0, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.down)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, -90, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);

        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.right)
        {
            myPathTileNeighbors.myStraightRoad.SetActive(true);
            myPathTileNeighbors.myTurnRoad.SetActive(false);

        }
        myStraightRoad.SetActive(true);
        myTurnRoad.SetActive(false);
        myStraightRoad.transform.rotation = Quaternion.Euler(0, 90, 0);

    }
    void MoveObjectToPlaceDown(Transform aTransform)
    {
        myCurrentSpeed = mySpeed * mySpeed;
        if (aTransform.position.y + (myCurrentSpeed * Time.deltaTime) > 0)
        {
            aTransform.position = Vector3.Lerp(aTransform.position, new Vector3(aTransform.position.x, 0, aTransform.position.z), myCurrentSpeed * Time.deltaTime); ;
        }

    }
    public void ResetMe()
    {
        if (myTurnRoad != null)
        {
            myTurnRoad.transform.rotation = Quaternion.Euler(0, 0, 0);
            myTurnRoad.SetActive(false);
        }
        if (myStraightRoad != null)
        {
            myStraightRoad.transform.rotation = Quaternion.Euler(0, 0, 0);
            myStraightRoad.SetActive(false);
        }
        if (myPathTileNeighbors != null)
        {
            myPathTileNeighbors.GetNeighbor = Neighbor.none;
            myPathTileNeighbors = null;
        }
        myNeigbor = Neighbor.none;


    }


    public void CheckForPortal()
    {
        myCheckForPortal = Physics.BoxCast(gameObject.transform.position, transform.localScale / 2, transform.up, out myPortalHit, Quaternion.identity, 1f, myPortalMask);

        if (myCheckForPortal == true)
        {
            PathManager pathManager = FindObjectOfType<PathManager>();

            List<Portals> portals = pathManager.GetPortals;

            for (int i = 0; i < portals.Count; i++)
            {
                if (portals[i].name == myPortalHit.collider.gameObject.name)
                {
                    myPlacement = FindObjectOfType<Placement>();
                    myPlacement.SetValidPlacement(i);
                    myPlacementIndicator = FindObjectOfType<PlacementIndicator>();
                    myPlacementIndicator.transform.position = portals[i].GetExit() + gameObject.transform.position;
                    myPlacementIndicator.CheckPlacementIndicators();
                    break;
                }
            }

        }

    }
}
