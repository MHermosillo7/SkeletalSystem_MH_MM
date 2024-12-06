using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sources : MonoBehaviour
{
    [SerializeField] GameObject info;
    [SerializeField] GameObject programming;
    [SerializeField] GameObject logo;

    Animator infoAnim;
    Animator programmingAnim;
    Animator logoAnim;

    GameObject activeObject;

    SourceUI sourceUI;
    // Start is called before the first frame update
    void Start()
    {
        info.SetActive(true);
        programming.SetActive(false);
        logo.SetActive(false);

        infoAnim = info.GetComponent<Animator>();
        programmingAnim = programming.GetComponent<Animator>();
        logoAnim = logo.GetComponent<Animator>();

        activeObject = info;
        infoAnim.SetTrigger("Start");

        sourceUI = FindObjectOfType<SourceUI>();
    }

    public void SwitchSource(string source)
    {
        activeObject.GetComponent<Animator>().SetTrigger("Reset");

        activeObject.SetActive(false);

        switch (source.ToLower())
        {
            case "information":
                info.SetActive(true);

                infoAnim.SetTrigger("Start");

                activeObject = info;
                break;

            case "programming":
                programming.SetActive(true);

                programmingAnim.SetTrigger("Start");

                activeObject = programming;
                break;

            case "logo":
                logo.SetActive(true);

                logoAnim.SetTrigger("Start");

                activeObject = logo;
                break;
        }
        sourceUI.TogglePanel();
    }
}
