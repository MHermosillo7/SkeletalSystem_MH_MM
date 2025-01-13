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
        FilterUI filterUI;
        ZoomUI zoomUI;
        HelpUI helpUI;

        GameObject selectedItem;
        [SerializeField] GameObject zoomedBone;
        public Information selectedItemComp;

        ZoomControl selectedItemZoom;
        ZoomControl previousItemZoom;

        int selectedItemIndex;

        [SerializeField] LayerMask ignoreLayer;

        // Start is called before the first frame update
        void Awake()
        {
            cam = FindObjectOfType<Camera>();

            camMov = FindObjectOfType<CameraMovement>();
            camStatus = FindObjectOfType<CameraStatus>();

            infoUI = FindObjectOfType<InfoUI>();
            filterUI = FindObjectOfType<FilterUI>();
            zoomUI = FindObjectOfType<ZoomUI>();
            helpUI = FindObjectOfType<HelpUI>();

            if (zoomUI)
            {
                zoomUI.EnableButton(false);
            }
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
                    // Else it does not hit object tagged as derived bone
                    // It resets zoom and 

                    if (!IsDerivedBone() || selectedItemZoom.layerIndex < previousItemZoom.layerIndex)
                    {
                        if (selectedItemZoom && previousItemZoom)
                        {
                            ZoomIntoCurrentLayer();
                        }

                        if (zoomUI) 
                        {
                            if (!zoomUI.IsUIActive())
                            {
                                zoomUI.EnableButton(true);
                            }
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

                    if (selectedItemComp.needsCenter || selectedItem.CompareTag("Bone"))
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
            if (filterUI)
            {
                filterUI.HideUI();
            }
            if (zoomUI)
            {
                zoomUI.HideUI();
            }
        }
        bool IsDerivedBone()
        {
            return selectedItem.CompareTag("DerivedBone");
        }

        public void ZoomIn()
        {
            if (selectedItemZoom.canZoomIn)
            {
                selectedItemZoom.Zoom("in");

                camMov.CenterVector();

                infoUI.HideUI();

                ChangeSelected(selectedItemZoom.firstChildControl.gameObject);
                //zoomUI.EnableButton(false);
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

                    //Center camera around the pivot of previous object's parent
                    camMov.CenterCamera(selectedItemComp.pivot);
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

            selectedItem = newItem;

            previousItemZoom = selectedItemZoom;

            if(newItem != null)
            {
                selectedItemComp = newItem.GetComponent<Information>();
                newItem.TryGetComponent<ZoomControl>(out selectedItemZoom);

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
    }
}
