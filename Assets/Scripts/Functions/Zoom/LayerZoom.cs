using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerZoom : MonoBehaviour
{
    int layerIndex = 0;
    int maxLayers = 0;

    List<Transform> children = new List<Transform>();
    ZoomControl zom;

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
        ManageChildren(this.transform);

        print(layerTwo.Count());
        print(layerIndex);

        ResetLayers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetLayers()
    {
        ZoomManagement.EnableParent(true, layerZeroCol, layerZeroRend);

        switch (layerIndex)
        {
            case 0:
                break;
            case 1:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                break;
            case 2:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                ZoomManagement.EnableChildren(false, layerTwoLight, layerTwoCol, layerTwoRend);
                break;
            case 3:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                ZoomManagement.EnableChildren(false, layerTwoLight, layerTwoCol, layerTwoRend);
                ZoomManagement.EnableChildren(false, layerThreeLight, layerThreeCol, layerThreeRend);
                break;
            case 4:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                ZoomManagement.EnableChildren(false, layerTwoLight, layerTwoCol, layerTwoRend);
                ZoomManagement.EnableChildren(false, layerThreeLight, layerThreeCol, layerThreeRend);
                ZoomManagement.EnableChildren(false, layerFourLight, layerFourCol, layerFourRend);
                break;
            case 5:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                ZoomManagement.EnableChildren(false, layerTwoLight, layerTwoCol, layerTwoRend);
                ZoomManagement.EnableChildren(false, layerThreeLight, layerThreeCol, layerThreeRend);
                ZoomManagement.EnableChildren(false, layerFourLight, layerFourCol, layerFourRend);
                ZoomManagement.EnableChildren(false, layerFiveLight, layerFiveCol, layerFiveRend);
                break;
            case 6:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                ZoomManagement.EnableChildren(false, layerTwoLight, layerTwoCol, layerTwoRend);
                ZoomManagement.EnableChildren(false, layerThreeLight, layerThreeCol, layerThreeRend);
                ZoomManagement.EnableChildren(false, layerFourLight, layerFourCol, layerFourRend);
                ZoomManagement.EnableChildren(false, layerFiveLight, layerFiveCol, layerFiveRend);
                ZoomManagement.EnableChildren(false, layerSixLight, layerSixCol, layerSixRend);
                break;
            case 7:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                ZoomManagement.EnableChildren(false, layerTwoLight, layerTwoCol, layerTwoRend);
                ZoomManagement.EnableChildren(false, layerThreeLight, layerThreeCol, layerThreeRend);
                ZoomManagement.EnableChildren(false, layerFourLight, layerFourCol, layerFourRend);
                ZoomManagement.EnableChildren(false, layerFiveLight, layerFiveCol, layerFiveRend);
                ZoomManagement.EnableChildren(false, layerSixLight, layerSixCol, layerSixRend);
                ZoomManagement.EnableChildren(false, layerSevenLight, layerSevenCol, layerSevenRend);
                break;
            case 8:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                ZoomManagement.EnableChildren(false, layerTwoLight, layerTwoCol, layerTwoRend);
                ZoomManagement.EnableChildren(false, layerThreeLight, layerThreeCol, layerThreeRend);
                ZoomManagement.EnableChildren(false, layerFourLight, layerFourCol, layerFourRend);
                ZoomManagement.EnableChildren(false, layerFiveLight, layerFiveCol, layerFiveRend);
                ZoomManagement.EnableChildren(false, layerSixLight, layerSixCol, layerSixRend);
                ZoomManagement.EnableChildren(false, layerSevenLight, layerSevenCol, layerSevenRend);
                ZoomManagement.EnableChildren(false, layerEightLight, layerEightCol, layerEightRend);
                break;
            case 9:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                ZoomManagement.EnableChildren(false, layerTwoLight, layerTwoCol, layerTwoRend);
                ZoomManagement.EnableChildren(false, layerThreeLight, layerThreeCol, layerThreeRend);
                ZoomManagement.EnableChildren(false, layerFourLight, layerFourCol, layerFourRend);
                ZoomManagement.EnableChildren(false, layerFiveLight, layerFiveCol, layerFiveRend);
                ZoomManagement.EnableChildren(false, layerSixLight, layerSixCol, layerSixRend);
                ZoomManagement.EnableChildren(false, layerSevenLight, layerSevenCol, layerSevenRend);
                ZoomManagement.EnableChildren(false, layerEightLight, layerEightCol, layerEightRend);
                ZoomManagement.EnableChildren(false, layerNineLight, layerNineCol, layerNineRend);
                break;
            case 10:
                ZoomManagement.EnableChildren(false, layerOneLight, layerOneCol, layerOneRend);
                ZoomManagement.EnableChildren(false, layerTwoLight, layerTwoCol, layerTwoRend);
                ZoomManagement.EnableChildren(false, layerThreeLight, layerThreeCol, layerThreeRend);
                ZoomManagement.EnableChildren(false, layerFourLight, layerFourCol, layerFourRend);
                ZoomManagement.EnableChildren(false, layerFiveLight, layerFiveCol, layerFiveRend);
                ZoomManagement.EnableChildren(false, layerSixLight, layerSixCol, layerSixRend);
                ZoomManagement.EnableChildren(false, layerSevenLight, layerSevenCol, layerSevenRend);
                ZoomManagement.EnableChildren(false, layerEightLight, layerEightCol, layerEightRend);
                ZoomManagement.EnableChildren(false, layerNineLight, layerNineCol, layerNineRend);
                ZoomManagement.EnableChildren(false, layerTenLight, layerTenCol, layerTenRend);
                break;
        }
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

    void GetComponents(Transform obj, int target)
    {
        zom = obj.GetComponent<ZoomControl>();

        switch (target)
        {
            case 0:
                layerZeroRend.Add(zom.rend);
                layerZeroCol.Add(zom.col);
                layerZeroLight.Add(zom.light);
                break;

            case 1:
                layerOneRend.Add(zom.rend);
                layerOneCol.Add(zom.col);
                layerOneLight.Add(zom.light);
                break;

            case 2:
                layerTwoRend.Add(zom.rend);
                layerTwoCol.Add(zom.col);
                layerTwoLight.Add(zom.light);
                break;

            case 3:
                layerThreeRend.Add(zom.rend);
                layerThreeCol.Add(zom.col);
                layerThreeLight.Add(zom.light);
                break;

            case 4:
                layerFourRend.Add(zom.rend);
                layerFourCol.Add(zom.col);
                layerFourLight.Add(zom.light);
                break;

            case 5:
                layerFiveRend.Add(zom.rend);
                layerFiveCol.Add(zom.col);
                layerFiveLight.Add(zom.light);
                break;

            case 6:
                layerSixRend.Add(zom.rend);
                layerSixCol.Add(zom.col);
                layerSixLight.Add(zom.light);
                break;

            case 7:
                
                layerSevenRend.Add(zom.rend);
                layerSevenCol.Add(zom.col);
                layerSevenLight.Add(zom.light);
                break;

            case 8:
                layerEightRend.Add(zom.rend);
                layerEightCol.Add(zom.col);
                layerEightLight.Add(zom.light);
                break;

            case 9:
                layerNineRend.Add(zom.rend);
                layerNineCol.Add(zom.col);
                layerNineLight.Add(zom.light);
                break;

            case 10:
                layerTenRend.Add(zom.rend);
                layerTenCol.Add(zom.col);
                layerTenLight.Add(zom.light);
                break;
        }
    }

    List<Transform> TryGetChildren(Transform obj)
    {
        children.Clear();

        foreach(Transform child in obj)
        {
            if(child.CompareTag("Bone") || child.CompareTag("DerivedBone"))
            {
                child.GetComponent<ZoomControl>().layerIndex = layerIndex;

                GetComponents(child, layerIndex);

                children.Add(child);
            }
        }
        //Checks if object passed has a bone children
        //If not, it decreases layer index to make it correspond with layers active.
        if(children.Count == 0)
        {
            layerIndex--;
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

    //Currently has reached the level of a forbidden, pc chrashing, while loop

    //Somewhere along the hierarchy,
    //if the object with the most children is not at the bottom, it crashes
    void ManageChildren(Transform obj)
    {
        layerZero.Add(obj);
        GetComponents(obj, layerIndex);

            layerIndex = 1;
            layerOne.AddRange(TryGetChildren(obj));

            foreach (Transform child in layerOne)
            {
                    layerIndex = 2;
                    layerTwo.AddRange(TryGetChildren(child));

                    foreach (Transform grandchild in layerTwo)
                    {
                            layerIndex = 3;
                            layerThree.AddRange(TryGetChildren(grandchild));

                            foreach (Transform greatGrandchild in layerThree)
                            {
                                    layerIndex = 4;
                                    layerFour.AddRange(TryGetChildren(greatGrandchild));

                                    foreach (Transform obj4 in layerFour)
                                    {
                                            layerIndex = 5;
                                            layerFive.AddRange(TryGetChildren(obj4));

                                            foreach (Transform obj5 in layerFive)
                                            {
                                                    layerIndex = 6;
                                                    layerSix.AddRange(TryGetChildren(obj5));

                                                    foreach (Transform obj6 in layerSix)
                                                    {
                                                            layerIndex = 7;
                                                            layerSeven.AddRange(TryGetChildren(obj6));

                                                            foreach (Transform obj7 in layerSeven)
                                                            {
                                                                    layerIndex = 8;
                                                                    layerEight.AddRange(TryGetChildren(obj7));

                                                                    foreach (Transform obj8 in layerEight)
                                                                    {
                                                                            layerIndex = 9;
                                                                            layerNine.AddRange(TryGetChildren(obj8));

                                                                            foreach (Transform obj9 in layerNine)
                                                                            {
                                                                                    layerIndex = 10;
                                                                                    layerTen.AddRange(TryGetChildren(obj9));
                                                                                
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
