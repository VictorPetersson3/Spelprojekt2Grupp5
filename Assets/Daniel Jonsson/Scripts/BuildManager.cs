using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Rendering;

public class BuildManager : MonoBehaviour
{
    public static BuildManager globalInstance;

    private void Awake()
    {
        globalInstance = this;
    }


    public Dictionary<string, Queue<GameObject>> myPoolDictionary;
    public List<Pool> myPoolList;


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    // Start is called before the first frame update
    void Start()
    {
        myPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in myPoolList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            myPoolDictionary.Add(pool.tag, objectPool);

        }
    }


    public GameObject SpawnFromPool(string aTag, Vector3 aPosition, Quaternion aRotation)
    {

        if (myPoolDictionary.ContainsKey(aTag) == false)
        {
            Debug.LogWarning("Pool with tag " + aTag + " doesn't exist.");
            return null;
        }

        GameObject objectSpawn = myPoolDictionary[aTag].Dequeue();

        objectSpawn.SetActive(true);
        objectSpawn.transform.position = aPosition;
        objectSpawn.transform.rotation = aRotation;

        objectSpawn.GetComponent<CubeForce>().OnObjectSpawn();

        myPoolDictionary[aTag].Enqueue(objectSpawn);

        return objectSpawn;
    }

}
