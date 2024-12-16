using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    public string partName;
    public List<string> function = new List<string>();
    public List<string> structure = new List<string>();
    public List<string> components = new List<string>();

    public Transform pivot;

    public bool needsCenter = false;

    // Start is called before the first frame update
    void Awake()
    {
        if(partName == null)
        {
            partName = name;
        }
        if(function.Count == 0 || structure.Count == 0 || components.Count == 0)
        {
            Console.WriteLine("Field is empty");
        }
        GetPivot();
    }

    public string GetName()
    {
        return partName;
    }
    public string GetFunction()
    {
        return NiceString(function);
    }
    public string GetStructure()
    {
        return NiceString(structure);
    }
    public string GetDerived()
    {
        return NiceString(components);
    }

    //Iterates through given list, formatting them in a bullet point style
    //Then, it deletes the last break of page to ensure there is no blank line
    //Lastly, it returns the new, formatted, single string
    private string NiceString(List<string> info)
    {
        string niceString = "";

        foreach (string str in info)
        {
            niceString += $"- {str} \n";
        }

        niceString = niceString.Substring(0, niceString.Length - 2);

        return niceString;
    }

    void GetPivot()
    {
        for(int i = 0; i < transform.childCount; i++)
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
