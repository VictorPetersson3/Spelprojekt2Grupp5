using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{

    public Vector3 amount = new Vector3(1f, 1f, 0);

    public float duration = 1;

    public float speed = 10;

    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    public Transform camera;

    float time = 0;
    Vector3 lastPos;
    Vector3 nextPos;

    
    public void Shake()
    {
        ResetCamera();
        time = duration;
    }

    private void LateUpdate()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            if(time > 0)
            {
                nextPos = (Mathf.PerlinNoise(time * speed, time * speed * 2)-0.5f) * amount.x * camera.right * curve.Evaluate(1f-time/duration) +
                    (Mathf.PerlinNoise(time * speed * 2, time * speed) -0.5f) * amount.y * transform.up * curve.Evaluate(1f - time / duration);

                camera.Translate(nextPos - lastPos);
                lastPos = nextPos;
            }
            else
            {
                ResetCamera();
            }
        }
    }

    public void StopShake()
    {
        time = 0;
    }

    void ResetCamera()
    {
        lastPos = nextPos = Vector3.zero;
        camera.localPosition = Vector3.zero;
    }

}
