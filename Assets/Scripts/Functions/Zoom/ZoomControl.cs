using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoomControl : MonoBehaviour
{
    LayerZoom layerZoom;
    ZoomControl parentControl;
    List<ZoomControl> derivedControls = new List<ZoomControl>();

    public int layerIndex;


    enum ControlZoomOut
    {
        ActiveParentLayer,
        ActiveParentOnly
    }

    [SerializeField] ControlZoomOut controlZoomOut = ControlZoomOut.ActiveParentOnly;

    public bool canZoomIn;
    public bool canZoomOut;

    public Collider col;
    public Renderer rend;
    public Highlight light;

    List<GameObject> derivedBones = new List<GameObject>();
    List<Renderer> derivedRends = new List<Renderer>();
    List<Collider> derivedCols = new List<Collider>();
    List<Highlight> derivedLight = new List<Highlight>();

    // Start is called before the first frame update
    void Start()
    {
        CheckIfRoot();

        layerZoom = transform.root.GetComponent<LayerZoom>();
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        light = GetComponent<Highlight>();

        GetChildren();
    }

    void CheckIfRoot()
    {
        if(layerIndex == 0)
        {
            canZoomOut = false;
        }
        else
        {
            canZoomOut = true;
            parentControl = transform.parent.gameObject.GetComponent<ZoomControl>();
        }
    }

    void GetChildren()
    {
        foreach(Transform child in transform)
        {
            if(child.CompareTag("Bone") || child.CompareTag("Derived Bone"))
            {
                derivedControls.Add(child.GetComponent<ZoomControl>());
            }
        }

        if(derivedControls.Count == 0)
        {
            canZoomIn = false;
        }
    }

    public void Zoom(string type)
    {
        type = type.ToLower();

        switch (type)
        {
            case "in":
                ZoomIntoChild();
                break;
            case "out":
                ZoomOut();
                break;
        }
    }

    void ZoomIntoChild()
    {
        ZoomManagement.ZoomIn(col, rend, light, derivedCols, derivedRends, derivedLight);
    }
    void ZoomIntoThis()
    {
        ZoomManagement.ZoomOut(col, rend, derivedCols, derivedRends, derivedLight);
    }
    void ZoomOut()
    {
        if (canZoomOut)
        {
            switch (controlZoomOut)
            {
                case ControlZoomOut.ActiveParentOnly:
                    parentControl.ZoomIntoThis();
                    break;

                case ControlZoomOut.ActiveParentLayer:

                    layerZoom.ManageLayer(layerIndex);
                    break;
            }
        }
    }
}
