using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicComponent : MonoBehaviour
{
    public string partName;

    public Transform pivot;

    public string GetName()
    {
        return partName;
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
