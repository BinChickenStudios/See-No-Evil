using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VR_Eye : MonoBehaviour
{

    //the hand that will cover the eye
    [SerializeField] private Transform m_Hand = null;

    //when something enters the collision of the eye
    private void OnTriggerEnter(Collider other)
    {
        //if the thing colliding with the eye is not the required object (Desired hand)
        if (other.transform != m_Hand)
        {
            //end here
            return;
        }

        //set the eye closed bool to true
        //m_EyeClosed = true;

        //enable the VR Vision 
        VR_Vision.instance.EnableVision();
    }

    //when something enters the collision of the eye
    private void OnTriggerExit(Collider other)
    {
        //if the thing colliding with the eye is not the required object (Desired hand)
        if (other.transform != m_Hand)
        {
            //end here
            return;
        }

        //set the eye closed bool to true
        //m_EyeClosed = false;

        //disable the VR Vision 
        VR_Vision.instance.DisableVision();
    }

}
