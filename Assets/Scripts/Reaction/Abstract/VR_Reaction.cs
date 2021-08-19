using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VR_Reaction : MonoBehaviour
{
    //is the reaction reversable
    [SerializeField] private bool reverseable;

    //core functions (edited by inheriters)
    #region Core

    //during the start of the game
    private void Start()
    {
        //incorperate the roots
        AddRoots();
    }

    //the function that will be overrided via other scripts to redefine it
    [SerializeField] private UnityEvent React = new UnityEvent();
    //the function that will be overrided via other scripts to redefine it (unreact)
    [SerializeField] private UnityEvent Unreact = new UnityEvent();

    //the funtion that inherited scripts will call to react/unreact
    public void CheckReact()
    {

        //check the requirements (if true... react)
        if (CheckRequirements()) React.Invoke();
        //if its not reversable end here
        else if (!reverseable) return;
        //unreact
        else Unreact.Invoke();
        
    }

    #endregion

    //functionality for requirements
    #region roots/requirements

    //a list of the root functionality scripts
    [SerializeField] protected List<VR_Root> roots = new List<VR_Root>();

    //the function that adds roots to objects
    private void AddRoots()
    {
        //for every root
        for (int i = 0; i < roots.Count; i++)
        {
            //add its root requirement to the requirement list
            Requirements(roots[i].rootRequirement);
            //add this reactive object to the base react script
            roots[i].AddBaseReact(this);
        }
    }

    //updates the requirements using the roots
    private void UpdateRequirements()
    {

        //if the root and requirements are not even/equal
        if(roots.Count != requirements.Count)
        {
            //log the error (they should be even/equal or the booleans will go haywire)
            Debug.LogError(gameObject.name +" roots are not the same as the requirements, requirements cannot be updated");
            //end the loop here
            return; 
        }

        //for each root
        for (int i = 0; i < roots.Count; i++)
        {
            //send a message to CheckProgression (temporary?) used by getprogression and limiter
            roots[i].SendMessage("CheckToReact", SendMessageOptions.DontRequireReceiver);
            //update the value to the roots value
            requirements[i] = roots[i].rootRequirement;
        }
    }

    //the list of requirements (for checking)
    [HideInInspector] protected List<bool> requirements = new List<bool>();

    //adds a new requirement to the list of requirements
    private void Requirements(bool newRequirement)
    {
        //add the new requirement to the list
        requirements.Add(newRequirement);
    }

    //checks to see if all the requirements are met (returning a boolean)
    private bool CheckRequirements()
    {
        //update the requirement values 
        UpdateRequirements();

        //for every requirement 
        for (int i = 0; i < requirements.Count; i++)
        {
            //if the requirement is false
            if (!requirements[i])
            {
                //return false (end the script here)
                return false;
            }
        }

        //return true (only occurs if all requirements returned true)
        return true;
    }

    #endregion
}
