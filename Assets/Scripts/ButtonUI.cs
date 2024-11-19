using UnityEngine;
using UnityEngine.EventSystems;

namespace BodySystem
{
    public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject textPanel;
        
        private void Awake()
        {
            textPanel.SetActive(false);
        }


        /* Uses On Pointer Data in order to detect the mouse because 
         * this is designed to be used on UI elements only without requiring colliders*/

        //Activate the text panel when mouse is over object
        public void OnPointerEnter(PointerEventData pointerData)
        {
            textPanel.SetActive(true);
        }

        //Deactivate text panel when mouse no longer over object
        public void OnPointerExit(PointerEventData pointerData)
        {
            textPanel.SetActive(false);
        }
    }
}