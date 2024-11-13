using UnityEngine;
using UnityEngine.UI;

namespace BodySystem
{
    public class ZoomUI : MonoBehaviour
    {
        [SerializeField] GameObject button;
        [SerializeField] Image plus;
        [SerializeField] Image minus;

        bool plusIsActive = true;
        bool isZoomed = false;

        User user;

        void Start()
        {
            //EnableButton(false);

            user = FindObjectOfType<User>();
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
            button.SetActive(true);

            plusIsActive = false;
            ToggleIcon();
        }
        public void HideUI()
        {
            button.SetActive(false);
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

        public void ResetZoom()
        {
            if (!plusIsActive)
            {
                ToggleIcon();
            }

            button.SetActive(false);
            isZoomed = false;
        }
    }
}