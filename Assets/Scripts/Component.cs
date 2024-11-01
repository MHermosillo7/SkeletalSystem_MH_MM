using Unity.VisualScripting;
using UnityEngine;

namespace BodySystem
{
    public class Component : MonoBehaviour
    {
        public string partName;
        public string function;
        public string structure;
        public string components;

        private void Start()
        {
            if (partName == "")
            {
                partName = this.name;
            }
        }
    }
}
