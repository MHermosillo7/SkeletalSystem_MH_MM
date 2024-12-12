using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoomControl : MonoBehaviour
{
    List<ZoomControl> derivedControls = new List<ZoomControl>();

    public ZoomControl parentControl;

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
        parentControl = transform.parent.gameObject.GetComponent<ZoomControl>();

        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
        light = GetComponent<Highlight>();

        GetChildren();


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
    }
}
