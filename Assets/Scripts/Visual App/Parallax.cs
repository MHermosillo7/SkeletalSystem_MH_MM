using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    List<Transform> images = new List<Transform>();

    //An approximate of how far the object can go.
    /*  I am unsure how reliable screen.width is,
        so I hard coded the limits just in case*/
    Vector3 outOfScreen = new Vector3 (1866.7f, 0, 0);
    Vector3 resetPosition;

    //The movement speed is far too fast even at 5,
    //so set limit and implemented decimals to further control speed
    [SerializeField, Range(0, 5)] float moveSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Transform child in transform)
        {
            images.Add(child);
        }

    }
    private void Start()
    {

        resetPosition = images.Last().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ParallaxControl();
    }

    void ParallaxControl()
    {
        Vector2 moveForce = Vector2.right * moveSpeed;
        /*for(int i = 0; i < images.Count; i++)
        {
            images[i].Translate(moveForce);

            if (images[)
        }*/
        foreach(Transform image in images)
        {
            if(image.localPosition.x >= outOfScreen.x)
            {
                //The images are offset between one another (one more up than other),
                //So I can't just reset the whole position, I have to maintain its y-coordinate
                image.position = new Vector2(resetPosition.x, image.position.y);
            }
            image.Translate(moveForce);
        }
    }
}