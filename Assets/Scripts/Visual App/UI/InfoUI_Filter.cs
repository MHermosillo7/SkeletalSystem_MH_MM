using UnityEngine;
using UnityEngine.UI;

/*namespace BodySystem
{
    public class InfoUI_Filter : MonoBehaviour
    {
        [SerializeField] GameObject uiPivot;
        [SerializeField] Button nameButton;
        [SerializeField] Text nameText;
        [SerializeField] GameObject buttons;
        [SerializeField] Image image;
        [SerializeField] Text body;

        V1User userScript;
        InfoPopUp popUp;

        private void Start()
        {
            userScript = FindObjectOfType<V1User>();
            popUp = FindObjectOfType<InfoPopUp>();

            HideUI();
        }
        public void GetFunctionInfo()
        {
            body.text = GetComponent().GetFunction();

            //Checks whether pop up is completely
            //visible even after changing info
            EnablePanel();
        }
        public void GetStructureInfo()
        {
            body.text = GetComponent().GetStructure();

            EnablePanel();
        }
        public void GetComponentsInfo()
        {
            body.text = GetComponent().GetDerived();

            EnablePanel();
        }
        public void GetName()
        {
            if (userScript.selectedItemComp != null)
            {
                nameText.text = GetComponent().GetName();
            }
            else
            {
                print("Other name");
                nameText.text = userScript.selectedBasicComp.GetName();
            }
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
            return userScript.selectedItemComp;
        }
        public void EnableButtons()
        {
            if (userScript.selectedItemComp != null)
            {
                buttons.SetActive(true);
            }
        }
        void EnablePanel()
        {
            if (image.IsActive() == false)
            {
                image.gameObject.SetActive(true);
            }
            popUp.RunPopUpCheck();
        }
    }
}*/
