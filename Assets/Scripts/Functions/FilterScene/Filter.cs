using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BodySystem
{
    public class Filter : MonoBehaviour
    {
        List<Category> boneComponents = new List<Category>();

        CameraMovement camMov;
        V1User user;

        [SerializeField] bool activateLong = true;
        [SerializeField] bool activateShort = true;
        [SerializeField] bool activateFlat = true;
        [SerializeField] bool activateIrr = true;

        //Changed Game object lists to Zoom types because of Zoom's Enable function which
        //should be less expensive than deactivating the gameobject itselft and runs
        //all the calculations for deactivating it natively

        List<Zoom> longBones = new List<Zoom>();
        List<Zoom> shortBones = new List<Zoom>();
        List<Zoom> flatBones = new List<Zoom>();
        List<Zoom> irrBones = new List<Zoom>();
        // Awake is called when loading scene
        void Awake()
        {
            boneComponents.AddRange(FindObjectsOfType<Category>());

            camMov = FindObjectOfType<CameraMovement>();
            user = FindObjectOfType<V1User>();

            longBones.AddRange(GetBoneType("long"));
            shortBones.AddRange(GetBoneType("short"));
            flatBones.AddRange(GetBoneType("flat"));
            irrBones.AddRange(GetBoneType("irregular"));
        }

        // Use LINQ querys to get to search through a list of all object's component script
        // Then select and return game objects list whose type of bone is same as input field
        List<Zoom> GetBoneType(string type)
        {
            type = type.ToLower();

            var filteredBones = boneComponents
                                .Where(b => b.boneType.ToString().ToLower() == type)
                                .Select(b => b.GetComponent<Zoom>());

            return filteredBones.ToList();
        }

        // Center Camera to main pivot in scene and 
        // Activate/Deactivate objects in input list
        void FilterByType(List<Zoom> boneType, bool activate)
        {
            // Center camera around main scene pivot 
            // [Note] could probably implement so that it center first object in list of non-filtered bones
            camMov.CenterCamera(GameObject.FindGameObjectWithTag("Origin").transform);

            user.ZoomOut();

            foreach (var bone in boneType)
            {
                bone.Enable(activate);
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
        public int CheckListCount(string type)
        {
            type = type.ToLower();
            switch (type)
            {
                case "long":
                    return longBones.Count;

                case "short":
                    return shortBones.Count;

                case "flat":
                    return flatBones.Count;

                case "irregular":
                    return irrBones.Count;

                default:
                    print("Type not found");
                    return -1;
            }
        }
    }
}
