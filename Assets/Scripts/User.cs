using UnityEngine;
using Unity.UI;

namespace BodySystem
{
    public class User : MonoBehaviour
    {
        Camera cam;
        CameraScript camScript;
        UI ui;

        public GameObject selectedItem;

        [SerializeField] LayerMask ignoreLayer;

        // Start is called before the first frame update
        void Start()
        {
            cam = FindObjectOfType<Camera>();
            camScript = FindObjectOfType<CameraScript>();
            ui = FindObjectOfType<UI>();
        }

        // Update is called once per frame
        void Update()
        {
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit objectHit;

            if (Physics.Raycast(ray, out objectHit))
            {
                if (selectedItem == objectHit.transform.gameObject)
                {
                    ui.ShowUI();
                }

                //Else object hit is another object
                else
                {
                    //Reset vector rotation

                    //Get child (vector) inside object hit
                    Transform vectorHit = objectHit.transform.GetChild(0);

                    //Store vectorHit's transform as camera script's vector transform
                    camScript.vectorTrans = vectorHit;

                    //Make new vector the camera's parent
                    cam.transform.parent = vectorHit;

                    camScript.ResetPrevVector();
                    selectedItem = objectHit.transform.gameObject;

                    camScript.cameraCanMove = false;
                    StartCoroutine(camScript.CenterCameraRot());
                    StartCoroutine(camScript.CenterCameraPos());
                }
            }
            //Work in Progress
            else if (objectHit.transform.gameObject.layer != ignoreLayer)
            {
                DeSelect();
            }

        }
        void DeSelect()
        {
            ui.HideUI();
        }

        Quaternion LookAway(Transform obj, GameObject target)
        {
            Transform transform = obj;
            transform.LookAt(target.transform.position);
            return Quaternion.Inverse(transform.rotation);
        }
        void MovePanel()
        {
            
        }
    }
}
