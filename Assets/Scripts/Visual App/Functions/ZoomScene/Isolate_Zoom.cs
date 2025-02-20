using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BodySystem
{
    public class Isolate_Zoom : MonoBehaviour
    {
        //The zoom control is the unique identifier of each object while the integer corresponds
        //to their layer number that is sometimes shared between objects

        Dictionary<ZoomControl, int> objects = new Dictionary<ZoomControl, int>();

        List<ZoomControl> allControls = new List<ZoomControl>();
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GetObjects());
        }
        
        /*  There is an operation error where this function retrieves objects faster than layer
            zoom can assign the layer indexes.Thus, the dictionary ended up with a bunch of
            objects all in layer zero because the indexes had not been assigned yet.

            WaitForEndOfFrame() is a failsafe to ensure getting the correct reference values*/
        IEnumerator GetObjects()
        {
            yield return new WaitForEndOfFrame();
            //Get all Zoom Controls in scene and add them to dictionary
            allControls.AddRange(FindObjectsOfType<ZoomControl>());

            foreach (ZoomControl control in allControls)
            {
                objects.Add(control, control.layerIndex);
            }
        }

        public void EnableLayer(int deactivateLayer, bool enable)
        {

            print(deactivateLayer);
            foreach (var (key, value) in objects)
            {
                if (value == deactivateLayer)
                {
                    key.Enable(enable);
                }
            }
        }

        public void EnableLayer(int deactivateLayer, bool enable, ZoomControl exception)
        {
            foreach (var (key, value) in objects)
            {
                if (value == deactivateLayer && key != exception)
                {
                    key.Enable(enable);
                }
            }
        }
    }
}
