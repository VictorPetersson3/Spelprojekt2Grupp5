using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BounceElement : MonoBehaviour
{
    [SerializeField]
    RectTransform myRectTransform;

    public LeanTweenType easeType;
    Vector3 myMovementDestination = new Vector3(1.0f,200.2f,1.0f);
    bool myIsTweening = false;
    bool myHasReachedEnd = false;

    private void Update()
    {
        //if(!myHasReachedEnd)
        //{
            LeanTween.move(myRectTransform, myMovementDestination, 3.0f).setEase(easeType);
        //    if (LeanTween.isTweening(myRectTransform))
        //    {
        //        myIsTweening = true;
        //    }
        //    if (myIsTweening)
        //    {
        //        myHasReachedEnd = true;
        //    }
        //}
        //else
        //{
        //    LeanTween.move(myRectTransform, new Vector3 (1.0f, 1.0f, 1.0f), 3.0f).setEase(easeType);
        //    if (LeanTween.isTweening(myRectTransform))
        //    {
        //        myIsTweening = true;
        //    }
        //    if (myIsTweening)
        //    {
        //        myHasReachedEnd = false;
        //    }
        //}

    }

}
