using UnityEngine;

namespace BodySystem
{
    public class User : MonoBehaviour
    {
        Camera cam;
        CameraScript camScript;
        UI ui;
        public GameObject selectedItem;

        // Start is called before the first frame update
        void Start()
        {
            cam = FindObjectOfType<Camera>();
            camScript = FindObjectOfType<CameraScript>();
            ui = FindObjectOfType<UI>();
            selectedItem = GameObject.FindGameObjectWithTag("Player");
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
                    camScript.ResetVector();

                    //Get child (vector) inside object hit
                    Transform vectorHit = objectHit.transform.GetChild(0);

                    //Store vectorHit's transform as camera script's vector transform
                    camScript.vectorTrans = vectorHit;

                    //Make new vector the camera's parent
                    cam.transform.parent = vectorHit;

                    selectedItem = objectHit.transform.gameObject;

                    //camScript.ResetCamera
                    //(Quaternion.LookRotation(cam.transform.position - vectorHit.position));
/*
                    vectorHit.rotation = LookAway(vectorHit, cam.gameObject);*/
                }

            }
        }
        void DeSelect()
        {
            
            selectedItem = null;

            ui.HideUI();
        }

        Quaternion LookAway(Transform obj, GameObject target)
        {
            Transform transform = obj;
            transform.LookAt(target.transform.position);
            return Quaternion.Inverse(transform.rotation);
        }
    }
}
