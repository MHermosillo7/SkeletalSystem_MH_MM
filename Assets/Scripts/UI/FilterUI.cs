using System;
using UnityEngine;
using UnityEngine.UI;

namespace BodySystem
{
    public class FilterUI : MonoBehaviour
    {
        [SerializeField] GameObject panel;

        [SerializeField] Toggle toggleLong;
        [SerializeField] Toggle toggleShort;
        [SerializeField] Toggle toggleFlat; 
        [SerializeField] Toggle toggleIrregular;

        InfoUI infoUI;
        HelpUI helpUI;
        ZoomUI zoomUI;

        Filter filter;
        // Start is called before the first frame update
        void Start()
        {
            infoUI = FindObjectOfType<InfoUI>();
            helpUI = FindObjectOfType<HelpUI>();
            TryGetComponent<ZoomUI>(out zoomUI);
            filter = FindObjectOfType<Filter>();

            if (panel)
            {
                HideUI();
            }
            else Console.Error.WriteLine("Panel has not been selected");

            CheckToggle(toggleLong);
            CheckToggle(toggleShort);
            CheckToggle(toggleFlat);
            CheckToggle(toggleIrregular);
        }

        public void HideUI()
        {
            panel.SetActive(false);
        }
        public void ShowUI()
        {
            infoUI.HideUI();
            helpUI.HideUI();
            if (zoomUI)
            {
                zoomUI.HideUI();
            }
            panel.SetActive(true);
        }

        void CheckToggle(Toggle toggle)
        {
            switch (toggle.tag.ToLower())
            {
                case "togglelong":

                    if(filter.CheckListCount("long") == 0)
                    {
                        toggleLong.interactable = false;
                    }
                    break;

                case "toggleshort":

                    if(filter.CheckListCount("short") == 0)
                    {
                        toggleShort.interactable = false;
                    }
                    break;

                case "toggleflat":

                    if (filter.CheckListCount("flat") == 0)
                    {
                        toggleFlat.interactable = false;
                    }
                    break;

                case "toggleirregular":

                    if (filter.CheckListCount("irregular") == 0)
                    {
                        toggleIrregular.interactable = false;
                    }
                    break;

                default:
                    print("Type of Toggle no Found");
                    break;
            }
        }
    }
}