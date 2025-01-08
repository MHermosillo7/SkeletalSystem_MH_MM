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

/*

    
    // Cycles through all children of a game object and
    // adds their transforms into layers depending on
    // hierarchal depth compared to primary object passed

    // I add one to layersActive before getting children
    // because when adding them to the list, I change their layer level
    // in ZoomControl to equal the amount of layers active.
    // It serves as an index of on what layer the object is.

    //Currently has reached the level of a forbidden, pc chrashing, while loop

    //Somewhere along the hierarchy,
    //if the object with the most children is not at the bottom, it crashes
    void ManageChildren(Transform obj)
    {
        GetComponent<ZoomControl>().layerIndex = layerIndex;

            layerIndex = 1;
            layerOne.AddRange(TryGetChildren(obj));

            foreach (Transform child in layerOne)
            {
                    layerIndex = 2;
                    layerTwo.AddRange(TryGetChildren(child));

                    foreach (Transform grandchild in layerTwo)
                    {
                            layerIndex = 3;
                            layerThree.AddRange(TryGetChildren(grandchild));

                            foreach (Transform greatGrandchild in layerThree)
                            {
                                    layerIndex = 4;
                                    layerFour.AddRange(TryGetChildren(greatGrandchild));

                                    foreach (Transform obj4 in layerFour)
                                    {
                                            layerIndex = 5;
                                            layerFive.AddRange(TryGetChildren(obj4));

                                            foreach (Transform obj5 in layerFive)
                                            {
                                                    layerIndex = 6;
                                                    layerSix.AddRange(TryGetChildren(obj5));

                                                    foreach (Transform obj6 in layerSix)
                                                    {
                                                            layerIndex = 7;
                                                            layerSeven.AddRange(TryGetChildren(obj6));

                                                            foreach (Transform obj7 in layerSeven)
                                                            {
                                                                    layerIndex = 8;
                                                                    layerEight.AddRange(TryGetChildren(obj7));

                                                                    foreach (Transform obj8 in layerEight)
                                                                    {
                                                                            layerIndex = 9;
                                                                            layerNine.AddRange(TryGetChildren(obj8));

                                                                            foreach (Transform obj9 in layerNine)
                                                                            {
                                                                                    layerIndex = 10;
                                                                                    layerTen.AddRange(TryGetChildren(obj9));
                                                                                
                                                                            }
                                                                    }
                                                            }
                                                    }
                                            }
                                    }
                            }
                    }
            }
    }
 */