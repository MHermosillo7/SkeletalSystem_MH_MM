using UnityEngine;
using TMPro;

namespace BodySystem
{
    public class UI : MonoBehaviour
    {
        public TMP_Text header;
        [SerializeField] TMP_Text body;
        public GameObject panel;
        User userScript;

        private void Start()
        {
            userScript = FindObjectOfType<User>();
            HideUI();

            //MODIFY THIS LATER, KEPT FOR TEST
            body.text = "";
        }
        public void GetFunctionInfo()
        {
            body.text = GetComponent().function;
        }
        public void GetStructureInfo()
        {
            body.text = GetComponent().structure;
        }
        public void GetComponentsInfo()
        {
            body.text = GetComponent().components;
        }
        public void GetName()
        {
            header.text = GetComponent().partName;
        }
        public void HideUI()
        {
            body.text = "";
            panel.SetActive(false);
        }
        public void ShowUI()
        {
            GetName();
            panel.transform.position = Input.mousePosition;
            panel.SetActive(true);
        }

        Component GetComponent()
        {
            return userScript.selectedItem.GetComponent<Component>();
        }
    }
}
