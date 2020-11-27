using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BounceElement : MonoBehaviour
{
    [SerializeField]
    RectTransform myRectTransform;
    [SerializeField]
    AnimationCurve myCurve;
    [SerializeField]
    LeanTweenType easeType;

    Vector3 myMovementDestination = new Vector3(0.0f, 1.0f, 0.0f);
   

    private void Update()
    {
        if(easeType == LeanTweenType.animationCurve)
        {
            LeanTween.move(myRectTransform, myMovementDestination * 40, 4.0f).setLoopPingPong().setEase(myCurve);
        }
        else
        {
            LeanTween.move(myRectTransform, myMovementDestination * 40, 4.0f).setLoopPingPong().setEase(easeType);
        }
    }

}
