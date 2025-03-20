using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BodySystem
{
    public class CreditsScroll : MonoBehaviour
    {
        RectTransform objRect;

        Vector3 startPosition;

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

        public enum Platform
        {
            Computer,
            Android,
            IOS
        }

        public Platform platform = Platform.Computer;

        // Start is called before the first frame update
        void Awake()
        {
            objRect = GetComponent<RectTransform>();

            topPanel = GameObject.Find("TopPanel").GetComponent<RectTransform>();

            startPosition = objRect.position;

            GetPlatform(platform);
        }

        // Update is called once per frame
        void Update()
        {
            switch (platform)
            {
                case Platform.Computer:

                    if (Input.GetAxis("Scroll Wheel") != 0)
                    {
                        TryScroll();
                    }
                    if (Input.GetMouseButton(2))
                    {
                        StartCoroutine(AutoScroll());
                    }
                    break;
                case Platform.Android:
                    if (Input.touchCount == 0)
                    {
                        return;
                    }

                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        StartCoroutine(AutoScroll_Phone());
                    }
                    else if(Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        StopCoroutine(AutoScroll_Phone());
                    }
                    break;
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

                yield return null;
            }
        }
        IEnumerator AutoScroll_Phone()
        {
            float inputForce = Input.acceleration.y * mouseSpeed;

            while (Input.GetTouch(0).phase != TouchPhase.Ended 
                || Input.GetTouch(0).phase != TouchPhase.Canceled)
            {
                GetCoordinates();
                CheckPosition();

                ScrollTypeTouch(inputForce);

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
            if (Input.GetAxis("Scroll Wheel") > 0 && canMoveDown)
            {
                //Don't ask, genuinely, I don't know how this works or makes sense, but it does.
                //Found out GetInput result always returns a negative value, so I multiplied it
                //by -1 to make it positive(?) and get movement to feel natural
                objRect.Translate(Vector2.up * -GetInput("Scroll Wheel", scrollSpeed));
            }
            //Else scrolling down
            if (Input.GetAxis("Scroll Wheel") < 0 && canMoveUp)
            {
                objRect.Translate(Vector2.down * GetInput("Scroll Wheel", scrollSpeed));
            }
        }
        void ScrollTypeMouse(float inputForce)
        {
            //If scrolling up
            if (inputForce > 0 && canMoveDown)
            {
                objRect.Translate(Vector2.up *
                    -inputForce * Time.deltaTime);
            }
            //Else scrolling down
            if (inputForce < 0 && canMoveUp)
            {
                objRect.Translate(Vector2.down *
                    inputForce * Time.deltaTime);
            }
        }
        void ScrollTypeTouch(float inputForce)
        {

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
                    inputForce = inputForce / 4 + newInputForce * 2;
                }
                //If user is autoscrolling up but they move mouse down
                if (newInputForce < inputForce && inputForce > 0)
                {
                    inputForce = inputForce / 4 - newInputForce * 2;
                }

                if (newInputForce > inputForce)
                {
                    inputForce = 0;
                }

                //If user was scrolling down but newForce changed input so
                //It will now scroll down, reset input
                if (originalInputForce > 0 && inputForce < 0)
                {
                    inputForce = 0;
                }
                //If user was scrolling up but newForce changed input so
                //It will now scroll down, reset input
                if (originalInputForce < 0 && inputForce > 0)
                {
                    inputForce = 0;
                }
                if (inputForce <= originalInputForce / 10)
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
            if (objMaxY <= topPanelMinY)
            {
                canMoveDown = false;
            }
            else
            {
                canMoveDown = true;
            }
            if (objMinY >= 0)
            {
                canMoveUp = false;
            }
            else
            {
                canMoveUp = true;
            }
        }

        //Ensures the credits always start from the top every time they are re-activated
        //ie. If the user moves the credits for programming, but then switch to research's,
        //    when they switch back to programming sources, they will start from the top again
        void OnDisable()
        {
            if(objRect.position != startPosition)
            {
                objRect.position = startPosition;
            }
        }

        void GetPlatform(Platform platform)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                    platform = Platform.Computer;
                    break;
                case RuntimePlatform.WindowsEditor:
                    platform = Platform.Computer;
                    break;
                case RuntimePlatform.OSXPlayer:
                    platform = Platform.Computer;
                    break;
                case RuntimePlatform.OSXEditor:
                    platform = Platform.Computer;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    platform = Platform.IOS;
                    break;
                case RuntimePlatform.Android:
                    platform = Platform.Android;
                    break;
            }
        }
    }
}
