using System;
using UnityEngine;

namespace BodySystem
{
    public class FilterUI : MonoBehaviour
    {
        [SerializeField] GameObject panel;

        InfoUI infoUI;
        // Start is called before the first frame update
        void Start()
        {
            infoUI = FindObjectOfType<InfoUI>();
            if (panel)
            {
                HideUI();
            }
            else Console.Error.WriteLine("Panel has not been selected");
        }

        public void HideUI()
        {
            panel.SetActive(false);
        }
        public void ShowUI()
        {
            infoUI.HideUI();
            panel.SetActive(true);
        }
    }
}