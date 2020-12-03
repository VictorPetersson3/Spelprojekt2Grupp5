using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    float myMaxDistance;
    bool myMoneyDetection;
    bool myEndGameDetection;
    bool myFinishedLevel;

    [SerializeField]
    LayerMask myMoneyMask;
    [SerializeField]
    LayerMask myEndMask;
    RaycastHit myHit;
    GameManager myGameManger;

    private void Start()
    {
        myFinishedLevel = false;
        myMaxDistance = 0.3f;
        myGameManger = GameManager.globalInstance;
    }

    private void FixedUpdate()
    {
        myMoneyDetection = Physics.BoxCast(gameObject.transform.position, transform.localScale / 2, transform.up, out myHit, Quaternion.identity, myMaxDistance, myMoneyMask);
        myEndGameDetection = Physics.BoxCast(gameObject.transform.position, transform.localScale / 2, transform.up, out myHit, Quaternion.identity, myMaxDistance, myEndMask);

        if (myMoneyDetection == true)
        {
            myHit.transform.GetComponent<MoneyValue>().AddingMoney();
            Debug.Log("HIT " + myHit.collider.name);
        }

        if (myEndGameDetection == true && myFinishedLevel == false)
        {
            Debug.Log("HIT " + myHit.collider.name);
            //myGameManger.SetFinishedLevel();
            myFinishedLevel = true;
        }

    }
}
