using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    enum Folder
    {
        Downloads,
        Documents
    }
    enum ImageType
    {
        png,
        jpg
    }
    string completePath;
    [SerializeField] string startPath = "C:/Users";
    [SerializeField] string userName = Environment.UserName;
    [SerializeField] Folder folder = Folder.Downloads;
    [SerializeField] string projectFolder = "Skeletal System";
    [SerializeField] string imageName = "BoneImage";
    [SerializeField] ImageType type = ImageType.png;

    [SerializeField] int index = 0; //Number of pictures taken

    // Unused as of now
    // (planned to prompt user whether to
    // delete previous screenshots or not)
    bool cleanDirectory = true;

    Canvas canvas;

    Animator anim;

    private void Awake()
    {
        completePath = $"{startPath}/{userName}/{folder}/{projectFolder}";

        // ONLY works for Windows devices due to 'using UnityEngine.Windows'
        // Change to using 'UnityEngine.IO' in order for it to work on other devices.
        if (!Directory.Exists(completePath))
        {
            Directory.CreateDirectory(completePath);
        }
        else if (cleanDirectory)
        {
            CleanDirectory();
            Directory.CreateDirectory(completePath);
        }
        canvas = FindObjectOfType<Canvas>();
        anim = GetComponent<Animator>();
    }

    IEnumerator TakeScreenshot()
    {
        canvas.enabled = false;
        CheckForScreenShot();

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot
            ($"{completePath}/{imageName}{index}.{type}");

        index++;
        canvas.enabled = true;
    }
    public void TakeScreenShot()
    {
        StartCoroutine(TakeScreenshot());
    }

    void CleanDirectory()
    {
        Directory.Delete(completePath);
    }
    void CheckForScreenShot()
    {
        //Only works for Windows devices due to calling UnityEngine.Windows.File.Exists()
        while (File.Exists($"{completePath}/{imageName}{index}"))
        {
            index++;
        }
    }
    public void TriggerAnimation()
    {
        anim.SetTrigger("Saved");
    }
}
