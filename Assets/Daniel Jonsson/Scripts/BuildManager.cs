using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class BuildManager : MonoBehaviour
{
    public static BuildManager globalInstance;

    private void Awake()
    {
        if (globalInstance == null)
        {
            globalInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (globalInstance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private List<Pool> myPoolList;

    [System.Serializable]
    public class Pool
    {
        public string myTileTag;
        public GameObject myTilePrefab;
        public int myAmountOfTiles;
        public List<GameObject> myTileList = new List<GameObject>();
    }

    [SerializeField]
    private Vector3 myOriginalSpawnPoolPosition;


    void Start()
    {
        for (int x = 0; x < myPoolList.Count; x++)
        {
            for (int y = 0; y < myPoolList[x].myAmountOfTiles; y++)
            {
                GameObject gameObj = Instantiate(myPoolList[x].myTilePrefab);
                gameObj.transform.parent = gameObject.transform;
                gameObj.SetActive(false);
                gameObj.transform.position = myOriginalSpawnPoolPosition;
                myPoolList[x].myTileList.Add(gameObj);
            }
        }
    }


    public GameObject SpawnFromPool(string aTag, Quaternion aRotation)
    {
        for (int x = 0; x < myPoolList.Count; x++)
        {
            for (int y = 0; y < myPoolList[x].myTileList.Count; y++)
            {
                if (myPoolList[x].myTileList[y].activeSelf == false && myPoolList[x].myTileTag == aTag)
                {
                    myPoolList[x].myTileList[y].SetActive(true);
                    return myPoolList[x].myTileList[y].gameObject;
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
                if (myPoolList[x].myTileList[y].activeSelf == true)
                {
                    myPoolList[x].myTileList[y].transform.position = myOriginalSpawnPoolPosition;
                    myPoolList[x].myTileList[y].SetActive(false);
                }
            }
        }
    }

    public void ReturnToPool(GameObject aPooledGameObject)
    {
        aPooledGameObject.SetActive(false);
        aPooledGameObject.transform.position = myOriginalSpawnPoolPosition;
    }
}
