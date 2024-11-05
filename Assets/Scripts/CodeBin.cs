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