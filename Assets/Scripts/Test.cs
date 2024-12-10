using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    Transform trans;
    Vector3 eulerAngles;

    int clampAngle = 50;

    bool canRotateDown = true;
    bool canRotateUp = true;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    /*// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Rotate();
        }
    }

    void Rotate()
    {
        CheckRotation();

        RotateVertically()//Rotate vertically

        //Rotate horizontally
        vectorTrans.Rotate(Vector3.up, horizontalInput, Space.World);
    }
    void CheckRotation()
    {
        if (trans.rotation.eulerAngles.x > clampAngle)
        {
            Clamp(clampAngle);

            canRotateUp = false;
        }
        else if (trans.rotation.eulerAngles.x < -clampAngle)
        {
            Clamp(-clampAngle);

            canRotateDown = false;
        }
    }
    void RotateVertically()
    {
        if (verticalInput > 0 && canRotateUp)
        {
            vectorTrans.Rotate(Vector3.right, verticalInput * -1);
        }
        else if (verticalInput < 0 && canRotateDown)
        {
            vectorTrans.Rotate(Vector3.right, verticalInput * -1);
        }
    }
    void Clamp(int xAngle)
    {
        eulerAngles = trans.rotation.eulerAngles;

        Quaternion q = new Quaternion();
        q.eulerAngles = new Vector3(xAngle, eulerAngles.y, eulerAngles.z);

        trans.rotation = q;
    }*/
}
