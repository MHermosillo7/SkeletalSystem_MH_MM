using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerZoom : MonoBehaviour
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

    List<Highlight> layerZeroLight = new List<Highlight>();
    List<Highlight> layerOneLight = new List<Highlight>();
    List<Highlight> layerTwoLight = new List<Highlight>();
    List<Highlight> layerThreeLight = new List<Highlight>();
    List<Highlight> layerFourLight = new List<Highlight>();
    List<Highlight> layerFiveLight = new List<Highlight>();
    List<Highlight> layerSixLight = new List<Highlight>();
    List<Highlight> layerSevenLight = new List<Highlight>();
    List<Highlight> layerEightLight = new List<Highlight>();
    List<Highlight> layerNineLight = new List<Highlight>();
    List<Highlight> layerTenLight = new List<Highlight>();

    // Start is called before the first frame update
    void Awake()
    {
        ManageChildren(transform);

        ManageComponents();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ManageLayer(int layerIndex)
    {
        switch (layerIndex)
        {
            case 1:
                ActivateLayer(layerZeroCol, layerZeroRend, layerOneCol, layerOneRend, layerOneLight);
                break;
            case 2:
                ActivateLayer(layerOneCol, layerOneRend, layerTwoCol, layerTwoRend, layerTwoLight);
                break;
            case 3:
                ActivateLayer(layerTwoCol, layerTwoRend, layerThreeCol, layerThreeRend, layerThreeLight);
                break;
            case 4:
                ActivateLayer(layerThreeCol, layerThreeRend, layerFourCol, layerFourRend, layerFourLight);
                break;
            case 5:
                ActivateLayer(layerFourCol, layerFourRend, layerFiveCol, layerFiveRend, layerFiveLight);
                break;
            case 6:
                ActivateLayer(layerFiveCol, layerFiveRend, layerSixCol, layerSixRend, layerSixLight);
                break;
            case 7:
                ActivateLayer(layerSixCol, layerSixRend, layerSevenCol, layerSevenRend, layerSevenLight);
                break;
            case 8:
                ActivateLayer(layerSevenCol, layerSevenRend, layerEightCol, layerEightRend, layerEightLight);
                break;
            case 9:
                ActivateLayer(layerEightCol, layerEightRend, layerNineCol, layerNineRend, layerNineLight);
                break;
            case 10:
                ActivateLayer(layerNineCol, layerNineRend, layerTenCol, layerTenRend, layerTenLight);
                break;
        }
    }
    void ActivateLayer(
        List<Collider> parentCols, List<Renderer> parentRends,
        List<Collider> derivedCols, List<Renderer> derivedRends, List<Highlight> derivedLights)
    {
        ZoomManagement.ZoomOut(parentCols, parentRends, derivedCols, derivedRends, derivedLights);
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
                layerZeroLight.Add(transform.gameObject.GetComponent<Highlight>());
                break;

            case 1:
                layerOneRend.AddRange(layerOne.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerOneCol.AddRange(layerOne.Select(b => b.gameObject.GetComponent<Collider>()));
                layerOneLight.AddRange(layerOne.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 2:
                layerTwoRend.AddRange(layerTwo.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerTwoCol.AddRange(layerTwo.Select(b => b.gameObject.GetComponent<Collider>()));
                layerTwoLight.AddRange(layerTwo.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 3:
                layerThreeRend.AddRange(layerThree.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerThreeCol.AddRange(layerThree.Select(b => b.gameObject.GetComponent<Collider>()));
                layerThreeLight.AddRange(layerThree.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 4:
                layerFourRend.AddRange(layerFour.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerFourCol.AddRange(layerFour.Select(b => b.gameObject.GetComponent<Collider>()));
                layerFourLight.AddRange(layerFour.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 5:
                layerFiveRend.AddRange(layerFive.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerFiveCol.AddRange(layerFive.Select(b => b.gameObject.GetComponent<Collider>()));
                layerFiveLight.AddRange(layerFive.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 6:
                layerSixRend.AddRange(layerSix.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerSixCol.AddRange(layerSix.Select(b => b.gameObject.GetComponent<Collider>()));
                layerSixLight.AddRange(layerSix.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 7:
                layerSevenRend.AddRange(layerSeven.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerSevenCol.AddRange(layerSeven.Select(b => b.gameObject.GetComponent<Collider>()));
                layerSevenLight.AddRange(layerSeven.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 8:
                layerEightRend.AddRange(layerEight.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerEightCol.AddRange(layerEight.Select(b => b.gameObject.GetComponent<Collider>()));
                layerEightLight.AddRange(layerEight.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 9:
                layerNineRend.AddRange(layerNine.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerNineCol.AddRange(layerNine.Select(b => b.gameObject.GetComponent<Collider>()));
                layerNineLight.AddRange(layerNine.Select(b => b.gameObject.GetComponent<Highlight>()));
                break;

            case 10:
                layerTenRend.AddRange(layerTen.Select(b => b.gameObject.GetComponent<Renderer>()));
                layerTenCol.AddRange(layerTen.Select(b => b.gameObject.GetComponent<Collider>()));
                layerTenLight.AddRange(layerTen.Select(b => b.gameObject.GetComponent<Highlight>()));
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
                child.GetComponent<ZoomControl>().layerIndex = layersActive;

                children.Add(child);
            }
        }

        return children;
    }

    // Cycles through all children of a game object and
    // adds their transforms into layers depending on
    // hierarchal depth compared to primary object passed

    // I add one to layersActive before getting children
    // because when adding them to the list, I change their layer level
    // in ZoomControl to equal the amount of layers active.
    // It serves as an index of on what layer the object is.
    void ManageChildren(Transform obj)
    {
        layerZero.Add(obj);

        if (HasChildren(obj))
        {
            layersActive++;
            layerOne.AddRange(GetChildren(obj));

            foreach (Transform child in layerOne)
            {
                if (HasChildren(child))
                {
                    layersActive++;
                    layerTwo.AddRange(GetChildren(child));

                    foreach (Transform grandchild in layerTwo)
                    {
                        if (HasChildren(grandchild))
                        {
                            layersActive++;
                            layerThree.AddRange(GetChildren(grandchild));

                            foreach (Transform greatGrandchild in layerThree)
                            {
                                if (HasChildren(greatGrandchild))
                                {
                                    layersActive++;
                                    layerFour.AddRange(GetChildren(greatGrandchild));

                                    foreach (Transform obj4 in layerFour)
                                    {
                                        if (HasChildren(obj4))
                                        {
                                            layersActive++;
                                            layerFive.AddRange(GetChildren(obj4));

                                            foreach (Transform obj5 in layerFive)
                                            {
                                                if (HasChildren(obj5))
                                                {
                                                    layersActive++;
                                                    layerSix.AddRange(GetChildren(obj5));

                                                    foreach (Transform obj6 in layerSix)
                                                    {
                                                        if (HasChildren(obj6))
                                                        {
                                                            layersActive++;
                                                            layerSeven.AddRange(GetChildren(obj6));

                                                            foreach (Transform obj7 in layerSeven)
                                                            {
                                                                if (HasChildren(obj7))
                                                                {
                                                                    layersActive++;
                                                                    layerEight.AddRange(GetChildren(obj7));

                                                                    foreach (Transform obj8 in layerEight)
                                                                    {
                                                                        if (HasChildren(obj8))
                                                                        {
                                                                            layersActive++;
                                                                            layerNine.AddRange(GetChildren(obj8));

                                                                            foreach (Transform obj9 in layerNine)
                                                                            {
                                                                                if (HasChildren(obj9))
                                                                                {
                                                                                    layersActive++;
                                                                                    layerTen.AddRange(GetChildren(obj9));
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
