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

    List<ZoomControl> allControls = new List<ZoomControl>();
    // Start is called before the first frame update
    void Start()
    {
        objects.AddRange(GetObjects());
    }
    Dictionary<ZoomControl, int> GetObjects()
    {
        Dictionary<ZoomControl, int> objs = new Dictionary<ZoomControl, int>();

        //Get all Zoom Controls in scene and add them to dictionary
        allControls.AddRange(FindObjectsOfType<ZoomControl>());

        foreach(ZoomControl control in allControls)
        {
            objs.Add(control, control.layerIndex);
        }
        return objs;
    }

    public void EnableLayer(int deactivateLayer, bool enable)
    {
        foreach(var (key,value) in objects)
        {
            if(value == deactivateLayer)
            {
                key.Enable(enable);
            }
        }
    }
}
