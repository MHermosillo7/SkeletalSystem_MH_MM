// Camera:

/*  GameObject target;
    Vector3 mousePos;
    Vector3 allOne = new Vector3(1, 1, 1);*/

/*void MoveCamera()
{
    Vector3 mouseDir = mousePos - Input.mousePosition;

    float angle = Vector3.Angle(mouseDir, mousePos) * rotateSpeed;

    transform.RotateAround(target.transform.position,
        allOne, angle);
}

void MoveCamera()
{
    transform.RotateAround
        (target.transform.position, Vector3.up, horizontalInput);
    transform.RotateAround
        (target.transform.position, Vector3.right, verticalInput);
}*/

/*void MoveCamera()
{
    transform.RotateAround(parentTrans.position, transform.right,
        -verticalInput);
    transform.RotateAround(parentTrans.position, transform.up,
        horizontalInput);
}*/

/*using UnityEngine;

Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
RaycastHit hit;
if (Physics.Raycast(ray, out hit))
{
    print(hit.transform.gameObject.name);
}*/

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
        transform.Translate(ray.direction * -1 + Vector3.forward * -zoomSpeed);
    }
}*/



// Camera Rotation (Functional Code)

/*using UnityEngine;

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

// Update is called once per frame
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