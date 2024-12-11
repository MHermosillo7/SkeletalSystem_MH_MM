using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    List<GameObject> derivedBones = new List<GameObject>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("DerivedBone"))
            {
                derivedBones.Add(child.gameObject);
            }
            if (child.CompareTag("Bone"))
            {
                derivedBones.Add(child.gameObject);
            }
        }
        print(derivedBones.Count);
    }
}
