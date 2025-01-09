using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BodySystem
{
    public class ZoomControl : MonoBehaviour
    {
        LayerZoom layerZoom;
        public ZoomControl parentControl;
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

        public Collider col;
        public Renderer rend;
        public Highlight highlight;

        List<GameObject> derivedBones = new List<GameObject>();
        List<Renderer> derivedRends = new List<Renderer>();
        List<Collider> derivedCols = new List<Collider>();
        List<Highlight> derivedLight = new List<Highlight>();

        // Start is called before the first frame update
        void Awake()
        {
            CheckIfRoot();

            layerZoom = transform.root.GetComponent<LayerZoom>();
            col = GetComponent<Collider>();
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
            }
        }

        void GetChildren()
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Bone") || child.CompareTag("DerivedBone"))
                {
                    childControl = child.GetComponent<ZoomControl>();

                    derivedCols.Add(childControl.col);
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
            ZoomManagement.ZoomIn(col, rend, highlight, derivedCols, derivedRends, derivedLight);
        }
        void ZoomIntoThis()
        {
            ZoomManagement.ZoomOut(col, rend, derivedCols, derivedRends, derivedLight);
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
            col.enabled = enable;
            rend.enabled = enable;

            if (!highlight.IsStartingColor())
            {
                highlight.ResetColor();
            }
        }
    }

}