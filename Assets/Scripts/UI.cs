using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace BodySystem
{
    public class UI : MonoBehaviour
    {
        public GameObject uiPivot;
        public Text header;
        [SerializeField] GameObject buttons;
        [SerializeField] GameObject panel;
        bool panelEnabled;
        [SerializeField] Text body;

        User userScript;

        private void Start()
        {
            userScript = FindObjectOfType<User>();
            HideUI();
            panelEnabled = false;

            //MODIFY THIS LATER, KEPT FOR TEST
            body.text = "";
        }
        public void GetFunctionInfo()
        {
            UpdatePanel();
            body.text = GetComponent().function;
        }
        public void GetStructureInfo()
        {
            UpdatePanel();
            body.text = GetComponent().structure;
        }
        public void GetComponentsInfo()
        {
            UpdatePanel();
            body.text = GetComponent().components;
        }
        public void GetName()
        {
            header.text = GetComponent().partName;
        }
        public void HideUI()
        {
            body.text = "";
            UpdatePanel();
            buttons.SetActive(true);
;           uiPivot.SetActive(false);

        }
        public void ShowUI()
        {
            GetName();
            uiPivot.transform.position = Input.mousePosition;
            uiPivot.SetActive(true);
        }

        Component GetComponent()
        {
            return userScript.selectedItem.GetComponent<Component>();
        }

        public void EnableButtons()
        {
            buttons.SetActive(true);
        }
        void UpdatePanel()
        {
            if (!panelEnabled)
            {
                panel.SetActive(true);
                panelEnabled = true;
            }
            else
            {
                panel.SetActive(false);
                panelEnabled = false;
            }
        }
    }
}
