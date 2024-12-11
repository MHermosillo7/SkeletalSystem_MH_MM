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

        public GameObject selectedItem;
        [SerializeField] GameObject zoomedBone;
        Information selectedItemComp;

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
                    selectedItemComp = objectHit.transform.GetComponent<Information>();

                    // Else it does not hit object tagged as derived bone
                    // It resets zoom and 
                    if(!IsDerivedBone())
                    {
                        if (selectedItemZoom)
                        {
                            selectedItemZoom.ZoomOut();
                        }

                        if (!zoomUI.IsUIActive()) 
                        {
                            zoomUI.EnableButton(true);
                        }
                    }

                    if (selectedItemComp.needsCenter || selectedItem.CompareTag("Bone"))
                    {
                        //Get child (vector) inside object hit
                        camMov.CenterCamera(selectedItemComp.pivot);
                    }

                    objectHit.transform.TryGetComponent<Zoom>(out selectedItemZoom);
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

            camMov.CenterVector();

            infoUI.HideUI();
        }
        public void ZoomOut()
        {
            selectedItemZoom.ZoomOut();

            selectedItem = null;

            camMov.CenterVector();

            
        }

    }
}
