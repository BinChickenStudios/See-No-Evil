using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Progression : MonoBehaviour
{
    //a singleton for progression 
    public static VR_Progression instance;

    //before the game starts
    private void Awake()
    {
        //if there is an instance already  
        if (instance)
        {
            //destroy this object
            Destroy(this);
            //break out of the loop
            return;
        }

        //set the instance as this
        instance = this;
    }

    //contains a list of words that signify progression
    public List<string> progression = new List<string>();


    //a function which adds a progress string
    public void AddProgression(string progress)
    {
        //add the string to the progression list
        progression.Add(progress);
    }
}
