using BodySystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Isolate_Zoom : MonoBehaviour
{
    //The zoom control is the unique identifier of each object while the integer corresponds
    //to their layer number that is sometimes shared between objects

    Dictionary<ZoomControl, int> objects = new Dictionary<ZoomControl, int>(); 

    // Start is called before the first frame update
    void Awake()
    {
        //objects.AddRange<()>
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Dictionary<ZoomControl, int> GetObjects()
    {
        Dictionary<ZoomControl, int> objs = new Dictionary<ZoomControl, int>();

        //Get layer controls and then access the objects inside

        //OR

        //Iterate through all objects in scene

        //ie. too late for me to chose which to use right now
        return objs;
    }
}
