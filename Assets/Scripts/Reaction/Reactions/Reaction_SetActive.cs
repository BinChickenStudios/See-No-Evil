using UnityEngine;

public class Reaction_SetActive : MonoBehaviour
{
    //the objects to set active/inactive
    public GameObject[] objects;

    //the active/inactive value
    public bool setValue;

    //override the reaction command to add light effect
    public void React()
    {
        //loop through every object
        for (int i = 0; i < objects.Length; i++)
        {
            //set the object to the setValue (true or false)
            objects[i].SetActive(setValue);
        }
    }
}
