using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class InfoPopUp : MonoBehaviour
{
    RectTransform objRect;

    Vector3[] objectCorners = new Vector3[4];

    float objMaxX;
    float objMinX;
    float objMaxY;
    float objMinY;

    float movementSpeed = 200;

    bool moveHorizontally = false;
    bool moveVertically = false;

    float horizontalForce;
    float verticalForce;

    // Start is called before the first frame update
    void Awake()
    {
        objRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Hello");
            StartCoroutine(CheckPopUp());
        }
    }


    IEnumerator CheckPopUp()
    {
        GetCoordinates();
        CheckPosition();

        while(moveHorizontally == true || moveVertically == true)
        {
            objRect.Translate(Vector2.right * horizontalForce * Time.deltaTime);
            objRect.Translate(Vector2.up * verticalForce * Time.deltaTime);

            GetCoordinates();
            CheckPosition();

            yield return null;
        }
    }
    void GetCoordinates()
    {
        objRect.GetWorldCorners(objectCorners);

        float[] xCoordinates = objectCorners.Select(c => c.x).ToArray();
        float[] yCoordinates = objectCorners.Select(c => c.y).ToArray();

        objMaxX = Mathf.Max(xCoordinates);
        objMinX = Mathf.Min(xCoordinates);

        objMaxY = Mathf.Max(yCoordinates);
        objMinY = Mathf.Min(yCoordinates);
    }

    void CheckPosition()
    {
        moveHorizontally = true;

        if(objMaxX > Screen.width)
        {
            //Object is too right
            horizontalForce = -movementSpeed;
        }
        else if(objMinX < 0)
        {
            //Object is too left
            horizontalForce = movementSpeed;
        }
        else
        {
            moveHorizontally = false;
            horizontalForce = 0;
        }

        moveVertically = true;

        if(objMaxY > Screen.height)
        {
            //Object is too up
            verticalForce = -movementSpeed;
        }
        else if(objMinY < 0)
        {
            //Object is too down
            verticalForce = movementSpeed;
        }
        else
        {
            moveVertically = false;
            verticalForce = 0;
        }
    }


    //This was original way to get canvas' corners and know if pop up went over those limits
    //However, there was the realization afterward that all minimum values are always 0
    //And that all maximum values can be accessed through Screen height and width directly
    //Thus, these operations were considered unnecessary and replaced by available reference values

    /*
    transform.root.GetComponent<RectTransform>().GetWorldCorners(canvasCorners);

    float[] xCoordinates = canvasCorners.Select(c => c.x).ToArray();
    float[] yCoordinates = canvasCorners.Select(c => c.y).ToArray();

    canvasMaxX = Mathf.Max(xCoordinates);
        canvasMinX = Mathf.Min(xCoordinates);

        canvasMaxY = Mathf.Max(yCoordinates);

        Debug.Log(canvasMinX);
        Debug.Log(Screen.width);*/
}
