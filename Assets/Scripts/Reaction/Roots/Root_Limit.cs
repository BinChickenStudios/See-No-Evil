using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root_Limit : VR_Root
{
    //a list of all roots linked to this limiter
    [SerializeField] private List<VR_Root> roots = new List<VR_Root>();

    //the amount of times a limit before reactions no longer occur (default to 1)
    [SerializeField] private int limitAmount = 1;
    //limits the reaction when true 
    private bool limited = false;

    //a function which checks whether the limit is set
    public void CheckToReact()
    {
        //if already limited return
        if (limited)
        {
            //end the loop here
            return;
        }
        //decrease the limit amount of this root
        limitAmount--;
        //change the root requirement whatever limited is not (if limit is false, return true)
        rootRequirement = !limited;
        //request reaction
        RequestReaction();
        //if the limit amount has reached 0
        if (limitAmount > 0)
        {
            //set limited to true
            limited = true;
            //disable all roots linked to this limiter
            DisableRoots();
            //end here
            return;
        }
    }

    //Disables all the Roots limited by the limiter
    private void DisableRoots()
    {
        //for each root
        for (int i = 0; i < roots.Count; i++)
        {
            //set it to disabled
            roots[i].enabled = false;
        }

        //set this root to disabled
        enabled = false;
    }
}
