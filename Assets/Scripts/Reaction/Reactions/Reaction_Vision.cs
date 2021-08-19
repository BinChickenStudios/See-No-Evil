using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction_Vision : MonoBehaviour
{

    public void React()
    {
        //enable the VR Vision 
        VR_Vision.instance.EnableVision();
    }

    public void UnReact()
    {
        //disable the VR Vision 
        VR_Vision.instance.DisableVision();
    }
}
