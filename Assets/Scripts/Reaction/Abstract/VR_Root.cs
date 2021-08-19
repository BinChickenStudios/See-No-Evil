using System.Collections.Generic;
using UnityEngine;

public abstract class VR_Root : MonoBehaviour
{
    //the boolean that VR_Reaction checks for calling a reaction (inheriters change this to there requirement)
   [HideInInspector] public bool rootRequirement;
    //a list of reactive objects that are using this root for definitions/properties
   [HideInInspector] private List<VR_Reaction> baseReact = new List<VR_Reaction>();

    //a function which adds a base reactor to the list
    public void AddBaseReact(VR_Reaction newBaseReact)
    {
        //add a new base reactor to the base react list
        baseReact.Add(newBaseReact);
    }

    //a function that talks to each base Reactor and calls to checkReact()
    public void RequestReaction()
    {
        //for every base reactor
        for (int i = 0; i < baseReact.Count; i++)
        {
            //call the checkreact function
            baseReact[i].CheckReact();
        }
    }

}
