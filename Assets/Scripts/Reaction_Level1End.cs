using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction_Level1End : MonoBehaviour
{

    [SerializeField] private Animator masterAnim_1;
    [SerializeField] private string animParameterID_1;
    [SerializeField] private int animIntID_1;

    [SerializeField] private Animator masterAnim_2;
    [SerializeField] private string animParameterID_2;
    [SerializeField] private int animIntID_2;


    [SerializeField] private Animator masterAnim_3;
    [SerializeField] private string animParameterID_3;
    [SerializeField] private int animIntID_3;



    public void React()
    {
        masterAnim_1.SetInteger(animParameterID_1, animIntID_1);

        masterAnim_2.SetInteger(animParameterID_2, animIntID_2);

        masterAnim_3.SetInteger(animParameterID_3, animIntID_3);

        
    }
}
