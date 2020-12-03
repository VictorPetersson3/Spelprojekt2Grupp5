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
    RaycastHit myMoneyHit;
    RaycastHit myEndHit;
    GameManager myGameManger;

    private void Start()
    {
        myFinishedLevel = false;
        myMaxDistance = 0.3f;
        myGameManger = GameManager.globalInstance;
    }

    private void FixedUpdate()
    {
        myMoneyDetection = Physics.BoxCast(gameObject.transform.position, transform.localScale / 2, transform.up, out myMoneyHit, Quaternion.identity, myMaxDistance, myMoneyMask);
        myEndGameDetection = Physics.BoxCast(gameObject.transform.position, transform.localScale / 2, transform.up, out myEndHit, Quaternion.identity, myMaxDistance, myEndMask);

        if (myMoneyDetection == true)
        {
            Debug.Log("HIT " + myMoneyHit.collider.name);
            myMoneyHit.transform.GetComponent<MoneyValue>().AddingMoney();
        }

        if (myEndGameDetection == true && myFinishedLevel == false)
        {
            Debug.Log("HIT " + myEndHit.collider.name);
            myGameManger.SetFinishedLevel();
            myFinishedLevel = true;
        }

    }
}
