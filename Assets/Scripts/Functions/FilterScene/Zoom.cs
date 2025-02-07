using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BodySystem
{
    public class Zoom : MonoBehaviour
    {
        List<Collider> cols = new List<Collider>();
        Renderer rend;
        Highlight highlight;

        List<Renderer> derivedRends = new List<Renderer>();
        List<Collider> derivedCols = new List<Collider>();
        List<Highlight> derivedLight = new List<Highlight>();

        public bool canZoomIn;
        public bool canZoomOut;

        // Start is called before the first frame update
        void Awake()
        {
            cols = GetComponents<Collider>().ToList();
            rend = GetComponent<Renderer>();
            highlight = GetComponent<Highlight>();
        }
        private void Start()
        {
            EnableChildren(false);
        }
        void GetChildren()
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("DerivedBone"))
                {
                    derivedRends.Add(child.GetComponent<Renderer>());
                    derivedCols.Add(child.GetComponent<Collider>());
                    derivedLight.Add(child.GetComponent<Highlight>());
                }
            }

            // Checks for null in derived rends because it stores a Renderer for each derived bone
            // Thus, if there is at least one derived bone, derivedRend will have a value
            if (derivedRends == null)
            {
                canZoomIn = false;
            }
            else
            {
                canZoomIn = true;
            }
        }
        //Zoom In/Out Functions
        public void ZoomIn()
        {
            if (!highlight.IsStartingColor())
            {
                highlight.ResetColor();
            }
            EnableParent(false);

            EnableChildren(true);
        }

        public void ZoomOut()
        {
            EnableChildren(false);

            EnableParent(true);
        }

        void EnableParent(bool enable)
        {
            foreach(Collider col in cols)
            {
                col.enabled = enable;
            }
            rend.enabled = enable;
        }

        void EnableChildren(bool enable)
        {
            foreach(Highlight l in derivedLight)
            {
                if (!l.IsStartingColor())
                {
                    l.ResetColor();
                }
            }

            foreach(Collider c in derivedCols)
            {
                c.enabled = enable;
            }

            foreach(Renderer r in derivedRends)
            {
                r.enabled = enable;
            }
        }
        public void Enable(bool enable)
        {
            if (!highlight.IsStartingColor())
            {
                highlight.ResetColor();
            }

            foreach (Collider col in cols)
            {
                col.enabled = enable;
            }
 
            rend.enabled = enable;
        }
    }
}
