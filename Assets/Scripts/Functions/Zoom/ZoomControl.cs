using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoomControl : MonoBehaviour
{
    LayerZoom layerZoom;
    public ZoomControl parentControl;

    ZoomControl childControl;
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
    public bool zoomOutOnClick = true;

    public Collider col;
    public Renderer rend;
    public Highlight light;

    List<GameObject> derivedBones = new List<GameObject>();
    List<Renderer> derivedRends = new List<Renderer>();
    List<Collider> derivedCols = new List<Collider>();
    List<Highlight> derivedLight = new List<Highlight>();

    // Start is called before the first frame update
    void Awake()
    {
        layerZoom = transform.root.GetComponent<LayerZoom>();
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        light = GetComponent<Highlight>();
    }
    private void Start()
    {
        CheckIfRoot();
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
            if(child.CompareTag("Bone") || child.CompareTag("DerivedBone"))
            {
                childControl = child.GetComponent<ZoomControl>();

                derivedCols.Add(childControl.col);
                derivedRends.Add(childControl.rend);
                derivedLight.Add(childControl.light);
            }
        }

        // Checks for null because child control keeps overwriting for every bone
        // Thus, if there is at least one derived bone, child control will have a value
        if(childControl == null)
        {
            canZoomIn = false;
        }
        else
        {
            canZoomIn = true;
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
