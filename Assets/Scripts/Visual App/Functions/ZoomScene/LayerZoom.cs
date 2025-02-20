using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodySystem
{
    public class LayerZoom : MonoBehaviour
    {
        int layerIndex = 0;
        public Transform trans;
        List<Transform> children = new List<Transform>();
        public List<ZoomControl> childControls = new List<ZoomControl>();
        Transform child;
        public ZoomControl zom;

        ZoomControl mainControl;

        // Start is called before the first frame update
        void Awake()
        {
            trans = GetComponent<Transform>();
            mainControl = GetComponent<ZoomControl>();
            mainControl.layerIndex = layerIndex;
        }
        private void Start()
        {
            AssignLayer(transform);

            layerIndex++;
            for (int i = 0; i < children.Count; i++)
            {
                AssignLayer(children[i]);
            }

            ResetLayers();
        }
        public void AssignLayer(Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                child = parent.GetChild(i);

                if (!child.CompareTag("Pivot"))
                {
                    zom = child.GetComponent<ZoomControl>();
                    zom.layerIndex = zom.parentControl.layerIndex + 1;

                    childControls.Add(zom);
                    children.Add(child);
                }
            }
        }
        void EnableLayer(List<ZoomControl> controls, bool enable)
        {
            foreach (ZoomControl c in controls)
            {
                c.Enable(enable);
            }
        }
        public void EnableLayerNumber(int layerIndex, bool enable, ZoomControl exception = null)
        {
            foreach(ZoomControl control in childControls)
            {
                if(control.layerIndex == layerIndex && control != exception)
                {
                    control.Enable(enable);
                }
            }
        }
        void ResetLayers()
        {
            mainControl.Enable(true);

            EnableLayer(childControls, false);
        }
        void ActivateLayer(
            List<Collider> parentCols, List<Renderer> parentRends,
            List<Collider> derivedCols, List<Renderer> derivedRends, List<Highlight> derivedLights)
        {
            ZoomManagement.ZoomOut(parentCols, parentRends, derivedCols, derivedRends, derivedLights);
        }
    }
}
