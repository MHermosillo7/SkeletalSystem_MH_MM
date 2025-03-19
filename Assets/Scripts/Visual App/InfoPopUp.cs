using System;
using System.Collections;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class InfoPopUp : MonoBehaviour
{
    RectTransform objRect;

    Vector3[] objectCorners = new Vector3[4];

    float objMaxX;
    float objMinX;
    float objMaxY;
    float objMinY;

    float movementSpeed = 250;

    [SerializeField] bool moveHorizontally = false;
    [SerializeField] bool moveVertically = false;

    float horizontalForce;
    float verticalForce;

    Coroutine checkPopUp;

    [SerializeField] Vector2 resetPosition = new Vector2(740, 0);
    [SerializeField] bool reset = true;

    // Start is called before the first frame update
    void Awake()
    {
        objRect = GetComponent<RectTransform>();
    }
    private void OnDisable()
    {
        ResetPosition();
    }

    public void RunPopUpCheck()
    {
        if(checkPopUp != null)
        {
            StopCoroutine(checkPopUp);
        }

        ResetPosition();

        StartCoroutine(CheckPopUp());
    }
    void ResetPosition()
    {
        if (reset)
        {
            objRect.transform.localPosition = resetPosition;
        }
    }
    IEnumerator CheckPopUp()
    {
        // Needed for first call of coroutine
        // Without it, the function runs while the image is upscaling (or smthng like that)
        // And so the image does not move even if it is outside of canvas
        // However, if waiting until end of frame, the function always runs correctly
        yield return new WaitForEndOfFrame();

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

        //Object is too right
        if (objMaxX > Screen.width)
        {
            horizontalForce = -movementSpeed;
        }

        //Object is too left
        else if (objMinX < 0)
        {
            horizontalForce = movementSpeed;
        }
        
        //Else object is okay horizontally
        else
        {
            moveHorizontally = false;
            horizontalForce = 0;
        }

        moveVertically = true;

        //Object is too up
        if (objMaxY > Screen.height)
        {
            verticalForce = -movementSpeed;
        }

        //Object is too down
        else if (objMinY < 0)
        {
            verticalForce = movementSpeed;
        }

        //Else object is okay vertically
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
