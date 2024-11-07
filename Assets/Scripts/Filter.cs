using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BodySystem
{
    public class Filter : MonoBehaviour
    {
        [SerializeField] List<GameObject> bones = new List<GameObject>();
        List<Component> boneComponents = new List<Component>();

        CameraMovement camMov;

        [SerializeField] bool activateLong = true;
        [SerializeField] bool activateShort = true;
        [SerializeField] bool activateFlat = true;
        [SerializeField] bool activateIrr = true;
        // Awake is called when loading scene
        void Awake()
        {
            bones.AddRange(GameObject.FindGameObjectsWithTag("Bone"));

            boneComponents = (from b in bones 
                              select b.GetComponent<Component>()).ToList();

            camMov = FindObjectOfType<CameraMovement>();
        }

        void FilterByType(string type, bool activate)
        {
            //Center camera around main scene pivot 
            //Or could probably implement so that it center first object in list of non-filtered bones

            //Error: Unity cannot find inactive game objects.
            //(Modify hierarchy to have an independent main pivot)
            camMov.CenterCamera(GameObject.FindGameObjectWithTag("Origin").transform);

            var filteredBones = boneComponents.Where(b => b.boneType.ToString().ToLower() == type);

            foreach (var bone in filteredBones)
            {
                bone.gameObject.SetActive(activate);
            }
        }
        public void FilterLong()
        {
            activateLong = !activateLong; 
            FilterByType("long", activateLong);
        }
        public void FilterShort()
        {
            activateShort = !activateShort;
            FilterByType("short", activateShort);
        }
        public void FilterFlat()
        {
            activateFlat = !activateFlat;
            FilterByType("flat", activateFlat);
        }
        public void FilterIrregular()
        {
            activateIrr = !activateIrr;
            FilterByType("irregular", activateIrr);
        }
    }
}
