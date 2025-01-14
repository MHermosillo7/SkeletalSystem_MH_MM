using UnityEngine;

public class Sources : MonoBehaviour
{
    [SerializeField] GameObject info;
    [SerializeField] GameObject programming;
    [SerializeField] GameObject logo;
    [SerializeField] GameObject team;

    Animator infoAnim;
    Animator programmingAnim;
    Animator logoAnim;

    GameObject activeObject;
    Animator activeAnim;

    SourceUI sourceUI;
    // Start is called before the first frame update
    void Start()
    {
        info.SetActive(false);
        programming.SetActive(false);
        logo.SetActive(false);
        team.SetActive(true);

        infoAnim = info.GetComponent<Animator>();
        programmingAnim = programming.GetComponent<Animator>();
        logoAnim = logo.GetComponent<Animator>();

        activeObject = team;

        sourceUI = FindObjectOfType<SourceUI>();
    }

    public void SwitchSource(string source)
    {
        TryGetComponent<Animator>(out activeAnim);

        if (activeAnim)
        {
            activeAnim.SetTrigger("Reset");
        }

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

            case "team":
                team.SetActive(true);
                activeObject = team;
                break;
                
        }
        sourceUI.TogglePanel();
    }
}
