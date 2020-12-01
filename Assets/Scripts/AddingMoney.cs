using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingMoney : MonoBehaviour
{
    float myMaxDistance;
    bool myHitDetection;

    [SerializeField]
    LayerMask myLayerMask;
    RaycastHit myHit;
    GameManager myGameManger;

    private void Start()
    {
        myMaxDistance = 0.3f;
        myGameManger = GameManager.globalInstance;
    }

    private void FixedUpdate()
    {
        myHitDetection = Physics.BoxCast(gameObject.transform.position, transform.localScale / 2, transform.up, out myHit, Quaternion.identity, myMaxDistance, myLayerMask);

        if (myHitDetection == true)
        {
            myHit.transform.GetComponent<MoneyValue>().AddingMoney();
            Debug.Log("HIT " + myHit.collider.name);
        }
    }
}
