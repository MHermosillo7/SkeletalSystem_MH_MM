using UnityEngine;

namespace BodySystem
{
    public class HelpUI : MonoBehaviour
    {
        [SerializeField] GameObject panel;

        InfoUI infoUI;
        InfoUI infoUI_Filter;
        FilterUI filterUI;

        // Start is called before the first frame update
        void Awake()
        {
            if (panel) panel.SetActive(false);

            else print("Panel not specified in inspector");

            infoUI = FindObjectOfType<InfoUI>();
            if (infoUI == null )
            {
                infoUI_Filter = FindObjectOfType<InfoUI>();
            }
            filterUI = FindObjectOfType<FilterUI>();
        }

        public void ShowUI()
        {
            panel.SetActive(true);

            if (infoUI)
            {
                infoUI.HideUI();
            }
            else
            {
                infoUI_Filter.HideUI();
            }
            if (filterUI)
            {
                filterUI.HideUI();
            }
        }
        public void HideUI()
        {
            panel.SetActive(false);
        }
    }
}