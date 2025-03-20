using System.Data;
using UnityEngine;

namespace BodySystem
{
    public class CameraStatus : MonoBehaviour
    {
        public bool cameraCanMove;     //Whether camera can move

        [SerializeField] Animator anim; //Camera status animator

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            anim.SetBool("CameraCanMove", cameraCanMove);
        }

        //Triggers & updates camera animation demonstrating whether 
        // the camera can move according to input passed to function 
        public void UpdateCamStatus(bool status)
        {
            
            cameraCanMove = status;
            anim.SetBool("CameraCanMove", cameraCanMove);
        }
    }
}