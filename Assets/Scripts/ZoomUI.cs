using UnityEngine;
using UnityEngine.UI;

namespace BodySystem
{
    public class ZoomUI : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] Image plus;
        [SerializeField] Image minus;

        bool plusIsActive = true;
        bool isZoomed = false;

        User user;

        void Start()
        {
            user = FindObjectOfType<User>();

            minus.enabled = false;
            button.interactable = false;
        }
        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                ToggleIcon();
                print("Hello");
            }
        }
        void ToggleIcon()
        {
            plus.enabled = !plus.isActiveAndEnabled;
            minus.enabled = !minus.isActiveAndEnabled;
        }

        public void ShowUI()
        {
            if (!button.IsInteractable())
            {
                button.interactable = (true);
            }

            if (!plus.isActiveAndEnabled)
            {
                minus.enabled = false;
                plus.enabled = true;
            }
            plusIsActive = false;
            isZoomed = false;
        }

        public void Zoom()
        {
            if (!isZoomed)
            {
                user.ZoomIn();
            }
            else
            {
                user.ZoomOut();
                
            }
            isZoomed = !isZoomed;

            plus.enabled = !plus.isActiveAndEnabled;
            minus.enabled = !minus.isActiveAndEnabled;
            
        }
        public bool IsUIActive()
        {
            return button.IsInteractable();
        }

    }
}