using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction_Lightning : MonoBehaviour
{
    

    [SerializeField] Animator anim;



    public void React() //When Player reacts, an animation will happen
    {
        anim.Play("play");
    }
}
