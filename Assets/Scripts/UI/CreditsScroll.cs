using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    RectTransform objRect;

    Vector3[] objectCorners = new Vector3[4];

    float objMaxY;
    float objMinY;


    [SerializeField] RectTransform topPanel;
    Vector3[] topPanelCorners = new Vector3[4];
    float topPanelMinY;


    int scrollSpeed = 250;
    int mouseSpeed = 40;

    bool canMoveUp = false;
    bool canMoveDown = false;


    // Start is called before the first frame update
    void Awake()
    {
        objRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Scroll Wheel") != 0)
        {
            TryScroll();
        }
        if (Input.GetMouseButton(2))
        {
            StartCoroutine(AutoScroll());
        }
    }
    IEnumerator AutoScroll()
    {
        float inputForce = GetInput("Mouse Y", mouseSpeed);
        float newInputForce = 0;

        while (Input.GetMouseButton(2))
        {
            GetCoordinates();
            CheckPosition();

            UpdateMouseInput(inputForce, newInputForce);

            ScrollTypeMouse(inputForce);

            if(inputForce != 0)
            {
                print(inputForce);
            }
            yield return null;
        }
    }
    void TryScroll()
    {
        GetCoordinates();
        CheckPosition();

        ScrollTypeWheel();
    }
    void ScrollTypeWheel()
    {
        //If scrolling up
        if (Input.GetAxis("Scroll Wheel") > 0 && canMoveUp)
        {
            //Don't ask, genuinely, I don't know how this works or makes sense, but it does.
            //Found out GetInput result always returns a negative value, so I multiplied it
            //by -1 to make it positive(?) and get movement to feel natural
            objRect.Translate(Vector2.up * -GetInput("Scroll Wheel", scrollSpeed));
        }
        //Else scrolling down
        if (Input.GetAxis("Scroll Wheel") < 0 && canMoveDown)
        {
            objRect.Translate(Vector2.down * GetInput("Scroll Wheel", scrollSpeed));
        }
    }
    void ScrollTypeMouse(float inputForce)
    {
        //If scrolling up
        if (inputForce > 0 && canMoveUp)
        {
            objRect.Translate(Vector2.up * 
                -inputForce * Time.deltaTime);
        }
        //Else scrolling down
        if (inputForce < 0 && canMoveDown)
        {
            objRect.Translate(Vector2.down * 
                inputForce * Time.deltaTime);
        }
    }
    void UpdateMouseInput(float inputForce, float newInputForce)
    {
        //If user moves mouse while auto scrolling
        if (Input.GetAxisRaw("Mouse Y") != 0)
        {
            float originalInputForce = inputForce;

            newInputForce = GetInput("Mouse Y", mouseSpeed);
            float rawAxisInput = Input.GetAxisRaw("Scroll Wheel");

            if (inputForce == 0 && rawAxisInput < .3 && rawAxisInput > -.3)
            {
                return;
            }
            else
            {
                inputForce = newInputForce;
            }
            //If user is autoscrolling down but they move mouse up
            if (newInputForce < inputForce && inputForce < 0)
            {
                inputForce = inputForce/4 + newInputForce*2;
            }
            //If user is autoscrolling up but they move mouse down
            if (newInputForce < inputForce && inputForce > 0)
            {
                inputForce = inputForce/4 - newInputForce*2;
            }

            if (newInputForce > inputForce)
            {
                inputForce = 0;
            }

            //If user was scrolling down but newForce changed input so
            //It will now scroll down, reset input
            if(originalInputForce > 0 && inputForce < 0)
            {
                inputForce = 0;
            }
            //If user was scrolling up but newForce changed input so
            //It will now scroll down, reset input
            if (originalInputForce < 0 && inputForce > 0)
            {
                inputForce = 0;
            }
            if(inputForce <= originalInputForce / 10)
            {
                inputForce = 0;
            }
        }
    }
    float GetInput(string axisName, int speed)
    {
        float input = Input.GetAxisRaw(axisName) * speed;
        return input;
    }

    void GetCoordinates()
    {
        objRect.GetWorldCorners(objectCorners);

        float[] yCoordinates = objectCorners.Select(c => c.y).ToArray();

        objMaxY = Mathf.Max(yCoordinates);
        objMinY = Mathf.Min(yCoordinates);

        //Due to the nature of how the credits work, they should never obscure the title
        //Thus, a top panel was put in place to hide the credits when they scroll up
        //However, due to it, I can no longer use the Screen.Height reference, since the
        //credits would remain hidden by the top panel in that case
        // That is why I get the min Y coordinate of the panel to use instead of screen height
        topPanel.GetWorldCorners(topPanelCorners);

        float[] panelYCoordinates = topPanelCorners.Select(c => c.y).ToArray();

        topPanelMinY = Mathf.Min(panelYCoordinates);
    }

    void CheckPosition()
    {
        if(objMaxY <= topPanelMinY)
        {
            canMoveDown = false;
        }
        else
        {
            canMoveDown = true;
        }
        if(objMinY >= 0)
        {
            canMoveUp = false;
        }
        else
        {
            canMoveUp = true;
        }
    }
}
