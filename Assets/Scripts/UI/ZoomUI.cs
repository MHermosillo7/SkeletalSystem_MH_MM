using UnityEngine;
using UnityEngine.UI;

namespace BodySystem
{
    public class ZoomUI : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] Button plusButton;
        [SerializeField] Button minusButton;
        [SerializeField] GameObject panel;


        User user;

        void Start()
        {
            user = FindObjectOfType<User>();

            panel.SetActive(false);
        }
        public void EnableButton(bool enabled)
        {
            button.interactable = enabled;
        }

        public void ZoomIn()
        {
            user.ZoomIn();
        }
        public void ZoomOut()
        {
            user.ZoomOut();
        }
        public bool IsUIActive()
        {
            return button.IsInteractable();
        }
        public void ShowUI()
        {
            panel.SetActive(true);
        }
        public void HideUI()
        {
            panel.SetActive(false);
        }
        public void UpdateZoom(bool canZoomIn, bool canZoomOut)
        {
            plusButton.interactable = canZoomIn;
            minusButton.interactable = canZoomOut;
        }

    }
}