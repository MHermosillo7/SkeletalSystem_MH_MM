using UnityEngine;

namespace BodySystem
{
    [RequireComponent(typeof(Collider2D))]
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] GameObject textPanel;
        
        private void Awake()
        {
            textPanel.SetActive(false);
        }
        private void OnMouseEnter()
        {
            textPanel.SetActive(true);
        }
        private void OnMouseExit()
        {
            textPanel.SetActive(false);
        }
    }
}