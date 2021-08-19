using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction_AnimChange : MonoBehaviour
{
    [SerializeField] private Animator masterAnim;
    [SerializeField] private string animParameterID;
    [SerializeField] private int animIntID;

    public void React()
    {
        masterAnim.SetInteger(animParameterID, animIntID);
    }

}
