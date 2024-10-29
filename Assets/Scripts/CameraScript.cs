using UnityEngine;

namespace BodySystem
{
    public class CameraScript : MonoBehaviour
    {
        float horizontalInput;
        float verticalInput;

        int rotateSpeed = 750;
        int zoomSpeed = 600;
        int slideSpeed = 200;

        float rotateProgress = 0;
        float rotateTime = 1;
        float rotatePerc;

        GameObject vector;
        public Transform vectorTrans;
        public GameObject prevVector;

        Quaternion startQuaternion;

        // Start is called before the first frame update
        void Start()
        {
            vector = GameObject.FindWithTag("MainPivot");
            vectorTrans = vector.transform;
            prevVector = vector;
            startQuaternion = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(2))
            {
                horizontalInput = GetInput("Mouse X", rotateSpeed);
                verticalInput = GetInput("Mouse Y", rotateSpeed);

                MoveCamera();
            }

            Zoom();

            //It is using Left Control and Right Mouse Click (NOT LEFT)
            if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl))
            {
                Slide();
            }
        }

        void MoveCamera()
        {
            vectorTrans.Rotate
                (Vector3.right, verticalInput * -1);
            vectorTrans.Rotate
                (Vector3.up, horizontalInput, Space.World);
        }


        void Zoom()
        {
            transform.Translate(Vector3.forward * GetInput("Scroll Wheel", zoomSpeed));
        }

        //WORK IN PROGRESS
        void Slide()
        {
            transform.Translate(Vector3.up * -GetInput("Mouse Y", slideSpeed));
            transform.Translate(Vector3.right * -GetInput("Mouse X", slideSpeed));
        }


        float GetInput(string axis, int speed)
        {
            float input = Input.GetAxis(axis) * speed * Time.deltaTime;
            return input;
        }

        public void ResetCamera(Quaternion targetQuaternion)
        {
            while(rotateProgress > rotateTime)
            {
                rotatePerc = rotateProgress / rotateTime;

                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetQuaternion, rotatePerc * Time.deltaTime);
                rotateProgress += Time.deltaTime;
            }
            rotateProgress = 0;
        }

        public void ResetPrevVector()
        {
            prevVector.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
