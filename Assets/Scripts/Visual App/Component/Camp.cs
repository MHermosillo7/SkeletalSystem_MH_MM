using UnityEngine;
using System;
using System.Collections;

namespace BodySystem
{
    public class Camp : MonoBehaviour
    {
        //Component Information
        public string partName;
        public string function;
        public string structure;
        public string components;

        //Higlight Control
        [SerializeField] Color changedColor = Color.gray;
        Color startColor;

        float changeProgress = 0f;
        float changeTime = 1;
        [Range(0, 1)][SerializeField] float rate = 1f;

        Coroutine onEnterCoroutine;
        Coroutine onExitCoroutine;
        Renderer rend;

        public enum ZoomLevel
        {
            Zero,
            One,
            Two,
            Three,
        }
        public ZoomLevel zoomLevel;

        public Transform pivot;

        private void Awake()
        {
            if (partName == null)
            {
                partName = this.name;
            }
            if (function == null || structure == null || components == null)
            {
                Console.Error.WriteLine($"Field on {this.name} was left blank");
            }

            //Get Renderer and object's original color
            rend = GetComponent<Renderer>();

            startColor = rend.material.color;

            GetPivot();
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
            //Reset object's current color to original
            ResetColor();
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

        public void ResetColor()
        {
            //Stop On Enter Coroutine
            StopCoroutine(onEnterCoroutine);

            //Reset color change progress
            changeProgress = 0f;

            //Reset object's current color to original
            onExitCoroutine = StartCoroutine(ChangeColor(rend.material.color, startColor));
        }

        public bool IsStartingColor()
        {
            return rend.material.color == startColor;
        }
        void GetPivot()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.CompareTag("Pivot"))
                {
                    pivot = child;

                    return;
                }
            }
        }
    }
}
