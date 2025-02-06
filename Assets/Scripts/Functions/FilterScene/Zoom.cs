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

        // Start is called before the first frame update
        void Awake()
        {
            cols = GetComponents<Collider>().ToList();
            rend = GetComponent<Renderer>();
            highlight = GetComponent<Highlight>();

            foreach(Transform child in transform)
            {
                if (child.CompareTag("DerivedBone"))
                {
                    derivedRends.Add(child.GetComponent<Renderer>());
                    derivedCols.Add(child.GetComponent<Collider>());
                    derivedLight.Add(child.GetComponent<Highlight>());
                }
            }

        }
        private void Start()
        {
            EnableChildren(false);
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
