using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace BodySystem
{
    public class CameraMovement : MonoBehaviour
    {
        float horizontalInput;
        float verticalInput;
        Vector3 pos;

        [SerializeField] int rotateSpeed = 750;
        [SerializeField] int slideSpeed = 100;
        [SerializeField] float zoomSpeed = 1.5f;

        Coroutine vecRotationCoroutine;
        Coroutine camRotationCoroutine;
        Coroutine positionCoroutine;

        float vecRotProgress = 0;
        float camRotProgress = 0;
        float posProgress = 0;

        float endTime = 1;
        [Range(0, 1)] [SerializeField] float rate = .5f;

        GameObject vector;
        [SerializeField] Transform vectorTrans;
        Transform prevVectorTrans;

        CameraStatus camStatus;
        [SerializeField] LayerMask ignoreLayer;

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
                StopAllCoroutines();

                if (Input.GetMouseButton(2))
                {
                    horizontalInput = GetInput("Mouse X", rotateSpeed);
                    verticalInput = GetInput("Mouse Y", rotateSpeed);

                    Rotate();
                }

                Zoom();

                //If holding right click
                if (Input.GetMouseButton(1))
                {
                    Slide();
                }
            }
            else if(camRotProgress == 0 && posProgress == 0 && vecRotProgress == 0)
            {
                camStatus.UpdateCamStatus(true);
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

        void Zoom()
        {
            if (Input.GetAxis("Scroll Wheel") > 0)
            {
                Ray ray = new Ray(transform.position, transform.forward);

                if (Physics.Raycast(ray, 2f))
                {
                    return;
                }

                else
                {
                    transform.Translate(Vector3.forward * zoomSpeed);
                }
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
            if (/*camStatus.cameraCanMove*/1 == 1)
            {
                //Locks user driven camera movement
                //camStatus.UpdateCamStatus(false);

                //Store vectorHit's transform as camera script's vector transform
                vectorTrans = newVector;

                //Make new vector the camera's parent
                transform.parent = newVector;

                ResetPrevVector();

                CheckCoroutine(positionCoroutine, CenterCameraPos());
                CheckCoroutine(camRotationCoroutine, CenterCameraRot());

            }
        }

        //Used when zooming out, since camera is reset and
        //centers object but vector remains the same
        public void CenterVector()
        {
            //Locks user driven camera movement
            //camStatus.UpdateCamStatus(false);

            CheckCoroutine(vecRotationCoroutine, CenterVectorRot());
        }

        /* Note to self: Consider using Lerp or SmoothDamp 
           because movement becomes too harsh even when at low movement rate*/
        IEnumerator CenterCameraPos()
        {
            while (posProgress < endTime)
            {
                transform.position =
                    Vector3.Slerp(transform.position, 
                        new Vector3(vectorTrans.position.x, vectorTrans.position.y, -30f),
                        posProgress);

                posProgress += Time.deltaTime * rate;

                yield return null;
            }
            transform.position = new Vector3(vectorTrans.position.x, vectorTrans.position.y, -30f);

            posProgress = 0;
        }

        IEnumerator CenterCameraRot()
        {
            while (camRotProgress < endTime)
            {
                transform.rotation = 
                    Quaternion.Slerp(transform.rotation, Quaternion.identity, camRotProgress);

                camRotProgress += Time.deltaTime * rate;

                yield return null;
            }
            camRotProgress = 0;
        }

        IEnumerator CenterVectorRot()
        {
            while (vecRotProgress < endTime)
            {
                vectorTrans.rotation =
                    Quaternion.Slerp(vectorTrans.rotation, Quaternion.identity, vecRotProgress);

                vecRotProgress += Time.deltaTime * rate;

                yield return null;
            }
            vecRotProgress = 0;
        }
        public void ResetPrevVector()
        {
            prevVectorTrans.rotation = Quaternion.identity;

            prevVectorTrans = vectorTrans;
        }

        void CheckCoroutine(Coroutine cor, IEnumerator ie)
        {
            switch (cor)
            {
                case null:
                    cor = StartCoroutine(ie);
                    break;

                default:
                    StopCoroutine(cor);

                    cor = StartCoroutine(ie);
                    break;
            }
        }
    }
}
