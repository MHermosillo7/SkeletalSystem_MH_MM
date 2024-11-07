using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using UnityEditor;

namespace BodySystem
{
    public class CameraMovement : MonoBehaviour
    {
        float horizontalInput;
        float verticalInput;

        int rotateSpeed = 750;
        int slideSpeed = 100;
        float zoomSpeed = 1.5f;

        float rotProgress = 0;
        float posProgress = 0;
        float endTime = 1;
        [Range(0, 1)] [SerializeField] float rate = .5f;

        GameObject vector;
        public Transform vectorTrans;
        Transform prevVectorTrans;

        CameraStatus camStatus;

        // Start is called before the first frame update
        void Start()
        {
            vector = GameObject.FindWithTag("Origin");
            vectorTrans = vector.transform;
            prevVectorTrans = vectorTrans;

            camStatus = FindObjectOfType<CameraStatus>();
        }

        // Update is called once per frame
        void Update()
        {
            if (camStatus.cameraCanMove)
            {
                if (Input.GetMouseButton(2))
                {
                    horizontalInput = GetInput("Mouse X", rotateSpeed);
                    verticalInput = GetInput("Mouse Y", rotateSpeed);

                    Rotate();
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
                    camStatus.UpdateCamStatus(true);
                }
            }
        }

        //Rotate camera around object in all axis (almost)
        void Rotate()
        {
            //Rotate vertically
            vectorTrans.Rotate(Vector3.right, verticalInput * -1);

            //Rotate horizontally
            vectorTrans.Rotate(Vector3.up, horizontalInput, Space.World);
        }

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

        public void CenterCamera(Transform newVector)
        {
            if (camStatus.cameraCanMove)
            {
                //Locks user driven camera movement
                camStatus.UpdateCamStatus(false);

                //Store vectorHit's transform as camera script's vector transform
                vectorTrans = newVector;

                //Make new vector the camera's parent
                transform.parent = newVector;

                ResetPrevVector();

                StartCoroutine(CenterCameraPos());
                StartCoroutine(CenterCameraRot());
            }
        }
        /* Note to self: Consider using Lerp or SmoothDamp 
           because movement becomes too harsh even when at low movement rate*/
        IEnumerator CenterCameraPos()
        {
            while (posProgress < endTime)
            {
                transform.position =
                    Vector3.Slerp(transform.position, 
                        new Vector3(vectorTrans.position.x, vectorTrans.position.y, -8f),
                        posProgress);

                posProgress += Time.deltaTime * rate;

                yield return null;
            }
            transform.position = new Vector3(vectorTrans.position.x, vectorTrans.position.y, -8f);

            posProgress = 0;
        }

        IEnumerator CenterCameraRot()
        {
            while (rotProgress < endTime)
            {
                transform.rotation = 
                    Quaternion.Slerp(transform.rotation, Quaternion.identity, rotProgress);

                rotProgress += Time.deltaTime * rate;

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
