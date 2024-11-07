using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BodySystem
{
    public class InfoPopUI : MonoBehaviour
    {
        [SerializeField] GameObject uiPivot;
        [SerializeField] Button nameButton;
        [SerializeField] Text nameText;
        [SerializeField] GameObject buttons;
        [SerializeField] Image image;
        [SerializeField] Text body;

        User userScript;

        private void Start()
        {
            userScript = FindObjectOfType<User>();

            HideUI();
        }
        public void GetFunctionInfo()
        {
            EnablePanel();
            body.text = GetComponent().function;
        }
        public void GetStructureInfo()
        {
            EnablePanel();
            body.text = GetComponent().structure;
        }
        public void GetComponentsInfo()
        {
            EnablePanel();
            body.text = GetComponent().components;
        }
        public void GetName()
        {
            nameText.text = GetComponent().partName;
        }
        public void HideUI()
        {
            body.text = "";
            buttons.SetActive(false);
            image.gameObject.SetActive(false);
            nameButton.gameObject.SetActive(false);

        }
        public void ShowUI()
        {
            GetName();
            uiPivot.transform.position = Input.mousePosition;
            nameButton.gameObject.SetActive(true);
            print("hello");
        }

        Component GetComponent()
        {
            return userScript.selectedItem.GetComponent<Component>();
        }

        public void EnableButtons()
        {
            buttons.SetActive(true);
        }
        void EnablePanel()
        {
            if (image.IsActive() == false)
            {
                image.gameObject.SetActive(true);
            }
        }
    }
}
