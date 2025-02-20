using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BodySystem
{
    public class ZoomControl : MonoBehaviour
    {
        public LayerZoom layerZoom;
        public ZoomControl parentControl = null;

        //Note: DO NOT DELETE
        // Used in User script to get a reference to a child bone
        // and be able to repeatedly zoom in and still zoom out do to how
        // the ability to zoom out lies in calling a child object's ZoomOut 
        // function to reference a parent's ZoomIntoThis() function (...weird)
        public ZoomControl firstChildControl;

        ZoomControl childControl;
        List<ZoomControl> derivedControls = new List<ZoomControl>();

        public int layerIndex = 0;


        enum ControlZoomOut
        {
            ActiveParentLayer,
            ActiveParentOnly
        }

        [SerializeField] ControlZoomOut controlZoomOut = ControlZoomOut.ActiveParentOnly;

        public bool canZoomIn;
        public bool canZoomOut;
        public bool zoomOutOnClick = true;

        public List<Collider> cols = new List<Collider>();
        public Renderer rend;
        public Highlight highlight;

        List<Renderer> derivedRends = new List<Renderer>();
        List<Collider> derivedCols = new List<Collider>();
        List<Highlight> derivedLight = new List<Highlight>();

        // Start is called before the first frame update
        void Awake()
        {
            CheckIfRoot();

            layerZoom = transform.root.GetComponent<LayerZoom>();
            cols = GetComponents<Collider>().ToList();
            rend = GetComponent<Renderer>();
            highlight = GetComponent<Highlight>();
        }
        private void Start()
        {
            GetChildren();
        }

        void CheckIfRoot()
        {
            if (transform == transform.root)
            {
                canZoomOut = false;
            }
            else
            {
                canZoomOut = true;
                parentControl = transform.parent.gameObject.GetComponent<ZoomControl>();

                if(parentControl == null)
                {
                    canZoomOut = false;
                }
            }
        }

        void GetChildren()
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Bone") || child.CompareTag("DerivedBone"))
                {
                    childControl = child.GetComponent<ZoomControl>();

                    derivedCols.AddRange(childControl.cols);
                    derivedRends.Add(childControl.rend);
                    derivedLight.Add(childControl.highlight);

                    derivedControls.Add(childControl);
                }
            }

            // Checks for null because child control keeps overwriting for every bone
            // Thus, if there is at least one derived bone, child control will have a value
            if (childControl == null)
            {
                canZoomIn = false;
            }
            else
            {
                canZoomIn = true;
                firstChildControl = derivedControls[0];
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
            ZoomManagement.ZoomIn(cols, rend, highlight, derivedCols, derivedRends, derivedLight);
        }
        void ZoomIntoThis()
        {
            ZoomManagement.ZoomOut(cols, rend, derivedCols, derivedRends, derivedLight);
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

                        //This is supposed to be implemented as a way to further control layers in the future
                        //For example, if I wanted to show or hide a layer of objects depending on the selected
                        //However, as of now, implementing it would serve no immediate or susbtantial use
                        //Tt would only create more work in a short deadline

                        /*case ControlZoomOut.ActiveParentLayer:

                            layerZoom.ManageLayer(layerIndex);
                            break;
                        */
                }
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

        public void EnableChildrenWithException(bool enable, ZoomControl exception)
        {
            foreach(ZoomControl child in derivedControls)
            {
                if(child != exception)
                {
                    child.Enable(enable);
                }
            }
        }
    }

}