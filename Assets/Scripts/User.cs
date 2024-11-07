using UnityEngine;
using Unity.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BodySystem
{
    public class User : MonoBehaviour
    {
        Camera cam;
        CameraMovement camMov;
        CameraStatus camStatus;
        InfoPopUI infoUI;

        public GameObject selectedItem;

        [SerializeField] LayerMask ignoreLayer;

        // Start is called before the first frame update
        void Start()
        {
            cam = FindObjectOfType<Camera>();
            camMov = FindObjectOfType<CameraMovement>();
            camStatus = FindObjectOfType<CameraStatus>();
            infoUI = FindObjectOfType<InfoPopUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Test();
            }
            if (Input.GetMouseButtonDown(0))
            {
                Select();
            }
            if(Input.GetMouseButtonDown(1))
            {
                DeSelect();
            }
        }

        void Select()
        {
            /*  Do not continue method if cursor is over a UI element
                Or if previous camera mov hasn't finished
                (If Center Camera is called again before previous processes are
                finished, the two clash and result in unexpected camera transformations) */
            if (EventSystem.current.IsPointerOverGameObject() || !camStatus.cameraCanMove)
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit objectHit;

            if (Physics.Raycast(ray, out objectHit, ignoreLayer))
            {
                if (selectedItem == objectHit.transform.gameObject)
                {
                    infoUI.ShowUI();
                }

                //Else object hit is another object
                else
                {
                    infoUI.HideUI();

                    //Get child (vector) inside object hit
                    Transform vectorHit = objectHit.transform.GetChild(0);

                    selectedItem = objectHit.transform.gameObject;

                    camMov.CenterCamera(vectorHit);

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
        }

        void Test()
        {

        }
    }
}
