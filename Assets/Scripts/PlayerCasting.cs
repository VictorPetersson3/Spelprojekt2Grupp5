using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    float myMaxDistance;
    bool myMoneyDetection;
    [SerializeField]
    LayerMask myMoneyMask;
    RaycastHit myMoneyHit;


    private void Start()
    {
       
        myMaxDistance = 0.3f;
      
    }

    private void FixedUpdate()
    {
        myMoneyDetection = Physics.BoxCast(gameObject.transform.position, transform.localScale / 2, transform.up, out myMoneyHit, Quaternion.identity, myMaxDistance, myMoneyMask);

        if (myMoneyDetection == true)
        {
            Debug.Log("HIT " + myMoneyHit.collider.name);
            myMoneyHit.transform.GetComponent<CoinScript>().AddingMoney();
            AudioManager.ourInstance.PlayEffect(AudioManager.EEffects.COIN);
        }
    }
}
