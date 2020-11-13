using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;

public class ObstructTileMap : MonoBehaviour
{

    [Header("Size settings")]
    [Range(1, 13)]
    [SerializeField]
    int myWidth = 1;
    [Range(1, 13)]
    [SerializeField]
    int myDepth = 1;
    [Header("Refernce settings")]
    [SerializeField]
    WorldController myWorldController;

   

    JobHandle myJobHandle;
    FindTilesJob myFindTilesJob;
    private void OnValidate()
    {

        myWorldController = FindObjectOfType<WorldController>();
    }

    void Start()
    {
        Invoke("CreateJob", 0.5f);
        myJobHandle.Complete();

    }




    void CreateJob()
    {
        myFindTilesJob = new FindTilesJob
        {
            worldDepth = myWorldController.GetWorldDepth,
            worldWidth = myWorldController.GetWorldWidth,
            sizeX = myWidth,
            sizeZ = myDepth,
            objectPosition = transform.position
        };
        myJobHandle = myFindTilesJob.Schedule();

    }
    public static Tile[] GetOverLappingTiles(int aXSize, int aZSize, int aWorldDepth, int aWorldWidth, Vector3 aObjectPosition)
    {
        int xPos = Mathf.FloorToInt(aObjectPosition.x);
        int zPos = Mathf.FloorToInt(aObjectPosition.z);

        int xExtents = aXSize / 2;
        int zExtents = aZSize / 2;

        Vector3 cornerPos = new Vector3(xPos - xExtents, 0, zPos - zExtents);

        int totalSizeX = Mathf.FloorToInt(cornerPos.x) + aXSize;
        int totalSizeZ = Mathf.FloorToInt(cornerPos.z) + aZSize;

        Tile[] tileInRange = new Tile[aXSize * aZSize];
        
        if (cornerPos.x < 0 || cornerPos.z < 0)
        {
            Debug.Log("Some Tiles are out of range");
            return null;
        }
        if (totalSizeZ > aWorldDepth || totalSizeX > aWorldWidth)
        {
            Debug.Log("Some Tiles are out of range");
            return null;
        }
        int count = 0;
        int xValue = 0;
        int zValue = 0;

        for (int x = xPos - xExtents; x < totalSizeX; x++)
        {
            for (int z = (zPos - zExtents); z < totalSizeZ; z++)
            {
                tileInRange[count] = WorldController.Instance.GetTileAtPosition(x, z);
               
                
                count++;
                zValue++;
            }
            xValue++;
        }
       
        return tileInRange;
    }

    public struct FindTilesJob : IJob
    {
        public int worldDepth;
        public int worldWidth;
        public int sizeX;
        public int sizeZ;
        public Vector3 objectPosition;

        public void Execute()
        {
            Tile[] tileArray = GetOverLappingTiles(sizeX, sizeZ, worldDepth, worldWidth, objectPosition);

            if (tileArray != null)
            {
                for (int i = 0; i < tileArray.Length; i++)
                {
                    if (tileArray[i].GetSetTileState == Tile.TileState.empty)
                    {
                        Tile t = tileArray[i];
                        t.GetSetTileState = Tile.TileState.obstructed;
                        tileArray[i] = t;

                    }
                    WorldController.Instance.GetWorld.CopySetTile(tileArray[i]);
               
                  
                }

            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(myWidth, 0, myDepth));
    }
}
