using BodySystem;
using UnityEngine;
using UnityEngine.EventSystems;
 
namespace BodySystem
{
    public class V1User : MonoBehaviour
    {
        Camera cam;
        CameraMovement camMov;
        CameraStatus camStatus;

        InfoUI infoUI;
        FilterUI filterUI;
        ZoomUI_Filter zoomUI;
        HelpUI helpUI;

        public GameObject selectedItem;
        [SerializeField] GameObject zoomedBone;
        public Information selectedItemComp;
        public BasicComponent selectedBasicComp;

        public Zoom selectedItemZoom;

        IsolateFilter isolate;
        public bool isIsolated = false;

        public bool isZoomedIn = false;

        // Start is called before the first frame update
        void Awake()
        {
            cam = FindObjectOfType<Camera>();

            camMov = FindObjectOfType<CameraMovement>();
            camStatus = FindObjectOfType<CameraStatus>();

            infoUI = FindObjectOfType<InfoUI>();
            filterUI = FindObjectOfType<FilterUI>();
            zoomUI = FindObjectOfType<ZoomUI_Filter>();
            helpUI = FindObjectOfType<HelpUI>();

            isolate = FindObjectOfType<IsolateFilter>();
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
                (If CenterCamera is called again before previous processes are
                finished, the two clash and result in unexpected camera transformations)*/
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            /*The LayerMask to ignore the UI elements was interfering with selection of distant objects
            such as arm and column, causing errors when trying to select it from afar due to not
            "detecting" those objects*/
            if (Physics.Raycast(ray, out RaycastHit objectHit))
            {
                if (selectedItem == objectHit.transform.gameObject)
                {
                    DeSelect();

                    infoUI.ShowUI();
                }
                else
                {
                    DeSelect();

                    selectedItem = objectHit.transform.gameObject;

                    objectHit.transform.TryGetComponent<Information>(out selectedItemComp);

                    if (selectedItemComp == null)
                    {
                        objectHit.transform.TryGetComponent<BasicComponent>(out selectedBasicComp);
                    }

                    infoUI.ShowUI();
                    isIsolated = false;

                    // If selected object is a derived bone
                    // Don't try getting its Zoom component or its vector
                    // since we will only be handling a a switch
                    // between a parent and its children & not layers
                    if (!IsDerivedBone())
                    {
                        objectHit.transform.TryGetComponent<Zoom>(out selectedItemZoom);

                        if (selectedItemZoom)
                        {
                            zoomUI.UpdateZoom(selectedItemZoom.canZoomIn, false);
                        }
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
            filterUI.HideUI();
            helpUI.HideUI();
        }
        bool IsDerivedBone()
        {
            return selectedItem.CompareTag("DerivedBone");
        }

        public void ZoomIn()
        {
            selectedItemZoom.ZoomIn();

            isolate.IsolateObjects(false);

            camMov.CenterCamera(selectedItemComp.pivot);

            //Cannot zoom in further, can exit zoom state
            zoomUI.UpdateZoom(false,true);

            DeSelect();
            isIsolated = false;

            isZoomedIn = true;
        }
        //Added variable isZoomedIn as failsafe because of object isolation interference with filter
        //it causes hidden objects by filter to be reactivated in an attempt to zoom out from current
        //selected object
        public void ZoomOut()
        {
            if (selectedItemZoom)
            {
                selectedItemZoom.ZoomOut();

                isolate.IsolateObjects(true);

                camMov.CenterCamera(camMov.originVector.transform);
                
                zoomUI.UpdateZoom(true, false);

                DeSelect();
                isIsolated = false;

                isZoomedIn = false;
            }
        }

        //This section of the code is here and not in Isolate script due to the need of using
        //multiple references to the selected item's zoom control and the previous item
        public void IsolateSelected()
        {
            if (selectedItem.CompareTag("Bone"))
            {
                isolate.IsolateObjects(isIsolated);
            }
            if (isIsolated == true)
            {
                print("Centered to origin");
                camMov.CenterCamera(camMov.originVector.transform);
            }
            else
            {
                print("Centered to selected");
                if (selectedItemComp)
                {
                    camMov.CenterCamera(selectedItemComp.pivot);
                }
                else
                {
                    camMov.CenterCamera(selectedItem.transform.GetChild(0));
                }
            }
            DeSelect();
            isIsolated = !isIsolated;
        }

        /*if (Physics.Raycast(ray, out objectHit, ignoreLayer))
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

    selectedItem = objectHit.transform.gameObject;
                    selectedItemComp = objectHit.transform.GetComponent<Component>();

                    if (IsDerivedBone())
                    {
                        infoUI.ShowUI();
                    }

                    // If selected object is a derived bone
                    // Don't try getting its Zoom component or its vector
                    // since we will only be handling a a switch
                    // between a parent and its children & not layers
                    else
{
    if (selectedItemZoom)
    {
        selectedItemZoom.ZoomOut();
    }

    selectedItemZoom = objectHit.transform.GetComponent<Zoom>();

    //Get child (vector) inside object hit
    Transform vectorHit = selectedItem.transform.GetChild(0);

    camMov.CenterCamera(vectorHit);

    if (!zoomUI.IsUIActive())
    {
        zoomUI.ShowUI();
    }
}
                }
            }*/

    }
}
