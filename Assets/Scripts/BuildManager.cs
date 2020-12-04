using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class BuildManager : MonoBehaviour
{
    public static BuildManager globalInstance;
    [SerializeField]
    [Header("Reference Settings")]
    PathManager myPathManager;
    private void Awake()
    {
        if (globalInstance == null)
        {
            globalInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (globalInstance != this)
        {
            Destroy(gameObject);
        }
    }
    private void OnValidate()
    {
        myPathManager = FindObjectOfType<PathManager>();

    }

    [Header("Path Type Settings")]
    [SerializeField]
    private List<Pool> myPoolList;
    [System.Serializable]
    public class Pool
    {
        public int myTileId;
        public PathTile myTilePrefab;
        public int myAmountOfTiles;
        [HideInInspector]
        public List<PathTile> myTileList = new List<PathTile>();
    }

    [SerializeField]
    private Vector3 myOriginalSpawnPoolPosition;


    void Start()
    {
        for (int x = 0; x < myPoolList.Count; x++)
        {
            for (int y = 0; y < myPoolList[x].myAmountOfTiles; y++)
            {
                PathTile gameObj = Instantiate(myPoolList[x].myTilePrefab);
                gameObj.transform.parent = gameObject.transform;
                gameObj.gameObject.SetActive(false);
                gameObj.transform.position = myOriginalSpawnPoolPosition;
                gameObj.SetPathManager = myPathManager;
                myPoolList[x].myTileList.Add(gameObj);
            }
        }
    }

    public PathTile SpawnFromPool(int aTag, Quaternion aRotation, Vector3 aPosition)
    {
        for (int x = 0; x < myPoolList.Count; x++)
        {
            for (int y = 0; y < myPoolList[x].myTileList.Count; y++)
            {
                if (myPoolList[x].myTileList[y].gameObject.activeSelf == false && myPoolList[x].myTileId == aTag)
                {
                    GameManager.globalInstance.ChangeMoney(1);
                    myPoolList[x].myTileList[y].gameObject.SetActive(true);
                    myPoolList[x].myTileList[y].transform.position = aPosition;
                    return myPoolList[x].myTileList[y];
                }
            }
        }
        return null;
    }

    public void ResetTiles()
    {
        for (int x = 0; x < myPoolList.Count; x++)
        {
            for (int y = 0; y < myPoolList[x].myTileList.Count; y++)
            {
                if (myPoolList[x].myTileList[y].gameObject.activeSelf == true)
                {
                    myPoolList[x].myTileList[y].transform.position = myOriginalSpawnPoolPosition;
                    myPoolList[x].myTileList[y].gameObject.SetActive(false);
                }
            }
        }
    }

    public void ReturnMoney()
    {
        GameManager.globalInstance.ChangeMoney(-1);
    }

    public void ReturnToPool(PathTile aPooledGameObject)
    {
        aPooledGameObject.gameObject.SetActive(false);
        aPooledGameObject.transform.position = myOriginalSpawnPoolPosition;
    }
}
