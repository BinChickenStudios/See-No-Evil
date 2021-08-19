using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction_Phenakistiscope : MonoBehaviour
{
    [SerializeField] Transform discTF;
    public void React()
    {
        StartCoroutine(Turning());
    }


    //Primitive Rotate Line
    IEnumerator Turning()
    {
        while (true)
        {
            discTF.Rotate(0, 0, 22.5f);

            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
