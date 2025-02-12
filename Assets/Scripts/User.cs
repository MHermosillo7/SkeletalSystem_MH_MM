using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BodySystem
{
    public class User : MonoBehaviour
    {
        //Reference to Camera Related Functions
        Camera cam;
        CameraMovement camMov;
        CameraStatus camStatus;

        //Pop Up UI references
        InfoUI infoUI;
        ZoomUI zoomUI;
        HelpUI helpUI;

        //Selected Item References
        /*  Used for camera center, information retrieval, or zoom calculations*/
        GameObject selectedItem;
        [SerializeField] GameObject zoomedBone;
        public Information selectedItemComp;
        public BasicComponent selectedBasicComp;
        ZoomControl selectedItemZoom;

        //Previous Item References
        /*  Used for camera center or zoom calculations*/
        GameObject previousItem;
        ZoomControl previousItemZoom;

        //Object layer to ignore when selecting objects
        /*  First intended to avoid collission with UI elements,
            later replaced with EventSystem.current.IsPointerOverGameObject()
            Note: Current use remains untested*/
        [SerializeField] LayerMask ignoreLayer;

        //Reference to Object Isolation script (Zoom Level version
        Isolate_Zoom isolate;

        //Controls whether to isolate object or not
        bool isIsolated = false;

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
            //When clicking left mouse button
            //Call for Select object logic
            if (Input.GetMouseButtonDown(0))
            {
                Select();
            }
        }

        //Handles all logic when selecting a new object (camera center, pop ups, etc)
        void Select()
        {
            /*  Do not continue method if cursor is over a UI element
                Or if previous camera mov hasn't finished
                (If Center Camera is called again before previous processes are
                finished, the two clash and result in unexpected camera transformations)

                Note to self: Sometimes this line causes a minor error where it detects a UI
                element even when there should be none.This rarely happens with the
                Distal Phalanges in the left arm when they are at a certain distance(far)*/
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit objectHit;

            if (Physics.Raycast(ray, out objectHit))
            {
                /*  If the user selects same object, show the information UI and hide other UI types
                    Serves to avoid repeating unnecessary logic required for new selected objects*/
                if (selectedItem == objectHit.transform.gameObject)
                {
                    DeSelect();

                    infoUI.ShowUI();
                }

                //Else object hit is another object
                else
                {
                    //Hide all UI pop ups
                    DeSelect();

                    //Change/Update all selected object references to new object's
                    ChangeSelected(objectHit.transform.gameObject);

                    //Display object's information UI
                    infoUI.ShowUI();

                    /*  If user selects an object tagged not tagged as derived bone
                        or in a lower layer than the current(now previous after re-selecting)
                        It resets zoom levels to zoom level of new object*/
                    if (!IsDerivedBone() || selectedItemZoom.layerIndex < previousItemZoom.layerIndex)
                    {
                        /*  Avoids calling for references that do not exist, in case 
                            function is called when selecting a non - derived bone*/
                        if (selectedItemZoom && previousItemZoom)
                        {
                            /*  Previously, if after selecting a main bone, the user selected another
                                main bone, the ZoomIntoCurrentLayer got triggered since the selected
                                item is not a derived bone, and a previous item had been selected,
                                All the previous conditions to trigger the function were completed

                                With this, however, if the previous bone and the current bone are both
                                main bones in layer 0, the line is supposed to not be triggered*/
                            if (previousItemZoom.layerIndex + selectedItemZoom.layerIndex != 0)
                            {
                                ZoomIntoCurrentLayer();
                            }
                        }
                    }

                    /*  Else the selected object is in the same layer as current,
                        then directly show the information UI to allow for
                        smoother user experience and skip double clicking to
                        access information of objects within same layer
                    
                        Note: Possibly now obsolete due to above logic activating information UI*/
                    else
                    {
                        infoUI.ShowUI();
                    }

                    /*  Fail safe to allow for camera centering to 
                        selected objects in case by case basis

                        Note: Currently unused as far as I know*/
                    if (selectedItemComp.needsCenter)
                    {
                        //Get child (vector) inside object hit
                        camMov.CenterCamera(selectedItemComp.pivot);
                    }
                }
            }
            // If user selects no object (the void)
            // Then hide all UI pop ups
            else
            {
                DeSelect();
            }
        }

        //Hides all UI references
        /*  Helps ensure all UI pop ups are hiddens
            Reducescode repetition*/
        void DeSelect()
        {
            infoUI.HideUI();
            helpUI.HideUI();
        }

        // Quick acess to knowing if currrently selected
        // object is a main or derived (part of a) bone
        // Helps minimize code repetition and readability
        bool IsDerivedBone()
        {
            return selectedItem.CompareTag("DerivedBone");
        }

        //Zooms into object, if possible 
        //and hides objects in layers different from that of new objects
        public void ZoomIn()
        {
            if (selectedItemZoom.canZoomIn)
            {
                //Center camera around the pivot of selected object's parent
                camMov.CenterCamera(selectedItemComp.pivot);

                //If object is a main bone
                if (selectedItemZoom.parentControl == null)
                {
                    //Disables all main bones with index 0
                    isolate.EnableLayer(selectedItemZoom.layerIndex, false);
                }
                //Else object is a derived bone
                else
                {
                    //Hide all bones with same index as selected item (bone)
                    selectedItemZoom.layerZoom.EnableLayerNumber(selectedItemZoom.layerIndex, false);
                }

                //Zoom into object to see components
                selectedItemZoom.Zoom("in");

                //Hide Information UI pop up
                infoUI.HideUI();

                //Update references using first child (bone) of selected object
                ChangeSelected(selectedItemZoom.firstChildControl.gameObject);

                //Fail safe to ensure next time function to hide selected object is called
                //it can properly do its purpose of hiding selected object
                isIsolated = false;
            }
        }
        //Zooms out of selected object, if possible
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
                    
                    //Update object references so information is displayed properly at first click
                    ChangeSelected(selectedItemZoom.parentControl.gameObject);

                    //If new item is a root in control of layer zoom
                    if(selectedItemZoom.parentControl == null)
                    {
                        //Enable the rest of roots
                        isolate.EnableLayer(0, true);

                        //Center camera to main, origin vector
                        camMov.CenterCamera(camMov.originVector.transform);
                    }

                    //Else enable the items inside the same zone according to new layer index
                    else
                    {
                        //Enable all objects in same layer as new object
                        selectedItemZoom.layerZoom.EnableLayerNumber(selectedItemZoom.layerIndex, true);

                        //Get main pivot of current section to center camera
                        camMov.CenterCamera(
                            selectedItemZoom.layerZoom.GetComponent<Information>().pivot);
                    }

                    //Hide all UI pop ups
                    DeSelect();

                    //Fail safe to ensure next time function to hide selected object is called
                    //it can properly do its purpose of hiding selected object
                    isIsolated = false;
                }
            }
        }

        //Zooms out of previous object until the object layer
        //displayed matches the one of new selected object
        //Centers camera to current vector

        /*  Note: possibly now unused due to hiding of objects in layers different from those of
            selected object*/
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
        //Gets Information and Basic Component scripts depending on which is available
        private void ChangeSelected(GameObject newItem)
        {
            previousItem = selectedItem;
            selectedItem = newItem;
            
            previousItemZoom = selectedItemZoom;

            if(newItem != null)
            {
                newItem.TryGetComponent<Information>(out selectedItemComp);

                if(selectedItemComp == null)
                {
                    selectedBasicComp = newItem.GetComponent<BasicComponent>();
                }

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
        //This section of the code is here and not in Isolate script due to the need of using
        //multiple references to the selected item's zoom control and the previous item
        public void IsolateSelected(bool enabled, ZoomControl exception = null)
        {
            //ISOLATION CONTROL

            //If selected object is a main bone
            if (selectedItemZoom.layerIndex == 0)
            {
                //Enable/Disable all main bones
                //Depending on whether exception is not null, it will not deactivate that object
                isolate.EnableLayer(0, enabled, exception);
            }

            //Else if there is an object exception, and selected object is a derived bone
            else if(exception != null)
            {
                //Enable/Disable all objects in same layer except for the object exception
                selectedItemZoom.parentControl.EnableChildrenWithException(enabled, exception);
            }

            //Else there is no object exception, but selected object is a derived bone
            else
            {
                //Enable/Disable all bones in same layer as selected object, including itself
                selectedItemZoom.layerZoom.EnableLayerNumber(selectedItemZoom.layerIndex, enabled);
            }

            //CAMERA CENTER CONTROL

            //If objects were enabled and selected object is a main bone
            if(enabled == true && selectedItemZoom.layerIndex == 0)
            {
                //Center camera to origin vector
                print("Centered to origin");
                camMov.CenterCamera(camMov.originVector.transform);
            }

            //Else if objects were enabled, selected object is a derived bone,
            //and there was a previous selected object
            else if (enabled == true && previousItem && selectedItemZoom.layerIndex > 0)
            {
                //Center camera to previous item's pivot
                print("Centered to previous");
                camMov.CenterCamera(previousItem.GetComponent<Information>().pivot);
            }

            //Else objects were disabled
            //Center camera to selected object
            else
            {
                print("Centered to selected");

                //Right now there are some items that have no vector because their rotation is messed up
                //Thus, I just double check there is a pivot before centering the camera to selected
                //Or, in case there is no pivot, I center to origin
                if(selectedItemComp.pivot != null)
                {
                    camMov.CenterCamera(selectedItemComp.pivot);
                }
                else
                {
                    camMov.CenterCamera(camMov.originVector.transform);
                }
            }

            //Hide all UI pop ups
            DeSelect();
        }

        //Enables selected object isolation similarly to an on/off switch
        public void IsolateObject()
        {
            IsolateSelected(isIsolated, selectedItemZoom);
            isIsolated = !isIsolated;
        }
    }
}
