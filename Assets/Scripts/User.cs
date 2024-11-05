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
        UI ui;

        public GameObject selectedItem;

        [SerializeField] LayerMask ignoreLayer;

        // Start is called before the first frame update
        void Start()
        {
            cam = FindObjectOfType<Camera>();
            camMov = FindObjectOfType<CameraMovement>();
            camStatus = FindObjectOfType<CameraStatus>();
            ui = FindObjectOfType<UI>();
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
                (If coroutines are called again before previous processes are
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
                    ui.ShowUI();
                }

                //Else object hit is another object
                else
                {
                    ui.HideUI();

                    //Get child (vector) inside object hit
                    Transform vectorHit = objectHit.transform.GetChild(0);

                    //Store vectorHit's transform as camera script's vector transform
                    camMov.vectorTrans = vectorHit;

                    //Make new vector the camera's parent
                    cam.transform.parent = vectorHit;

                    camMov.ResetPrevVector();
                    selectedItem = objectHit.transform.gameObject;

                    camStatus.UpdateCamStatus(false);
                    StartCoroutine(camMov.CenterCameraRot());
                    StartCoroutine(camMov.CenterCameraPos());
                }
            }
            else
            {
                DeSelect();
            }

        }
        void DeSelect()
        {
            ui.HideUI();
        }

        void Test()
        {

        }
    }
}
