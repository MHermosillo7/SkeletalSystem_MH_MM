using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenshotButton : MonoBehaviour, IPointerDownHandler
{
    Button button;
    Screenshot screenshot;
    void Awake()
    {
        button = GetComponent<Button>();
        screenshot = FindObjectOfType<Screenshot>();
    }

    public void OnPointerDown(PointerEventData pointerData)
    {
        if (button.interactable)
        {
            button.interactable = false;
            screenshot.TakeScreenShot();
            screenshot.TriggerAnimation();
        }
    }
}
