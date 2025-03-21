using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodySystem
{
    public class Information : MonoBehaviour
    {
        [Header("English")]
        public string partName;
        public List<string> function = new List<string>();
        public List<string> structure = new List<string>();
        public List<string> components = new List<string>();

        public Transform pivot;

        [Header("Spanish")]
        public string partName_ES = null;
        public List<string> function_ES = new List<string>();
        public List<string> structure_ES = new List<string>();
        public List<string> components_ES = new List<string>();

        public bool needsCenter = false;

        Camp comp;

        char[] charsToTrim = { ';' };

        // Start is called before the first frame update
        void Awake()
        {
            partName = name;
            
            if (function.Count == 0 || structure.Count == 0 || components.Count == 0)
            {
                Console.WriteLine("Field is empty");
            }
            GetPivot();

            //Temporal fixes while translation is done
            if(partName_ES == null || partName_ES == "")
            {
                partName_ES = name;
            }
            if(function_ES.Count == 0)
            {
                function_ES.Add("Traduccion en Progreso");
                function_ES.Add("");

                foreach(string sentence in function)
                {
                    function_ES.Add(sentence);
                }
            }
            if (structure_ES.Count == 0)
            {
                structure_ES.Add("    Traduccion en Progreso");
                structure_ES.Add("");

                foreach (string sentence in structure)
                {
                    structure_ES.Add(sentence);
                }
            }
            if (components_ES.Count == 0)
            {
                components_ES.Add("    Traduccion en Progreso");
                components_ES.Add("");

                foreach (string sentence in components)
                {
                    components_ES.Add(sentence);
                }
            }
        }
        //English Information Getters
        public string GetName()
        {
            return partName;
        }
        public string GetFunction()
        {
            return NiceString(function);
        }
        public string GetStructure()
        {
            return NiceString(structure);
        }
        public string GetComponents()
        {
            return NiceString(components);
        }

        //Spanish Information Getters
        public string GetName_ES()
        {
            return partName_ES;
        }
        public string GetFunction_ES()
        {
            return NiceString(function_ES);
        }
        public string GetStructure_ES()
        {
            return NiceString(structure_ES);
        }
        public string GetComponents_ES()
        {
            return NiceString(components_ES);
        }

        //Iterates through given list, formatting them in a bullet point style
        //Then, it deletes the last break of page to ensure there is no blank line
        //Lastly, it returns the new, formatted, single string
        private string NiceString(List<string> info)
        {
            string niceString = "";

            foreach (string str in info)
            {
                if(str == "")
                {
                    niceString += "\n";
                }
                else
                {
                    niceString += $"- {str} \n";
                }
            }

            niceString = niceString.Substring(0, niceString.Length - 2);

            return niceString;
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

        //Used to transcribe information from component (Camp) script through trimming
        //the information of respective fields by using keywords

        //Very useful when introducing new information due to automatic format function

        private void Start()
        {
            TryGetComponent<Camp>(out comp);

            if (comp != null)
            {
                CheckIfNull(function_ES, "f");
                CheckIfNull(structure_ES, "s");
                CheckIfNull(components_ES, "c");
            }
        }
        void CheckIfNull(List<string> list, string value)
        {
            list.Clear();
            switch (value)
            {
                case "f":
                    Trim(comp.function, list);
                    break;
                case "s":
                    Trim(comp.structure, list);
                    break;
                case "c":
                    Trim(comp.components, list);
                    break;
            }

        }
        void Trim(string sentence, List<string> list)
        {
            string[] newList = sentence.Split(charsToTrim);
            foreach (string word in newList)
            {
                word.Trim(charsToTrim);
            }
            for (int i = 0; i < newList.Length; i++)
            {
                string word = newList[i];
                if (word != "")
                {
                    while (word.StartsWith(" "))
                    {
                        word = word.TrimStart(' ');
                    }
                    while (word.EndsWith(" "))
                    {
                        word = word.TrimEnd(' ');
                    }
                    list.Add(word);
                }

            }
        }

    }

}