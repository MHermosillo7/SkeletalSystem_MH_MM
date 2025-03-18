using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicComponent : MonoBehaviour
{
    public string partName;

    public string partName_ES;

    public Transform pivot;

    public string GetName()
    {
        return partName;
    }
    public string GetName_ES()
    {
        return partName_ES;
    }

    private void Awake()
    {
        partName = gameObject.name;

        GetPivot();
    }
    void GetPivot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.CompareTag("Pivot"))
            {
                pivot = child;

                return;
            }
        }
    }

}
