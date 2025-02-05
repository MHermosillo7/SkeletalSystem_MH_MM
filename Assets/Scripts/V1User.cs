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

        InfoUI_Filter infoUI;
        FilterUI filterUI;
        ZoomUI_Filter zoomUI;
        HelpUI helpUI;

        public GameObject selectedItem;
        [SerializeField] GameObject zoomedBone;
        public Information selectedItemComp;

        public Zoom selectedItemZoom;

        [SerializeField] LayerMask ignoreLayer;

        IsolateFilter isolate;

        // Start is called before the first frame update
        void Awake()
        {
            cam = FindObjectOfType<Camera>();

            camMov = FindObjectOfType<CameraMovement>();
            camStatus = FindObjectOfType<CameraStatus>();

            infoUI = FindObjectOfType<InfoUI_Filter>();
            filterUI = FindObjectOfType<FilterUI>();
            zoomUI = FindObjectOfType<ZoomUI_Filter>();
            helpUI = FindObjectOfType<HelpUI>();

            isolate = FindObjectOfType<IsolateFilter>();
            zoomUI.EnableButton(false);
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

            if (Physics.Raycast(ray, out objectHit, ignoreLayer))
            {
                DeSelect();

                selectedItem = objectHit.transform.gameObject;
                selectedItemComp = objectHit.transform.GetComponent<Information>();

                infoUI.ShowUI();
                

                // If selected object is a derived bone
                // Don't try getting its Zoom component or its vector
                // since we will only be handling a a switch
                // between a parent and its children & not layers
                if(!IsDerivedBone())
                {
                    if (selectedItemZoom)
                    {
                        selectedItemZoom.ZoomOut();
                    }

                    objectHit.transform.TryGetComponent<Zoom>(out selectedItemZoom);

                    //Forced camera auto center sometimes leads to unsatisfactory
                    //user experience due to losing track of which bone
                    //they originally wanted to click

                    /*
                    //Get child (vector) inside object hit
                    Transform vectorHit = selectedItem.transform.GetChild(0);

                    camMov.CenterCamera(vectorHit);*/

                    if (!zoomUI.IsUIActive() && selectedItemZoom)
                    {
                        zoomUI.EnableButton(true);
                    }
                    else zoomUI.EnableButton(false);
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
            zoomUI.HideUI();
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
            infoUI.HideUI();
        }
        public void ZoomOut()
        {
            if (selectedItemZoom)
            {
                selectedItemZoom.ZoomOut();
                isolate.IsolateObjects(true);

                camMov.CenterCamera(camMov.originVector.transform);
                infoUI.HideUI();
            }
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
