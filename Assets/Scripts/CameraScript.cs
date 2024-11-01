using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using UnityEditor;

namespace BodySystem
{
    public class CameraScript : MonoBehaviour
    {
        float horizontalInput;
        float verticalInput;

        int rotateSpeed = 750;
        int slideSpeed = 100;
        float zoomSpeed = 1.5f;

        float rotProgress = 0;
        float posProgress = 0;
        float endTime = 1;

        GameObject vector;
        public Transform vectorTrans;
        Transform prevVectorTrans;

        public bool cameraCanMove = true;

        // Start is called before the first frame update
        void Start()
        {
            vector = GameObject.FindWithTag("MainPivot");
            vectorTrans = vector.transform;
            prevVectorTrans = vectorTrans;
        }

        // Update is called once per frame
        void Update()
        {
            if (cameraCanMove)
            {
                if (Input.GetMouseButton(2))
                {
                    horizontalInput = GetInput("Mouse X", rotateSpeed);
                    verticalInput = GetInput("Mouse Y", rotateSpeed);

                    MoveCamera();
                }

                ZoomA();

                //It is using Left Control and Right Mouse Click (NOT LEFT)
                if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl))
                {
                    Slide();
                }
            }

            else
            {
                if(rotProgress == 0 && posProgress == 0)
                {
                    cameraCanMove = true;
                }
                else
                {
                    Console.WriteLine("Wait! Modifying camera position...");
                }
            }
        }

        //Rotate camera around object in all axis (almost)
        void MoveCamera()
        {
            //Rotate vertically
            vectorTrans.Rotate(Vector3.right, verticalInput * -1);

            //Rotate horizontally
            vectorTrans.Rotate(Vector3.up, horizontalInput, Space.World);
        }
/*
        //Zoom in constant amounts (supposedly)
        void ZoomC()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Input.GetAxis("Scroll Wheel") > 0)
            {
                transform.Translate(ray.direction + Vector3.forward * zoomSpeed);
            }

            else if (Input.GetAxis("Scroll Wheel") < 0)
            {
                transform.Translate(ray.direction* -1 + Vector3.forward * -zoomSpeed);
            }
        }*/
        void ZoomA()
        {
            if (Input.GetAxis("Scroll Wheel") > 0)
            {
                transform.Translate(Vector3.forward * zoomSpeed);
            }

            else if (Input.GetAxis("Scroll Wheel") < 0)
            {
                transform.Translate(Vector3.forward * -zoomSpeed);
            }
        }

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

        public IEnumerator CenterCameraPos()
        {
            while (posProgress < endTime)
            {
                transform.position =
                    Vector3.Slerp(transform.position, 
                        new Vector3(vectorTrans.position.x, vectorTrans.position.y, -8f),
                        posProgress);
                posProgress += Time.deltaTime;

                yield return null;
            }
            transform.position = new Vector3(vectorTrans.position.x, vectorTrans.position.y, -8f);

            posProgress = 0;
        }

        public IEnumerator CenterCameraRot()
        {
            while (rotProgress < endTime)
            {
                float t = rotProgress / endTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, rotProgress);
                rotProgress += Time.deltaTime;

                yield return null;
            }
            rotProgress = 0;
        }
        public void ResetPrevVector()
        {
            prevVectorTrans.rotation = Quaternion.identity;

            prevVectorTrans = vectorTrans;
        }
    }
}
