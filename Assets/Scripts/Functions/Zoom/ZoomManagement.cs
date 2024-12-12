using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManagement : MonoBehaviour
{
    //Zoom In/Out Functions
    public static void ZoomIn(Collider col, Renderer rend, Highlight light, 
        List<Highlight> derivedLight, List<Collider> derivedCols, List<Renderer> derivedRends)
    {
        if (!light.IsStartingColor())
        {
            light.ResetColor();
        }
        EnableParent(false, col, rend);

        EnableChildren(true, derivedLight, derivedCols, derivedRends);
    }

    public static void ZoomOut(bool enableLayer, 
        Transform obj, Collider col, Renderer rend, Highlight light,
        List<Highlight> derivedLight, List<Collider> derivedCols, List<Renderer> derivedRends)
    {
        EnableParent(true, col, rend);

        EnableChildren(false, derivedLight, derivedCols, derivedRends);

        if (enableLayer)
        {

        }
    }

    static void EnableParent(bool enable, Collider col, Renderer rend)
    {
        col.enabled = enable;
        rend.enabled = enable;
    }

    static void EnableChildren(bool enable, 
        List<Highlight> derivedLight, List<Collider> derivedCols, List<Renderer> derivedRends)
    {
        foreach (Highlight c in derivedLight)
        {
            if (!c.IsStartingColor())
            {
                c.ResetColor();
            }
        }

        foreach (Collider c in derivedCols)
        {
            c.enabled = enable;
        }

        foreach (Renderer r in derivedRends)
        {
            r.enabled = enable;
        }
    }
}
