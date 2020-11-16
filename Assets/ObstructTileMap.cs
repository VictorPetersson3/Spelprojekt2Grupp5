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

    public WorldController GetWorldController { get { return myWorldController; } }

    JobHandle myJobHandle;
    FindTilesJob myFindTilesJob;
    public virtual void OnValidate()
    {

        myWorldController = FindObjectOfType<WorldController>();
    }

    public virtual void Start()
    {
        Invoke("CreateJob", 0.5f);
        myJobHandle.Complete();

    }
    public virtual void Update()
    {

    }



    protected void CreateJob()
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

        float xF = aXSize;
        float zF = aZSize;

        int xExtents = Mathf.FloorToInt(xF / 2);
        int zExtents = Mathf.FloorToInt(zF / 2);

        Vector3 cornerPos = new Vector3(xPos - xExtents, 0, zPos - zExtents);

        int totalSizeX = Mathf.FloorToInt(cornerPos.x) + aXSize;
        int totalSizeZ = Mathf.FloorToInt(cornerPos.z) + aZSize;

        Tile[] tileInRange = new Tile[aXSize * aZSize];

        if (cornerPos.x < 0 || cornerPos.z < 0)
        {
            Debug.LogError("Tiles are out of range in the negativ coordinate space\nObject Postion: " + aObjectPosition);
            return null;
        }
        if (totalSizeZ > aWorldDepth || totalSizeX > aWorldWidth)
        {
            Debug.LogError("Tiles are out of range at in the possitive coordinate space\nObject Postion: " + aObjectPosition);
            return null;
        }
        int xStart = xPos - xExtents;
        int zStart = zPos - zExtents;

        int count = 0;

        for (int x = xStart; x < totalSizeX; x++)
        {
            for (int z = zStart; z < totalSizeZ; z++)
            {
                tileInRange[count] = WorldController.Instance.GetTileAtPosition(x, z);
                count++;
            }

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

    protected virtual void OnDrawGizmos()
    {

        Color colorN = Color.magenta;
        colorN.a = 0.5f;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(new Vector3(Mathf.Floor(transform.position.x), 0, Mathf.Floor(transform.position.z)), new Vector3(myWidth, 0.1f, myDepth));
        Gizmos.color = colorN;
        Gizmos.DrawCube(new Vector3(Mathf.Floor(transform.position.x), 0, Mathf.Floor(transform.position.z)), new Vector3(myWidth - 0.05f, 0.05f, myDepth - 0.05f));
    }
}
