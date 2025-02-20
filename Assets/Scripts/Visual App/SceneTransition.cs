using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] static Animator transition;

    // Start is called before the first frame update
    void Awake()
    {
        transition = GetComponent<Animator>();
        PlayIntroAnim();
    }

    public static void PlayIntroAnim()
    {
        transition.SetTrigger("Intro");
    }
    public static void PlayOutroAnim()
    {
        print("Hello");
        transition.SetTrigger("Outro");
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(GameManager.newSceneIndex);
    }
}
