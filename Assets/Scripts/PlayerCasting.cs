using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    float myMaxDistance;

    bool myMoneyDetection;
    bool myEndDetection;

    [SerializeField]
    LayerMask myMoneyMask;
    [SerializeField]
    LayerMask myEndMask;

    RaycastHit myMoneyHit;
    RaycastHit myEndGameHit;

    PathManager myPathManager;
    GameManager myGameManager;
    Animator myAnimator;
    PlayerController myPlayerController;
    Placement myPlacement;


    private void Start()
    {
        myGameManager = GameManager.globalInstance;
        myPlayerController = GetComponent<PlayerController>();
        myPathManager = myPlayerController.GetManager();
        myAnimator = myPlayerController.GetAnimator();
        myPlacement = GameObject.Find("World").GetComponent<Placement>();
        myMaxDistance = 0.3f;
        myMoneyDetection = false;
        myEndDetection = false;
    }

    private void FixedUpdate()
    {
        myMoneyDetection = Physics.BoxCast(gameObject.transform.position, transform.localScale / 2, transform.up, out myMoneyHit, Quaternion.identity, myMaxDistance, myMoneyMask);
        myEndDetection = Physics.BoxCast(gameObject.transform.position, transform.localScale / 2, transform.up, out myEndGameHit, Quaternion.identity, myMaxDistance, myEndMask);


        if (myMoneyDetection == true)
        {
            myMoneyHit.transform.GetComponent<CoinScript>().AddingMoney();
            AudioManager.ourInstance.PlayEffect(AudioManager.EEffects.COIN);
        }

        if (myEndDetection == true && myPlayerController.GetWalking() == true)
        {
            myPlacement.GetSetIsEnded = true;
            myPlayerController.SetStopWalking();
            AudioManager.ourInstance.StopWalkingEffect();
            AudioManager.ourInstance.PlayEffect(AudioManager.EEffects.WIN);
            myAnimator.SetBool("isWalking", false);
            myAnimator.SetBool("isInGoal", true);
            //myPathManager.ResetPath();
            myGameManager.SetFinishedLevel();
            Debug.Log("You win");

        }
    }
}
