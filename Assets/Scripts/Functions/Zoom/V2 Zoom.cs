using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class V2Zoom : MonoBehaviour
{
    int layersActive = 0;

    List<Transform> layerZero = new List<Transform>();
    List<Transform> layerOne = new List<Transform>();
    List<Transform> layerTwo = new List<Transform>();
    List<Transform> layerThree = new List<Transform>();
    List<Transform> layerFour = new List<Transform>();
    List<Transform> layerFive = new List<Transform>();
    List<Transform> layerSix = new List<Transform>();
    List<Transform> layerSeven = new List<Transform>();
    List<Transform> layerEight = new List<Transform>();
    List<Transform> layerNine = new List<Transform>();
    List<Transform> layerTen = new List<Transform>();

    List<Renderer> layerZeroRend = new List<Renderer>();
    List<Renderer> layerOneRend = new List<Renderer>();
    List<Renderer> layerTwoRend = new List<Renderer>();
    List<Renderer> layerThreeRend = new List<Renderer>();
    List<Renderer> layerFourRend = new List<Renderer>();
    List<Renderer> layerFiveRend = new List<Renderer>();
    List<Renderer> layerSixRend = new List<Renderer>();
    List<Renderer> layerSevenRend = new List<Renderer>();
    List<Renderer> layerEightRend = new List<Renderer>();
    List<Renderer> layerNineRend = new List<Renderer>();
    List<Renderer> layerTenRend = new List<Renderer>();

    List<Collider> layerZeroCol = new List<Collider>();
    List<Collider> layerOneCol = new List<Collider>();
    List<Collider> layerTwoCol = new List<Collider>();
    List<Collider> layerThreeCol = new List<Collider>();
    List<Collider> layerFourCol = new List<Collider>();
    List<Collider> layerFiveCol = new List<Collider>();
    List<Collider> layerSixCol = new List<Collider>();
    List<Collider> layerSevenCol = new List<Collider>();
    List<Collider> layerEightCol = new List<Collider>();
    List<Collider> layerNineCol = new List<Collider>();
    List<Collider> layerTenCol = new List<Collider>();

    List<Highlight> layerZeroHigh = new List<Highlight>();
    List<Highlight> layerOneHigh = new List<Highlight>();
    List<Highlight> layerTwoHigh = new List<Highlight>();
    List<Highlight> layerThreeHigh = new List<Highlight>();
    List<Highlight> layerFourHigh = new List<Highlight>();
    List<Highlight> layerFiveHigh = new List<Highlight>();
    List<Highlight> layerSixHigh = new List<Highlight>();
    List<Highlight> layerSevenHigh = new List<Highlight>();
    List<Highlight> layerEightHigh = new List<Highlight>();
    List<Highlight> layerNineHigh = new List<Highlight>();
    List<Highlight> layerTenHigh = new List<Highlight>();

    // Start is called before the first frame update
    void Awake()
    {
        ManageChildren(transform);

        ManageComponents();
        /*derivedRends = derivedBones.Select(b => b.GetComponent<Renderer>()).ToList();
        derivedCols = derivedBones.Select(b => b.GetComponent<Collider>()).ToList();
        derivedComp = derivedBones.Select(b => b.GetComponent<Highlight>()).ToList();*/
    }

    // Update is called once per frame
    void Update()
    {

    }


    bool HasChildren(Transform obj)
    {
        if(obj.childCount > 0)
        {
            return true;
        }
        return false;
    }

    void ManageComponents()
    {
        switch (layersActive)
        {
            case 0:
                GetComponents(0);
                break;

            case 1:
                GetComponents(0);
                GetComponents(1);
                break;

            case 2:
                GetComponents(0);
                GetComponents(1);
                GetComponents(2);
                break;

            case 3:
                GetComponents(0);
                GetComponents(1);
                GetComponents(2);
                GetComponents(3);
                break;

            case 4:
                GetComponents(0);
                GetComponents(1);
                GetComponents(2);
                GetComponents(3);
                GetComponents(4);
                break;

            case 5:
                GetComponents(0);
                GetComponents(1);
                GetComponents(2);
                GetComponents(3);
                GetComponents(4);
                GetComponents(5);
                break;

            case 6:
                GetComponents(0);
                GetComponents(1);
                GetComponents(2);
                GetComponents(3);
                GetComponents(4);
                GetComponents(5);
                GetComponents(6);
                break;

            case 7:
                GetComponents(0);
                GetComponents(1);
                GetComponents(2);
                GetComponents(3);
                GetComponents(4);
                GetComponents(5);
                GetComponents(6);
                GetComponents(7);
                break;

            case 8:
                GetComponents(0);
                GetComponents(1);
                GetComponents(2);
                GetComponents(3);
                GetComponents(4);
                GetComponents(5);
                GetComponents(6);
                GetComponents(7);
                GetComponents(8);
                break;

            case 9:
                GetComponents(0);
                GetComponents(1);
                GetComponents(2);
                GetComponents(3);
                GetComponents(4);
                GetComponents(5);
                GetComponents(6);
                GetComponents(7);
                GetComponents(8);
                GetComponents(9);
                break;

            case 10:
                GetComponents(0);
                GetComponents(1);
                GetComponents(2);
                GetComponents(3);
                GetComponents(4);
                GetComponents(5);
                GetComponents(6);
                GetComponents(7);
                GetComponents(8);
                GetComponents(9);
                GetComponents(10);
                break;
        }
    }

    void GetComponents(int target)
    {
        switch (target)
        {
            case 0:
                layerZeroRend.Add(transform.gameObject.GetComponent<Renderer>());
                layerZeroCol.Add(transform.gameObject.GetComponent<Collider>());
                layerZeroHigh.Add(transform.gameObject.GetComponent<Highlight>());
                break;

            case 1:
                layerOneRend.AddRange(layerOne.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerOneCol.AddRange(layerOne.Select(b => b.gameObject.GetComponent<Collider>()));
                layerOneHigh.AddRange(layerOne.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 2:
                layerTwoRend.AddRange(layerTwo.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerTwoCol.AddRange(layerTwo.Select(b => b.gameObject.GetComponent<Collider>()));
                layerTwoHigh.AddRange(layerTwo.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 3:
                layerThreeRend.AddRange(layerThree.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerThreeCol.AddRange(layerThree.Select(b => b.gameObject.GetComponent<Collider>()));
                layerThreeHigh.AddRange(layerThree.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 4:
                layerFourRend.AddRange(layerFour.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerFourCol.AddRange(layerFour.Select(b => b.gameObject.GetComponent<Collider>()));
                layerFourHigh.AddRange(layerFour.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 5:
                layerFiveRend.AddRange(layerFive.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerFiveCol.AddRange(layerFive.Select(b => b.gameObject.GetComponent<Collider>()));
                layerFiveHigh.AddRange(layerFive.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 6:
                layerSixRend.AddRange(layerSix.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerSixCol.AddRange(layerSix.Select(b => b.gameObject.GetComponent<Collider>()));
                layerSixHigh.AddRange(layerSix.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 7:
                layerSevenRend.AddRange(layerSeven.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerSevenCol.AddRange(layerSeven.Select(b => b.gameObject.GetComponent<Collider>()));
                layerSevenHigh.AddRange(layerSeven.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 8:
                layerEightRend.AddRange(layerEight.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerEightCol.AddRange(layerEight.Select(b => b.gameObject.GetComponent<Collider>()));
                layerEightHigh.AddRange(layerEight.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 9:
                layerNineRend.AddRange(layerNine.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerNineCol.AddRange(layerNine.Select(b => b.gameObject.GetComponent<Collider>()));
                layerNineHigh.AddRange(layerNine.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 10:
                layerTenRend.AddRange(layerTen.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerTenCol.AddRange(layerTen.Select(b => b.gameObject.GetComponent<Collider>()));
                layerTenHigh.AddRange(layerTen.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;
        }
    }
    bool HasObjects(List<Transform> objs)
    {
        if(objs.Count > 0)
        {
            return true;
        }
        return false;
    }
    List<Transform> GetChildren(Transform obj)
    {
        List<Transform> children = new List<Transform>();

        foreach(Transform child in obj)
        {
            if(child.CompareTag("Bone") || child.CompareTag("Derived Bone"))
            {
                children.Add(child);
            }
        }

        return children;
    }

    //Cycles through all children of a game object and
    //adds their transforms into layers depending on
    //hierarchal depth compared to primary object passed
    void ManageChildren(Transform obj)
    {
        layerZero.Add(obj);

        if (HasChildren(obj))
        {
            layerOne.AddRange(GetChildren(obj));
            layersActive++;

            foreach (Transform child in layerOne)
            {
                if (HasChildren(child))
                {
                    layerTwo.AddRange(GetChildren(child));
                    layersActive++;

                    foreach (Transform grandchild in layerTwo)
                    {
                        if (HasChildren(grandchild))
                        {
                            layerThree.AddRange(GetChildren(grandchild));
                            layersActive++;

                            foreach (Transform greatGrandchild in layerThree)
                            {
                                if (HasChildren(greatGrandchild))
                                {
                                    layerFour.AddRange(GetChildren(greatGrandchild));
                                    layersActive++;

                                    foreach (Transform obj4 in layerFour)
                                    {
                                        if (HasChildren(obj4))
                                        {
                                            layerFive.AddRange(GetChildren(obj4));
                                            layersActive++;

                                            foreach (Transform obj5 in layerFive)
                                            {
                                                if (HasChildren(obj5))
                                                {
                                                    layerSix.AddRange(GetChildren(obj5));
                                                    layersActive++;

                                                    foreach (Transform obj6 in layerSix)
                                                    {
                                                        if (HasChildren(obj6))
                                                        {
                                                            layerSeven.AddRange(GetChildren(obj6));
                                                            layersActive++;

                                                            foreach (Transform obj7 in layerSeven)
                                                            {
                                                                if (HasChildren(obj7))
                                                                {
                                                                    layerEight.AddRange(GetChildren(obj7));
                                                                    layersActive++;

                                                                    foreach (Transform obj8 in layerEight)
                                                                    {
                                                                        if (HasChildren(obj8))
                                                                        {
                                                                            layerNine.AddRange(GetChildren(obj8));
                                                                            layersActive++;

                                                                            foreach (Transform obj9 in layerNine)
                                                                            {
                                                                                if (HasChildren(obj9))
                                                                                {
                                                                                    layerTen.AddRange(GetChildren(obj9));
                                                                                    layersActive++;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
