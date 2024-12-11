/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2Zoom : MonoBehaviour
{
    int layer;

    List<GameObject> layerOneBones = new List<GameObject>();
    List<Renderer> layerOneRends = new List<Renderer>();
    List<Collider> layerOneCols = new List<Collider>();
    List<Highlight> derivedComp = new List<Highlight>();
    // Start is called before the first frame update

    void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("DerivedBone"))
            {
                derivedBones.Add(child.gameObject);

            }
            if (child.CompareTag("Bone"))
            {
                derivedBones.Add(child.gameObject);
            }
        }

        derivedRends = derivedBones.Select(b => b.GetComponent<Renderer>()).ToList();
        derivedCols = derivedBones.Select(b => b.GetComponent<Collider>()).ToList();
        derivedComp = derivedBones.Select(b => b.GetComponent<Highlight>()).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/
