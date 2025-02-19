using System;
using System.Collections;
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

        public List<Zoom> longBones = new List<Zoom>();
        public List<Zoom> shortBones = new List<Zoom>();
        public List<Zoom> flatBones = new List<Zoom>();
        public List<Zoom> irrBones = new List<Zoom>();
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
        IEnumerator FilterByType(List<Zoom> boneType, bool activate)
        {
            // Center camera around main scene pivot 
            //  [Note] could probably implement so that it centers
            //  to first object in list of non-filtered bones
            camMov.CenterCamera(GameObject.FindGameObjectWithTag("Origin").transform);

            if (user.isZoomedIn)
            {
                user.ZoomOut();
            }

            //Errors happen when object is isolated and user tries to filter bones due to isolating
            //modifying the bone's state, resulting in nothing happening. This line prevents that
            //by returning everything to its original, unzoomed, unisolated state
            if (user.isIsolated)
            {
                user.IsolateSelected();
            }

            yield return new WaitForEndOfFrame();

            foreach (var bone in boneType)
            {
                bone.Enable(activate);
            }
        }

        // Toggle between activating or deactivating long bones
        public void FilterLong()
        {
            activateLong = !activateLong; 
            StartCoroutine(FilterByType(longBones, activateLong));
        }

        // Toggle between activating or deactivating short bones
        public void FilterShort()
        {
            activateShort = !activateShort;
            StartCoroutine(FilterByType(shortBones, activateShort));
        }

        // Toggle between activating or deactivating flat bones
        public void FilterFlat()
        {
            activateFlat = !activateFlat;
            StartCoroutine(FilterByType(flatBones, activateFlat));
        }

        // Toggle between activating or deactivating irregular bones
        public void FilterIrregular()
        {
            activateIrr = !activateIrr;
            StartCoroutine(FilterByType(irrBones, activateIrr));
        }

        // Used to check whether activating/deactivating function to filter bone types
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
