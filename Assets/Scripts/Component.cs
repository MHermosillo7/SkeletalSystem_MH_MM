using UnityEngine;
using System;
using System.Collections;

namespace BodySystem
{
    public class Component : MonoBehaviour
    {
        //Component Information
        public enum Type
        {
            Long,
            Short,
            Flat,
            Irregular
        }

        public string partName;
        public Type boneType;
        public string function;
        public string structure;
        public string components;

        //Zoom In/Out Control & Child Control
        GameObject zoomedBone;
        Collider col;

        public Transform vector;

        //Higlight Control
        [SerializeField] Color changedColor = Color.gray;
        Color startColor;

        float changeProgress = 0f;
        float changeTime = 1;
        [Range(0, 1)] [SerializeField] float rate = 1f;

        Coroutine onEnterCoroutine;
        Coroutine onExitCoroutine;
        Renderer rend;

        private void Start()
        {
            if (partName == "")
            {
                partName = this.name;
            }
            if (function == null|| structure == null)
            {
                Console.Error.WriteLine($"Field on {this.name} was left blank");
            }

            //Get Renderer and object's original color
            rend = GetComponent<Renderer>();
            startColor = rend.material.color;

            vector = this.transform.GetChild(0);
            zoomedBone = this.transform.GetChild(1).gameObject;
            zoomedBone.SetActive(false);
            col = this.GetComponent<Collider>();
        }

        private void OnMouseEnter()
        {
            //If Mouse has entered object previously
            if (onExitCoroutine != null)
            {
                //Stop On Exit Coroutine
                StopCoroutine(onExitCoroutine);

                //Reset color change progress
                changeProgress = 0f;
            }

            //Change object's color to a changed color
            onEnterCoroutine = StartCoroutine(ChangeColor(startColor, changedColor));
        }

        private void OnMouseExit()
        {
            //Stop On Enter Coroutine
            StopCoroutine(onEnterCoroutine);

            //Reset color change progress
            changeProgress = 0f;

            //Reset object's color to original
            onExitCoroutine = StartCoroutine(ChangeColor(rend.material.color, startColor));
        }

        //Uses Lerp to gradually change object's color given starting and end colors
        IEnumerator ChangeColor(Color start, Color end)
        {
            while (changeProgress < changeTime)
            {
                rend.material.color =
                    Color.Lerp(start, end, changeProgress);

                changeProgress += Time.deltaTime * rate;

                yield return null;
            }
            changeProgress = 0f;
            rend.material.color = end;
        }

        //Zoom In/Out Functions
        public void ZoomIn()
        { 
            StopAllCoroutines();
            col.enabled = false;
            rend.enabled = false;

            zoomedBone.SetActive(true);
        }

        public void ZoomOut()
        {
            zoomedBone.SetActive(false);

            col.enabled = true;
            rend.enabled = true;
        }
    }
}
