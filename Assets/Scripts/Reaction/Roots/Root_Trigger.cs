using System.Collections.Generic;
using UnityEngine;

public class Root_Trigger : VR_Root
{
    //the tag to trigger a response
    [SerializeField] private string triggerTag = "Player";

    //the colliders for checking (not used yet)
    [SerializeField] private List<Collider> colliders = new List<Collider>();

    //checks for objects that enter collision (trigger)
    private void OnTriggerEnter(Collider other)
    {
        //if the trigger tag is not true or the trigger was already commited
        if (!other.CompareTag(triggerTag))
        {
            //end here
            return;
        }

        //set rootRequirement to true
        rootRequirement = true;

        //request a reaction
        RequestReaction();

    }

    //check for objectsthat exit collision (for inheritance) ... if trigger gets reset after leaving
    private void OnTriggerExit(Collider other)
    {
        //if the trigger tag is not true or the trigger was already commited
        if (!other.CompareTag(triggerTag))
        {
            //end here
            return;
        }

        //set root requirement to false
        rootRequirement = false;

        //request a reaction
        RequestReaction();
    }
}
