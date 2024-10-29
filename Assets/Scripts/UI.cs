using UnityEngine.UI;
using UnityEngine;

namespace BodySystem
{
    public class UI : MonoBehaviour
    {
        [SerializeField] Text text;
        User userScript;

        void GetFunctionInfo()
        {
            text.text = GetComponent().function;
        }
        void GetStructureInfo()
        {
            text.text = GetComponent().structure;
        }
        void GetComponentsInfo()
        {
            text.text = GetComponent().components;
        }

        Component GetComponent()
        {
            return userScript.selectedItem.GetComponent<Component>();
        }
    }
}
