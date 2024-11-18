using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] string path = "C:/Users";
    [SerializeField] string userName = Environment.UserName;
    [SerializeField] Folder folder = Folder.Downloads;
    [SerializeField] string imageName = "BoneImage";
    [SerializeField] ImageType type = ImageType.png;

    [SerializeField] int index = 0; //Number of pictures taken

    Canvas canvas;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public IEnumerator TakeScreenshot()
    {
        canvas.enabled = false;

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot
            ($"{path}/{userName}/{folder}/{imageName}{index}.{type}");

        index++;
        canvas.enabled = true;
    }
    public void TakeScreenShot()
    {
        StartCoroutine(TakeScreenshot());
    }
}
