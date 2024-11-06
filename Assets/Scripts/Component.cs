using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Linq;

namespace BodySystem
{
    public class Component : MonoBehaviour
    {
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
        }
    }
}
