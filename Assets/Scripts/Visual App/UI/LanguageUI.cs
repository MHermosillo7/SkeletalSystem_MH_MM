using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LanguageUI : MonoBehaviour
{
    public List<GameObject> languageButtons = new List<GameObject>();
    bool enable = true;

    // Awake is called while compiling
    void Awake()
    {
        if(languageButtons.Count == 0)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            HideUI();
        }
    }
    public void ControlUI()
    {
        foreach (GameObject button in languageButtons)
        {
            button.SetActive(enable);
        }

        enable = !enable;
    }
    public void ShowUI()
    {
        foreach (GameObject button in languageButtons)
        {
            button.SetActive(true);
        }
    }
    void HideUI()
    {
        foreach (GameObject button in languageButtons)
        {
            button.SetActive(false);
        }
    }
}
