using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int imageIndex = 0;

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
        SceneManager.LoadScene(sceneIndex);
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
}
