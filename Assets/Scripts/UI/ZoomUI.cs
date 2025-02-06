using UnityEngine;
using UnityEngine.UI;

namespace BodySystem
{
    public class ZoomUI : MonoBehaviour
    {
        [SerializeField] Button plusButton;
        [SerializeField] Button minusButton;

        User user;

        void Start()
        {
            user = FindObjectOfType<User>();
        }

        public void ZoomIn()
        {
            user.ZoomIn();
        }
        public void ZoomOut()
        {
            user.ZoomOut();
        }
        public void UpdateZoom(bool canZoomIn, bool canZoomOut)
        {
            plusButton.interactable = canZoomIn;
            minusButton.interactable = canZoomOut;
        }

    }
}