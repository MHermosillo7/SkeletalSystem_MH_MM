using UnityEngine;

public class Sources : MonoBehaviour
{
    [SerializeField] GameObject info;
    [SerializeField] GameObject programming;
    [SerializeField] GameObject logo;
    [SerializeField] GameObject team;


    GameObject activeObject;

    SourceUI sourceUI;
    // Start is called before the first frame update
    void Start()
    {
        info.SetActive(false);
        programming.SetActive(false);
        logo.SetActive(false);
        team.SetActive(true);

        activeObject = team;

        sourceUI = FindObjectOfType<SourceUI>();
    }

    public void SwitchSource(string source)
    {
        activeObject.SetActive(false);

        switch (source.ToLower())
        {
            case "information":
                info.SetActive(true);

                activeObject = info;
                break;

            case "programming":
                programming.SetActive(true);

                activeObject = programming;
                break;

            case "logo":
                logo.SetActive(true);

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
