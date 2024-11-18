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
        void ToggleIcon()
        {
            if (plusIsActive)
            {
                plus.enabled = false;
                minus.enabled = true;
            }
            else
            {
                plus.enabled = true;
                minus.enabled = false;
            }

            plusIsActive = !plusIsActive;
        }

        public void ShowUI()
        {
            if (!button.IsInteractable())
            {
                button.interactable = (true);
            }

            plusIsActive = false;
            ToggleIcon();
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

            ToggleIcon();
        }

        public bool IsUIActive()
        {
            return button.IsInteractable();
        }
    }
}