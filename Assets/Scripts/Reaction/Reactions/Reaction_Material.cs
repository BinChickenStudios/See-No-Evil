using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction_Material : MonoBehaviour
{
    //the unique material to change the object to
    [SerializeField] private Material newMaterial = null;
    //the original materials of each mesh renderer
    [HideInInspector] private List<Material> originalMat = new List<Material>();
    //all mesh renderers that will be affected by the material swap
    [SerializeField] private List<MeshRenderer> rends = new List<MeshRenderer>();
    
    //at the start of the game
    protected void Start()
    {

        //for every renderer
        for (int i = 0; i < rends.Count; i++)
        {
            //store there original materials
            originalMat.Add(rends[i].material);
        }
    }

    //override react (change mat)
    public void React()
    {
        //change the mat
        ChangeMat(true);
    }

    //react (checks for alternative version)
    public void UnReact()
    {
        //change the material to false
        ChangeMat(false);
    }

    //change the material to another material
    private void ChangeMat(bool Reaction)
    {
        //if its the reaction (if it should swap it to a new material type)
        if (Reaction)
        {
            //for every renderer in the renderer list
            for (int i = 0; i < rends.Count; i++)
            {
                //change its material to the new material
                rends[i].material = newMaterial;
            }        
        }
        //if its an unreaction (if it should swap the material back to its original)
        else
        {
            //for every renderer in the renderer list
            for (int i = 0; i < rends.Count; i++)
            {
                //change its material to its original material
                rends[i].material = originalMat[i];
            }     
        }
    }


}
