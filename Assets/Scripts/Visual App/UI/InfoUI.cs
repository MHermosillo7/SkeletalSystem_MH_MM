using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace BodySystem
{
    public class InfoUI : MonoBehaviour
    {
        [SerializeField] GameObject uiPivot;
        [SerializeField] Button nameButton;
        [SerializeField] Text nameText;
        [SerializeField] GameObject buttonControl;
        [SerializeField] List<Text> buttonText = new List<Text>();
        [SerializeField] Image image;
        [SerializeField] Text body;

        User user;
        V1User user1;
        InfoPopUp popUp;

        private void Start()
        {
            CheckLanguage();

            user = FindObjectOfType<User>();

            //Avoids having to duplicate the script
            if(user == null)
            {
                user1 = FindObjectOfType<V1User>();
            }

            popUp = FindObjectOfType<InfoPopUp>();

            HideUI();
        }
        public void GetFunctionInfo()
        {
            if(GameManager.language == "english")
            {
                body.text = GetComponent().GetFunction();
            }
            else
            {
                body.text = GetComponent().GetFunction_ES();
            }

            //Checks whether pop up is completely
            //visible even after changing info
            EnablePanel();
        }
        public void GetStructureInfo()
        {
            if (GameManager.language == "english")
            {
                body.text = GetComponent().GetStructure();
            }
            else
            {
                body.text = GetComponent().GetStructure_ES();
            }
            
            EnablePanel();
        }
        public void GetComponentsInfo()
        {
            if (GameManager.language == "english")
            {
                body.text = GetComponent().GetComponents();
            }
            else
            {
                body.text = GetComponent().GetComponents_ES();
            }
            
            EnablePanel();
        }

        public void GetName()
        {
            if (user != null)
            {
                CheckForName_User();
            }
            else
            {
                CheckForName_User1();
            }
        }
        void CheckForName_User()
        {
            if(GameManager.language == "english")
            {
                if (user.selectedItemComp != null)
                {
                    nameText.text = GetComponent().GetName();
                }
                else if (user.selectedBasicComp != null)
                {
                    nameText.text = user.selectedBasicComp.GetName();
                }
            }
            else
            {
                if (user.selectedItemComp != null)
                {
                    nameText.text = GetComponent().GetName_ES();
                }
                else if (user.selectedBasicComp != null)
                {
                    nameText.text = user.selectedBasicComp.GetName_ES();
                }
            }
        }
        void CheckForName_User1()
        {
            if(GameManager.language == "english")
            {
                if (user1.selectedItemComp != null)
                {
                    nameText.text = GetComponent().GetName();
                }
                else if (user1.selectedBasicComp != null)
                {
                    nameText.text = user.selectedBasicComp.GetName();
                }
            }
            else
            {
                if (user1.selectedItemComp != null)
                {
                    nameText.text = GetComponent().GetName_ES();
                }
                else if (user1.selectedBasicComp != null)
                {
                    nameText.text = user.selectedBasicComp.GetName_ES();
                }
            }
        }
        public void HideUI()
        {
            body.text = "";
            buttonControl.SetActive(false);
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
            if(user != null)
            {
                return user.selectedItemComp;
            }
            else
            {
                return user1.selectedItemComp;
            }
        }
        public void EnableButtons()
        {
            if (user != null)
            {
                if (user.selectedItemComp != null)
                {
                    buttonControl.SetActive(true);
                }
            }
            else
            {
                if (user1.selectedItemComp != null)
                {
                    buttonControl.SetActive(true);
                    print("User 1");
                }
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
        void CheckLanguage()
        {
            if (GameManager.language == "english")
            {
                buttonText[0].text = "Function";
                buttonText[1].text = "Structure";
                buttonText[2].text = "Components";
            }
            else
            {
                buttonText[0].text = "Función";
                buttonText[1].text = "Estructura";
                buttonText[2].text = "Componentes";
            }
        }
    }
}
