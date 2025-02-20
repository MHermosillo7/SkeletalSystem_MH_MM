using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodySystem
{
    public class ZoomManagement : MonoBehaviour
    {
        //Zoom In/Out Functions
        public static void ZoomIn(List<Collider> cols, Renderer rend, Highlight light,
            List<Collider> derivedCols, List<Renderer> derivedRends, List<Highlight> derivedLight)
        {
            if (!light.IsStartingColor())
            {
                light.ResetColor();
            }

            EnableParent(false, cols, rend);

            EnableChildren(true, derivedLight, derivedCols, derivedRends);
        }

        public static void ZoomOut(List<Collider> cols, Renderer rend,
            List<Collider> derivedCols, List<Renderer> derivedRends, List<Highlight> derivedLight)
        {
            EnableParent(true, cols, rend);

            EnableChildren(false, derivedLight, derivedCols, derivedRends);
        }

        public static void ZoomOut(
            List<Collider> parentCols, List<Renderer> parentRends,
            List<Collider> derivedCols, List<Renderer> derivedRends, List<Highlight> derivedLights)
        {
            EnableParent(true, parentCols, parentRends);

            EnableChildren(false, derivedLights, derivedCols, derivedRends);
        }

        public static void EnableParent(bool enable, List<Collider> cols, Renderer rend)
        {
            foreach (Collider col in cols)
            {
                col.enabled = enable;
            }
            rend.enabled = enable;
        }

        public static void EnableParent(bool enable, List<Collider> parentCol, List<Renderer> parentRend)
        {
            foreach (Collider col in parentCol)
            {
                col.enabled = enable;
            }
            foreach (Renderer rend in parentRend)
            {
                rend.enabled = enable;
            }
        }

        public static void EnableChildren(bool enable,
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
}
