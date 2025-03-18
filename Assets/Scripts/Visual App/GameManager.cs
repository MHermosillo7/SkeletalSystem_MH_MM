using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int imageIndex = 0;

    public static int newSceneIndex;

    public static string language = "english";

    // Start is called before the first frame update
    void Start()
    {

        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public static void LoadScene(int sceneIndex)
    {
        newSceneIndex = sceneIndex;

        SceneTransition.PlayOutroAnim();
    }
    public static void Quit()
    {
        Application.Quit();
    }
    public static int GetImageIndex()
    {
        return imageIndex;
    }
    public static void AddImageIndex()
    {
        imageIndex++;
    }

    public static void ChangeLanguage(string languageToChange)
    {
        switch (languageToChange.ToLower())
        {
            case "spanish":
                language = "spanish";
                break;

            case "english":
                language = "english";
                break;
        }
    }
}
