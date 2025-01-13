using UnityEngine;

namespace BodySystem
{
    public class HelpUI : MonoBehaviour
    {
        [SerializeField] GameObject panel;

        InfoUI infoUI;
        FilterUI filterUI;
        ZoomUI zoomUI;

        // Start is called before the first frame update
        void Awake()
        {
            if (panel) panel.SetActive(false);

            else print("Panel not specified in inspector");

            infoUI = FindObjectOfType<InfoUI>();
            filterUI = FindObjectOfType<FilterUI>();
            zoomUI = FindObjectOfType<ZoomUI>();
        }

        public void ShowUI()
        {
            panel.SetActive(true);

            infoUI.HideUI();
            if (filterUI)
            {
                filterUI.HideUI();
            }
            if (zoomUI)
            {
                zoomUI.HideUI();
            }
        }
        public void HideUI()
        {
            panel.SetActive(false);
        }
    }
}