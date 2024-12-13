using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManagement : MonoBehaviour
{
    //Zoom In/Out Functions
    public static void ZoomIn(Collider col, Renderer rend, Highlight light, 
        List<Collider> derivedCols, List<Renderer> derivedRends, List<Highlight> derivedLight)
    {
        if (!light.IsStartingColor())
        {
            light.ResetColor();
        }

        EnableParent(false, col, rend);

        EnableChildren(true, derivedLight, derivedCols, derivedRends);
    }

    public static void ZoomOut(Collider col, Renderer rend,
        List<Collider> derivedCols, List<Renderer> derivedRends, List<Highlight> derivedLight)
    {
        EnableParent(true, col, rend);
        
        EnableChildren(false, derivedLight, derivedCols, derivedRends);
    }

    public static void ZoomOut(
        List<Collider> parentCols, List<Renderer> parentRends,
        List<Collider> derivedCols, List<Renderer> derivedRends, List<Highlight> derivedLights)
    {
        EnableParent(true, parentCols, parentRends);

        EnableChildren(false, derivedLights, derivedCols, derivedRends);
    }

    static void EnableParent(bool enable, Collider col, Renderer rend)
    {
        col.enabled = enable;
        rend.enabled = enable;
    }

    static void EnableParent(bool enable, List<Collider> parentCol, List<Renderer> parentRend)
    {
        foreach(Collider col in parentCol)
        {
            col.enabled = enable;
        }
        foreach(Renderer rend in parentRend)
        {
            rend.enabled = enable;
        }
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
