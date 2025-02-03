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
        ZoomUI zoomUI;
        HelpUI helpUI;

        public GameObject selectedItem;
        [SerializeField] GameObject zoomedBone;
        Component selectedItemComp;

        Zoom selectedItemZoom;

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
                selectedItemComp = objectHit.transform.GetComponent<Component>();

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

                    selectedItemZoom = objectHit.transform.GetComponent<Zoom>();


                    //Forced camera auto center sometimes leads to unsatisfactory
                    //user experience due to losing track of which bone
                    //they originally wanted to click

                    /*
                    //Get child (vector) inside object hit
                    Transform vectorHit = selectedItem.transform.GetChild(0);

                    camMov.CenterCamera(vectorHit);*/

                    if (!zoomUI.IsUIActive())
                    {
                        zoomUI.ShowUI();
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



            infoUI.HideUI();
        }
        public void ZoomOut()
        {
            if (selectedItemZoom)
            {
                selectedItemZoom.ZoomOut();

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
