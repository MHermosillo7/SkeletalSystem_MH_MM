using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity;
using UnityEngine;

namespace BodySystem
{
    public class Zoom : MonoBehaviour
    {
        Collider col;
        Renderer rend;
        Highlight highlight;

        Zoom childControl;

        List<Renderer> derivedRends = new List<Renderer>();
        List<Collider> derivedCols = new List<Collider>();
        List<Highlight> derivedLight = new List<Highlight>();

        // Start is called before the first frame update
        void Awake()
        {
            col = this.GetComponent<Collider>();
            rend = GetComponent<Renderer>();
            highlight = GetComponent<Highlight>();

            foreach (Transform child in transform)
            {
                if (child.CompareTag("Bone") || child.CompareTag("DerivedBone"))
                {
                    childControl = child.GetComponent<Zoom>();

                    childControl.
                }
            }
            foreach(Transform child in transform)
            {

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
            col.enabled = enable;
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

            col.enabled = enable;
            rend.enabled = enable;
        }
    }
}
