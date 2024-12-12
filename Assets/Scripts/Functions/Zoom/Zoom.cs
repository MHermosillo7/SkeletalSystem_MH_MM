using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BodySystem
{
    public class Zoom : MonoBehaviour
    {
        Collider col;
        Renderer rend;
        Highlight comp;

        List<GameObject> derivedBones = new List<GameObject>();
        List<Renderer> derivedRends = new List<Renderer>();
        List<Collider> derivedCols = new List<Collider>();
        List<Highlight> derivedComp = new List<Highlight>();

        // Start is called before the first frame update
        void Awake()
        {
            col = this.GetComponent<Collider>();
            rend = GetComponent<Renderer>();
            comp = GetComponent<Highlight>();

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
        private void Start()
        {
            EnableChildren(false);
        }
        //Zoom In/Out Functions
        public void ZoomIn()
        {
            if (!comp.IsStartingColor())
            {
                comp.ResetColor();
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
            foreach(Highlight c in derivedComp)
            {
                if (!c.IsStartingColor())
                {
                    c.ResetColor();
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
    }
}
