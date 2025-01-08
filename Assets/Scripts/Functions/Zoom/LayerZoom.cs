using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerZoom : MonoBehaviour
{
    static int listSize = 20;
    int layerIndex = 0;

    public List<Transform> children = new List<Transform>();
    List<ZoomControl> childControls = new List<ZoomControl>();
    Transform child;
    ZoomControl zom;

    ZoomControl mainControl;

    
    // Start is called before the first frame update
    void Awake()
    {
        mainControl = GetComponent<ZoomControl>();
        mainControl.layerIndex = layerIndex;

        AssignLayer(transform);

        layerIndex++;
        for (int i = 0; i < children.Count; i++)
        {
            AssignLayer(children[i]);


        }
        print(layerIndex);

        ResetLayers();
    }
    void AssignLayer(Transform parent)
    {
        for(int i = 0; i < parent.childCount; i ++)
        {
            child = parent.GetChild(i);

            if (!child.CompareTag("Pivot"))
            {
                zom = child.GetComponent<ZoomControl>();
                zom.layerIndex = zom.parentControl.layerIndex + 1;

                childControls.Add(zom);
                children.Add(child);
            }
        }
    }
    void EnableLayer(List<ZoomControl> controls, bool enable)
    {
        foreach(ZoomControl c in controls)
        {
            c.Enable(enable);
        }
    }
    void ResetLayers()
    {
        mainControl.Enable(true);

        EnableLayer(childControls, false);
    }
    void ActivateLayer(
        List<Collider> parentCols, List<Renderer> parentRends,
        List<Collider> derivedCols, List<Renderer> derivedRends, List<Highlight> derivedLights)
    {
        ZoomManagement.ZoomOut(parentCols, parentRends, derivedCols, derivedRends, derivedLights);
    }

    List<Transform> TryGetChildren(Transform obj)
    {
        children.Clear();

        
           if (!obj.CompareTag("Pivot"))
           {
                    obj.GetComponent<ZoomControl>().layerIndex = layerIndex;
/*
                    children.Add(obj);*/
            }

        //Checks if object passed has a bone children
        //If not, it decreases layer index to make it correspond with layers active.
        /*
        if (children.Count == 0)
        {
            layerIndex--;
        }
        else
        {
            
        }*/

        return children;
    }
}
