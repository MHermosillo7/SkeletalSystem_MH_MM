using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] static Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
        PlayIntroAnim();
    }

    public static void PlayIntroAnim()
    {
        anim.SetTrigger("Intro");
    }
    public static void PlayOutroAnim()
    {
        anim.SetTrigger("Outro");
    }
}
