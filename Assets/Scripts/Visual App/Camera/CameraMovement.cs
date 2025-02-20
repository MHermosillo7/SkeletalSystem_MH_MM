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

        // Camera speed to rotate, slide, and zoom in
        [SerializeField] int rotateSpeed = 750;
        [SerializeField] int slideSpeed = 100;
        [SerializeField] float zoomSpeed = 1.5f;

        // Rotation Control
        Vector3 eulerAngles;
        int clampAngle = 50;        //At what angle the camera blocks any further rotation
        bool canRotateDown = true;  //Controls down rotations
        bool canRotateUp = true;    //Controls up rotations

        // Coroutine Process References
        /*  Used to force stop executing coroutines to avoid visual errors
            due to same logic or movements happening at the same time
            Also speeds up user interaction and ease of use due to not having
            to wait until a process is finished*/
        Coroutine vecRotationCoroutine;
        Coroutine camRotationCoroutine;
        Coroutine positionCoroutine;

        // Tracks Coroutine Progress

        /*  Used to control whether user can rotate or zoom to avoid
            interference with coroutines or logic happening at the same time*/
        float vecRotProgress = 0;
        float camRotProgress = 0;
        float posProgress = 0;

        float endTime = 1;

        //Rate at which processes happen, such as camera centering
        /*  Currently its usefulness is shady due to not observing change in movement speed*/
       [Range(0, 1)] [SerializeField] float rate = .5f;

        public GameObject originVector;         //Camera's original vector, unnafected by any models
        [SerializeField] Transform vectorTrans; //Reference to camera position and rotation
        Transform prevVectorTrans;              //Reference to previous selected object's vector/pivot

        CameraStatus camStatus;

        // Awake is called at assembly
        void Awake()
        {
            originVector = GameObject.FindWithTag("Origin");
            vectorTrans = originVector.transform;
            prevVectorTrans = vectorTrans;

            camStatus = FindObjectOfType<CameraStatus>();
        }

        // Update is called once per frame
        void Update()
        {
            /*  Checks if the camera can move, and if so, allows user to move camera
                Avoids interferences with camera centering and other processes*/
            if (camStatus.cameraCanMove)
            {
                /*  While holding middle mouse
                    Get horizontal and vertical input
                    To rotate in all directions(spherical shape)*/
                if (Input.GetMouseButton(2))
                {
                    horizontalInput = GetInput("Mouse X", rotateSpeed);
                    verticalInput = GetInput("Mouse Y", rotateSpeed);

                    RotateB();
                }

                // Zooms forward or backward using scroll wheel input
                Zoom();

                // While holding right click
                // Slides vertically and horizontally using mouse movement
                if (Input.GetMouseButton(1))
                {
                    SlideB();
                }
            }
            else if(camRotProgress == 0 && posProgress == 0 && vecRotProgress == 0)
            {
                camStatus.UpdateCamStatus(true);
            }
        }

        //Rotate Functions

        /*  Cannot Have RotationB paired with SlideA
            because the camera stops rotating around object
            due to modified camera position interfering with rotation
            SlideA only with RotationA */

        //Rotate camera around object horizontally in a cylindrical shape
        void RotateA()
        {
            //Rotate horizontally by using vertical and horizontal mouse input
            vectorTrans.Rotate(Vector3.up, horizontalInput + verticalInput, Space.World);
        }

        //Rotates Camera around object omnidirectionally in a spherical shape
        void RotateB()
        {
            //Checks whether camera has reached maximum movement degrees
            //and stops further rotation if so
            CheckRotation();

            //Checks if can rotate vertically (up/down) and applies force
            RotateVertically();

            //Rotate horizontally (left/right)
            vectorTrans.Rotate(Vector3.up, horizontalInput, Space.World);
        }

        //Checkes whether camera has reached maximum
        //rotation degrees and resets position if so

        /*  The closer the camera moves to the top or bottom of an object alongside axis y
            The more unintuitive the camera movement becomes until a camera reset is needed
            This function avoids getting to that point by setting rotation limits*/
        void CheckRotation()
        {
            if (vectorTrans.rotation.eulerAngles.x > clampAngle 
                && vectorTrans.rotation.eulerAngles.x !< 180)
            {
                Clamp(clampAngle);
                
                canRotateUp = false;
            }
            else if (vectorTrans.rotation.eulerAngles.x < 310 
                && vectorTrans.rotation.eulerAngles.x !> 180)
            {
                Clamp(-clampAngle);

                canRotateDown = false;
            }
        }

        //Resets/Blocks/Clamps camera's rotation when they reach the rotation limit
        void Clamp(int xAngle)
        {
            //Gets camera's rotation angles
            eulerAngles = vectorTrans.rotation.eulerAngles;

            //Ceates new quaternion to store new angle rotation reference
            Quaternion q = new Quaternion();
            q.eulerAngles = new Vector3(xAngle, eulerAngles.y, eulerAngles.z);

            //Overrides camera's rotation to block further movement
            vectorTrans.rotation = q;
        }

        //Rotates camera up/down

        /*  Has checks before applying force to avoid trying to move when camera has
            reached the rotation limit, avoiding jittery movement otherwise cause by
            one function trying to block movement and the other applying it*/
        void RotateVertically()
        {
            if (verticalInput < 0 && canRotateUp)
            {
                vectorTrans.Rotate(Vector3.right, verticalInput * -1);
                canRotateDown = true;
            }
            else if (verticalInput > 0 && canRotateDown)
            {
                vectorTrans.Rotate(Vector3.right, verticalInput * -1);
                canRotateUp = true;
            }
        }

        //Moves camera forward or backward using scroll wheel input
        void Zoom()
        {
            //If scroll wheel is moved up, move object forward
            if (Input.GetAxis("Scroll Wheel") > 0)
            {
                //Direction to move camera in (forward)
                Ray ray = new Ray(transform.position, transform.forward);

                //Avoids moving camera through and object
                if (Physics.Raycast(ray, 2f))
                {
                    return;
                }

                else
                {
                    transform.Translate(Vector3.forward * zoomSpeed);
                }
            }
            //Or if scroll wheel is moved down, scroll backward/away from object
            else if (Input.GetAxis("Scroll Wheel") < 0)
            {
                transform.Translate(Vector3.forward * -zoomSpeed);
            }
        }

        // Sliding Functions

        /* Use mouse movement to move camera horizontally/vertically */

        //Move camera vertically only

        /*  In order to feel like a true sliding movement and be more intuitive,
            the forces applied are negative, substracting each other and resulting
            into a lower, negative number that is then used to move camera

            Note: Currently unused but could serve for android/tablet support*/
        void SlideA()
        {
            transform.Translate
                (Vector3.up * (-GetInput("Mouse Y", slideSpeed) - GetInput("Mouse X", slideSpeed)));
        }

        // Move Camera horizontally & vertically
        void SlideB()
        {
            transform.Translate(Vector3.up * -GetInput("Mouse Y", slideSpeed));
            transform.Translate(Vector3.right * -GetInput("Mouse X", slideSpeed));
        }

        //Gets Input of axis reference passed multiplide by speed in a constant timeframe

        /*  Used to avoid code repetition due to how many times 
            input references and force is needed*/
        float GetInput(string axis, int speed)
        {
            float input = Input.GetAxis(axis) * speed * Time.deltaTime;
            return input;
        }

        //Center camera to new vector/pivot reference passed
        public void CenterCamera(Transform newVector)
        {
            //Locks user driven camera movement to avoid interference
            camStatus.UpdateCamStatus(false);

            //Store referent of current vector to properly reset it
            prevVectorTrans = vectorTrans;

            //Store vectorHit's transform as camera script's vector transform
            vectorTrans = newVector;

            //Make new vector the camera's parent
            transform.parent = newVector;

            //Resets previous vector to return it to original state and avoid
            //accumulating of transformations and unexpected movements next time its
            //object is selected
            ResetPrevVector();

            //Checks if coroutines are currently running, stops them, and runs a new instance
            CheckCoroutine(positionCoroutine, CenterCameraPos());
            CheckCoroutine(camRotationCoroutine, CenterCameraRot());
        }

        //Used when zooming out, since camera is reset and
        //centers to object but vector remains the same
        public void CenterVector()
        {
            //Locks user driven camera movement to avoid interference
            camStatus.UpdateCamStatus(false);

            //Checks if coroutines are currently running, stops them, and runs a new instance
            CheckCoroutine(vecRotationCoroutine, CenterVectorRot());
        }

        //Centers camera to current vector

        /*  Used to avoid needing a reference to current vector*/
        public void CenterCameraToCurrentVector()
        {
            //Calls Center Camera with current vector transform reference
            CenterCamera(vectorTrans);
        }

        /*  Checks coroutine (process) reference, and if it has been 
            started before or is currently running, stops it.

            Always starts the Enumator (process) passed
            and stores a reference to it

            This avoid logic overlap when user clicks different
            objects too fast and camera has to reset position many consecutive times
            or is in the middle of resetting when function is called again*/
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

        //Center camera position to current object using vector reference

        /*  Note to self: Consider using Lerp or SmoothDamp 
            because movement becomes too harsh even when at low movement rate*/
        IEnumerator CenterCameraPos()
        {
            while (posProgress < endTime)
            {
                transform.position =
                    Vector3.Slerp(transform.position, 
                        new Vector3(vectorTrans.position.x, vectorTrans.position.y, -35.5f),
                        posProgress);

                posProgress += Time.deltaTime * rate;

                yield return null;
            }
            transform.position = new Vector3(vectorTrans.position.x, vectorTrans.position.y, -35.5f);

            posProgress = 0;
        }

        //Resets/Centers camera position
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
        //Resets/Centers current vector's rotation
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

        //Resets previous vector's rotation to 0's

        /*  Avoid interference or unexpected movements when vector 
            is used again due to accumulation of transformations*/
        public void ResetPrevVector()
        {
            prevVectorTrans.rotation = Quaternion.identity;

            prevVectorTrans = vectorTrans;
        }
    }
}
