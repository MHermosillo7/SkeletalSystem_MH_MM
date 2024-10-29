using UnityEngine;

namespace BodySystem
{
    public class User : MonoBehaviour
    {
        Camera cam;
        CameraScript camScript;
        public GameObject selectedItem;

        // Start is called before the first frame update
        void Start()
        {
            cam = FindObjectOfType<Camera>();
            camScript = FindObjectOfType<CameraScript>();
            selectedItem = GameObject.FindGameObjectWithTag("Player");
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit objectHit;

            if (Physics.Raycast(ray, out objectHit))
            {
                Transform vectorHit = objectHit.transform.GetChild(0);

                camScript.vectorTrans = vectorHit;
                cam.transform.parent = vectorHit;
                camScript.prevVector = vectorHit.gameObject;
                selectedItem = objectHit.transform.gameObject;

                camScript.ResetCamera
                    (Quaternion.LookRotation(cam.transform.position - vectorHit.position));
                //cam.transform.LookAt(vectorHit.position);

                camScript.ResetPrevVector();
            }
        }
    }
}
