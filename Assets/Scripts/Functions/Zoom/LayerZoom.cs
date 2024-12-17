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

    List<ZoomControl> universalControl = new List<ZoomControl>();
    List<ZoomControl> layerZeroControl = new List<ZoomControl>();
    List<ZoomControl> layerOneControl = new List<ZoomControl>();
    List<ZoomControl> layerTwoControl = new List<ZoomControl>();
    List<ZoomControl> layerThreeControl = new List<ZoomControl>();
    List<ZoomControl> layerFourControl = new List<ZoomControl>();
    List<ZoomControl> layerFiveControl = new List<ZoomControl>();
    List<ZoomControl> layerSixControl = new List<ZoomControl>();
    List<ZoomControl> layerSevenControl = new List<ZoomControl>();
    List<ZoomControl> layerEightControl = new List<ZoomControl>();
    List<ZoomControl> layerNineControl = new List<ZoomControl>();
    List<ZoomControl> layerTenControl = new List<ZoomControl>();

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
        print(layerIndex);

        ResetLayers();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddToLayer(ZoomControl control, int layerIndex)
    {
        universalControl.Add(control);
        switch (layerIndex)
        {
            case 0:
                layerZeroControl.Add(control);

                layerZeroRend.Add(control.rend);
                layerZeroCol.Add(control.col);
                layerZeroLight.Add(control.light);
                break;
            case 1:
                layerOneControl.Add(control);

                layerOneRend.Add(control.rend);
                layerOneCol.Add(control.col); 
                layerOneLight.Add(control.light);
                break;
            case 2:
                layerTwoControl.Add(control);

                layerTwoRend.Add(control.rend);
                layerTwoCol.Add(control.col);
                layerTwoLight.Add(control.light);
                break;
            case 3:
                layerThreeControl.Add(control);

                layerThreeRend.Add(control.rend);
                layerThreeCol.Add(control.col);
                layerThreeLight.Add(control.light);
                break;
            case 4:
                layerFourControl.Add(control);

                layerFourRend.Add(control.rend);
                layerFourCol.Add(control.col);
                layerFourLight.Add(control.light);
                break;
            case 5:
                layerFiveControl.Add(control);

                layerFiveRend.Add(control.rend);
                layerFiveCol.Add(control.col);
                layerFiveLight.Add(control.light);
                break;
            case 6:
                layerSixControl.Add(control);

                layerSixRend.Add(control.rend);
                layerSixCol.Add(control.col);
                layerSixLight.Add(control.light);
                break;
            case 7:
                layerSevenControl.Add(control);
                
                layerSevenRend.Add(control.rend);
                layerSevenCol.Add(control.col);
                layerSevenLight.Add(control.light);
                break;
            case 8:
                layerEightControl.Add(control);

                layerEightRend.Add(control.rend);
                layerEightCol.Add(control.col);
                layerEightLight.Add(control.light);
                break;
            case 9:
                layerNineControl.Add(control);

                layerNineRend.Add(control.rend);
                layerNineCol.Add(control.col);
                layerNineLight.Add(control.light);
                break;
            case 10:
                layerTenControl.Add(control);

                layerTenRend.Add(control.rend);
                layerTenCol.Add(control.col);
                layerTenLight.Add(control.light);
                break;
        }
    }
    void EnableLayer(List<ZoomControl> controls, bool enable)
    {
        foreach(ZoomControl c in controls)
        {
            c.Enable(enable);
        }
    }
    void ResetLayers()
    {
        EnableLayer(layerZeroControl, true);

        switch (layerIndex)
        {
            case 0:
                break;
            case 1:
                EnableLayer(layerOneControl, false);
                break;
            case 2:
                EnableLayer(layerOneControl, false);
                EnableLayer(layerTwoControl, false);
                break;
            case 3:
                EnableLayer(layerOneControl, false);
                EnableLayer(layerTwoControl, false);
                EnableLayer(layerThreeControl, false);
                break;
            case 4:
                EnableLayer(layerOneControl, false);
                EnableLayer(layerTwoControl, false);
                EnableLayer(layerThreeControl, false);
                EnableLayer(layerFourControl, false);
                break;
            case 5:
                EnableLayer(layerOneControl, false);
                EnableLayer(layerTwoControl, false);
                EnableLayer(layerThreeControl, false);
                EnableLayer(layerFourControl, false);
                EnableLayer(layerFiveControl, false);
                break;
            case 6:
                EnableLayer(layerOneControl, false);
                EnableLayer(layerTwoControl, false);
                EnableLayer(layerThreeControl, false);
                EnableLayer(layerFourControl, false);
                EnableLayer(layerFiveControl, false);
                EnableLayer(layerSixControl, false);
                break;
            case 7:
                EnableLayer(layerOneControl, false);
                EnableLayer(layerTwoControl, false);
                EnableLayer(layerThreeControl, false);
                EnableLayer(layerFourControl, false);
                EnableLayer(layerFiveControl, false);
                EnableLayer(layerSixControl, false);
                EnableLayer(layerSevenControl, false);
                break;  
            case 8:
                EnableLayer(layerOneControl, false);
                EnableLayer(layerTwoControl, false);
                EnableLayer(layerThreeControl, false);
                EnableLayer(layerFourControl, false);
                EnableLayer(layerFiveControl, false);
                EnableLayer(layerSixControl, false);
                EnableLayer(layerSevenControl, false);
                EnableLayer(layerEightControl, false);
                break;
            case 9:
                EnableLayer(layerOneControl, false);
                EnableLayer(layerTwoControl, false);
                EnableLayer(layerThreeControl, false);
                EnableLayer(layerFourControl, false);
                EnableLayer(layerFiveControl, false);
                EnableLayer(layerSixControl, false);
                EnableLayer(layerSevenControl, false);
                EnableLayer(layerEightControl, false);
                EnableLayer(layerNineControl, false);
                break;
            case 10:
                EnableLayer(layerOneControl, false);
                EnableLayer(layerTwoControl, false);
                EnableLayer(layerThreeControl, false);
                EnableLayer(layerFourControl, false);
                EnableLayer(layerFiveControl, false);
                EnableLayer(layerSixControl, false);
                EnableLayer(layerSevenControl, false);
                EnableLayer(layerEightControl, false);
                EnableLayer(layerNineControl, false);
                EnableLayer(layerTenControl, false);
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

    List<Transform> TryGetChildren(Transform obj)
    {
        children.Clear();

        if (obj.childCount > 1)
        {
            foreach (Transform child in obj)
            {
                if (!child.CompareTag("Pivot"))
                {
                    child.GetComponent<ZoomControl>().layerIndex = layerIndex;

                    children.Add(child);
                }
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
        GetComponent<ZoomControl>().layerIndex = layerIndex;

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
