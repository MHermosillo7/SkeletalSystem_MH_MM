using BodySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsolateFilter : MonoBehaviour
{
    // Pseudo-code

    //Get a list of all objects in scene
    List<Zoom> objects = new List<Zoom>();

    V1User user;

    // Start is called before the first frame update
    void Awake()
    {
        objects.AddRange(FindObjectsOfType<Zoom>());

        user = FindObjectOfType<V1User>();
    }
    //Deactivate all objects except the one currently active
    public void IsolateObjects(bool activated)
    {
        //if isolate is true it will enable other objects
        //if false, deactivate them
        foreach (Zoom z in objects)
        {
            if (user != user.selectedItemZoom)
            {
                z.Enable(activated);
            }
        }
    }
}
