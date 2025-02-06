using UnityEngine;
using UnityEngine.EventSystems;

namespace BodySystem
{
    public class User : MonoBehaviour
    {
        Camera cam;
        CameraMovement camMov;
        CameraStatus camStatus;

        InfoUI infoUI;
        ZoomUI zoomUI;
        HelpUI helpUI;

        GameObject selectedItem;
        GameObject previousItem;
        [SerializeField] GameObject zoomedBone;
        public Information selectedItemComp;
        public BasicComponent selectedBasicComp;

        ZoomControl selectedItemZoom;
        ZoomControl previousItemZoom;

        int selectedItemIndex;

        [SerializeField] LayerMask ignoreLayer;

        Isolate_Zoom isolate;

        // Start is called before the first frame update
        void Awake()
        {
            cam = FindObjectOfType<Camera>();

            camMov = FindObjectOfType<CameraMovement>();
            camStatus = FindObjectOfType<CameraStatus>();

            infoUI = FindObjectOfType<InfoUI>();
            zoomUI = FindObjectOfType<ZoomUI>();
            helpUI = FindObjectOfType<HelpUI>();

            isolate = FindObjectOfType<Isolate_Zoom>();

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Select();
            }
        }

        void Select()
        {
            /*  Do not continue method if cursor is over a UI element
                Or if previous camera mov hasn't finished
                (If Center Camera is called again before previous processes are
                finished, the two clash and result in unexpected camera transformations) */

            // Note to self: Sometimes this line causes a minor error where it detects a UI
            // element even when there should be none. This rarely happens with the
            // Distal Phalanges in the left arm when they are at a certain distance (far)
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit objectHit;

            if (Physics.Raycast(ray, out objectHit))
            {
                if (selectedItem == objectHit.transform.gameObject)
                {
                    DeSelect();

                    infoUI.ShowUI();
                }
                //Else object hit is another object
                else
                {
                    DeSelect();

                    ChangeSelected(objectHit.transform.gameObject);

                    infoUI.ShowUI();
                    // Else it does not hit object tagged as derived bone
                    // It resets zoom and 

                    if (!IsDerivedBone() || selectedItemZoom.layerIndex < previousItemZoom.layerIndex)
                    {
                        if (selectedItemZoom && previousItemZoom)
                        {
                            ZoomIntoCurrentLayer();
                        }
                    }

                    //If the selected object is in the same layer as current,
                    //then directly show the information UI to allow for
                    //smoother user experience and skip double clicking to
                    //access information of objects within same layer
                    else
                    {
                        infoUI.ShowUI();
                    }

                    if (selectedItemComp.needsCenter)
                    {
                        //Get child (vector) inside object hit
                        camMov.CenterCamera(selectedItemComp.pivot);
                    }
                }
            }
            else
            {
                DeSelect();
            }
        }
        void DeSelect()
        {
            infoUI.HideUI();
            helpUI.HideUI();
        }
        bool IsDerivedBone()
        {
            return selectedItem.CompareTag("DerivedBone");
        }

        public void ZoomIn()
        {
            if (selectedItemZoom.canZoomIn)
            {
                //Center camera around the pivot of previous object's parent
                camMov.CenterCamera(selectedItemComp.pivot);

                if (selectedItemZoom.parentControl == null)
                {
                    isolate.EnableLayer(selectedItemZoom.layerIndex, false);
                }
                else
                {
                    selectedItemZoom.layerZoom.EnableLayerNumber(selectedItemZoom.layerIndex, false);
                }

                selectedItemZoom.Zoom("in");

                infoUI.HideUI();

                ChangeSelected(selectedItemZoom.firstChildControl.gameObject);
            }
        }
        public void ZoomOut()
        {
            //Added multiple checks for selected item zoom component due to addition of scene
            //without any such scripts or UI types, thus, it would give errors without them
            if (selectedItemZoom)
            {
                if (selectedItemZoom.canZoomOut)
                {
                    //Zoom out of current object
                    selectedItemZoom.Zoom("out");
                    //Get parent's zoom controls

                    //Override current selected item with parent,
                    //so information pop ups are displayed at first click

                    //Override selectedItem information reference, so when info pop ups
                    //appears, information is displayed correctly

                    ChangeSelected(selectedItemZoom.parentControl.gameObject);

                    //If new item is a root in control of layer zoom
                    if(selectedItemZoom.parentControl == null)
                    {
                        //Enable the rest of roots
                        isolate.EnableLayer(0, true);

                        camMov.CenterCamera(camMov.originVector.transform);
                    }

                    //Else enable the items inside the same zone according to new layer index
                    else
                    {
                        selectedItemZoom.layerZoom.EnableLayerNumber(selectedItemZoom.layerIndex, true);

                        //Get main pivot of current section to center camera
                        camMov.CenterCamera(
                            selectedItemZoom.layerZoom.GetComponent<Information>().pivot);
                    }
                }
            }
        }
        //Zooms out of previous object until the object layer shown
        //matches the one of new selected object
        public void ZoomIntoCurrentLayer()
        {
            int newIndex = selectedItemZoom.layerIndex;
            int currentIndex = previousItemZoom.layerIndex;

            while(newIndex < currentIndex)
            {
                previousItemZoom.Zoom("out");

                previousItemZoom = previousItemZoom.parentControl;
                currentIndex = previousItemZoom.layerIndex;
            }

            previousItemZoom = selectedItemZoom.parentControl;

            camMov.CenterCameraToCurrentVector();
        }
        
        //Updates object references while keeping reference to previous item
        //Gets Information and Zoom Control scripts if available
        private void ChangeSelected(GameObject newItem)
        {
            previousItem = selectedItem;
            selectedItem = newItem;
            
            previousItemZoom = selectedItemZoom;

            if(newItem != null)
            {
                newItem.TryGetComponent<Information>(out selectedItemComp);
                newItem.TryGetComponent<BasicComponent>(out selectedBasicComp);
                selectedItemZoom = newItem.GetComponent<ZoomControl>();

                if (selectedItemZoom)
                {
                    zoomUI.UpdateZoom(selectedItemZoom.canZoomIn, selectedItemZoom.canZoomOut);
                }
            }
            else
            {
                selectedItemComp = null;
                selectedItemZoom = null;
            }
        }
        public void IsolateSelected(bool enabled)
        {
            if (selectedItemZoom.layerIndex == 0)
            {
                isolate.EnableLayer(0, true);
            }
            else
            {
                selectedItemZoom.layerZoom.EnableLayerNumber(selectedItemZoom.layerIndex, enabled);
            }
            if(enabled == true)
            {
                camMov.CenterCamera(selectedItemComp.pivot);
            }
            else
            {
                camMov.CenterCamera(previousItem.GetComponent<Information>().pivot);
            }
        }
    }
}
