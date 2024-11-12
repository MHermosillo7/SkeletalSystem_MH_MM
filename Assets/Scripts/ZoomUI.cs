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
            //EnableButton(false);

            user = FindObjectOfType<User>();
        }
        // Update is called once per frame
        void Update()
        {

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
        public void EnableButton(bool enable)
        {
            button.interactable = enable;
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
            button.interactable = false;
        }
    }
}