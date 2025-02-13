using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicComponent : MonoBehaviour
{
    public string partName;

    public string GetName()
    {
        return partName;
    }

    private void Awake()
    {
        partName = gameObject.name;
    }
}
