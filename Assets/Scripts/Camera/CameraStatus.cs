using UnityEngine;

namespace BodySystem
{
    public class CameraStatus : MonoBehaviour
    {
        public bool cameraCanMove
        {
            get { return this; }
            private set { cameraCanMove = value; }
        }

        [SerializeField] Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            anim.SetBool("CameraCanMove", cameraCanMove);
        }

        public void UpdateCamStatus(bool status)
        {
            cameraCanMove = status;
            anim.SetBool("CameraCanMove", cameraCanMove);
        }
    }
}