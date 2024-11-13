using System;
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
        User user;

        [SerializeField] bool activateLong = true;
        [SerializeField] bool activateShort = true;
        [SerializeField] bool activateFlat = true;
        [SerializeField] bool activateIrr = true;

        List<GameObject> longBones = new List<GameObject>();
        List<GameObject> shortBones = new List<GameObject>();
        List<GameObject> flatBones = new List<GameObject>();
        List<GameObject> irrBones = new List<GameObject>();
        // Awake is called when loading scene
        void Awake()
        {
            bones.AddRange(GameObject.FindGameObjectsWithTag("Bone"));
            bones.AddRange(GameObject.FindGameObjectsWithTag("DerivedBone"));

            boneComponents = (from b in bones 
                              select b.GetComponent<Component>()).ToList();

            camMov = FindObjectOfType<CameraMovement>();
            user = FindObjectOfType<User>();

            longBones.AddRange(GetBoneType("long"));
            shortBones.AddRange(GetBoneType("short"));
            flatBones.AddRange(GetBoneType("flat"));
            irrBones.AddRange(GetBoneType("irregular"));
        }

        // Use LINQ querys to get to search through a list of all object's component script
        // Then select and return game objects list whose type of bone is same as input field
        List<GameObject> GetBoneType(string type)
        {
            type = type.ToLower();

            var filteredBones = boneComponents
                                .Where(b => b.boneType.ToString().ToLower() == type)
                                .Select(b => b.gameObject);

            return filteredBones.ToList();
        }

        // Center Camera to main pivot in scene and 
        // Activate/Deactivate objects in input list
        void FilterByType(List<GameObject> boneType, bool activate)
        {
            // Center camera around main scene pivot 
            // Or could probably implement so that it center first object in list of non-filtered bones

            // Error: Unity cannot find inactive game objects.
            // (Modify hierarchy to have an independent main pivot)
            camMov.CenterCamera(GameObject.FindGameObjectWithTag("Origin").transform);

            user.ZoomOut();

            foreach (var bone in boneType)
            {
                bone.SetActive(activate);
            }
        }

        // Toggle between activating or deactivating long bones
        public void FilterLong()
        {
            activateLong = !activateLong; 
            FilterByType(longBones, activateLong);
        }

        // Toggle between activating or deactivating short bones
        public void FilterShort()
        {
            activateShort = !activateShort;
            FilterByType(shortBones, activateShort);
        }

        // Toggle between activating or deactivating flat bones
        public void FilterFlat()
        {
            activateFlat = !activateFlat;
            FilterByType(flatBones, activateFlat);
        }

        // Toggle between activating or deactivating irregular bones
        public void FilterIrregular()
        {
            activateIrr = !activateIrr;
            FilterByType(irrBones, activateIrr);
        }
    }
}
