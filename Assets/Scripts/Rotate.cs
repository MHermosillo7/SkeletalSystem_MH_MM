using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TEST
public class Rotate : MonoBehaviour
{

    public Quaternion startPos, endPos;
    public bool repeatable = false;
    public float speed = 1.0f;
    public float duration = 3.0f;

    float startTime, totalDistance;

    // Use this for initialization
    IEnumerator Start()
    {
        while (repeatable)
        {
            yield return RepeatLerp(startPos, endPos, duration);
            yield return RepeatLerp(endPos, startPos, duration);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!repeatable)
        {
            float currentDuration = (Time.time - startTime) * speed;
            float journeyFraction = currentDuration / totalDistance;
            this.transform.rotation = Quaternion.Lerp(startPos, endPos, journeyFraction);
        }
    }

    public IEnumerator RepeatLerp(Quaternion a, Quaternion b, float time)
    {
        float i = 0.0f;
        while (i < 1.0f)
        {
            transform.rotation =
                Quaternion.Slerp(a, b, i);
            i += Time.deltaTime;

            yield return null;
        }
    }
}