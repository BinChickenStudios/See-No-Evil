using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VR_Vision : MonoBehaviour
{
    #region singleton

    //creates a accessible reference to all scripts
    public static VR_Vision instance;

    //before the game starts (via loading)
    private void Awake()
    {
        //if there was already a VR_Vision in the scene ... destroy this version
        if (instance) Destroy(this);
        //set the current instance of VR_Vision to this object
        else instance = this;
        //dont destroy (on a scene change)
        //DontDestroyOnLoad(this);
    }

    #endregion

    #region variables

    //a publicly accessible boolean that returns its value but can only be set via this script
    [HideInInspector] public bool Vision { get; private set; }

    //an Action event that calls when the vision is enabled (allows other scripts to react to the event)
    public event Action onVisionEnabled;
    //an Action event that calls when the vision is Disabled (allows other scripts to react to the event)
    public event Action onVisionDisabled;

    #endregion

    #region EventCalls

    //the function for enabling the vision (currently public but maybe private?)
    public void EnableVision()
    {
        //set the vision value to true
        Vision = true;
        //if the on vision enabled event exists (if something is currently subscribed to it), invoke it (activate it)
        onVisionEnabled?.Invoke();
    }

    //the function for disabling the vision (currently public but maybe private?)
    public void DisableVision()
    {
        //set the vision value to false
        Vision = false;
        //if the on vision disabled event exists (if something is currently subscribed to it) , invoke it (activate it)
        onVisionDisabled?.Invoke();
    }

    #endregion

}
