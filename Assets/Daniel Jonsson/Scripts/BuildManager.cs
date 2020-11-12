using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class BuildManager : MonoBehaviour
{
    public static BuildManager globalInstance;

    private void Awake()
    {
        globalInstance = this;
    }


    //public Dictionary<string, Queue<GameObject>> myPoolDictionary;
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
                Debug.Log(myPoolList[x].myTilePrefab);
                GameObject gameObj = Instantiate(myPoolList[x].myTilePrefab);
                gameObj.SetActive(false);
                gameObj.transform.position = myOriginalSpawnPoolPosition;
                myPoolList[x].myTileList.Add(gameObj);
            }
        }


    }

    private void Update()
    {
       
    }


    public void SpawnFromPool(string aTag, Vector3 aPosition, Quaternion aRotation)
    {

        for (int x = 0; x < myPoolList.Count; x++)
        {
            for (int y = 0; y < myPoolList[x].myTileList.Count; y++)
            {
                if (myPoolList[x].myTileList[y].activeSelf == false && myPoolList[x].myTileTag == aTag)
                {
                    myPoolList[x].myTileList[y].transform.position = aPosition;
                    myPoolList[x].myTileList[y].SetActive(true);
                    break;
                }
                else if (myPoolList[x].myTileList[y].activeSelf == false && myPoolList[x].myTileTag == aTag)
                {
                    myPoolList[x].myTileList[y].transform.position = aPosition;
                    myPoolList[x].myTileList[y].SetActive(true);
                    break;
                }
                else if (myPoolList[x].myTileList[y].activeSelf == false && myPoolList[x].myTileTag == aTag)
                {
                    myPoolList[x].myTileList[y].transform.position = aPosition;
                    myPoolList[x].myTileList[y].SetActive(true);
                    break;
                }
            }
        }

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

}
