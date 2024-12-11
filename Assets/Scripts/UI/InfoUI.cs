using UnityEngine;
using UnityEngine.UI;

namespace BodySystem
{
    public class InfoUI : MonoBehaviour
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
            body.text = GetComponent().GetFunction();
        }
        public void GetStructureInfo()
        {
            EnablePanel();
            body.text = GetComponent().GetStructure();
        }
        public void GetComponentsInfo()
        {
            EnablePanel();
            body.text = GetComponent().GetDerived();
        }
        public void GetName()
        {
            nameText.text = GetComponent().GetName();
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
        }
        Information GetComponent()
        {
            return userScript.selectedItem.GetComponent<Information>();
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
