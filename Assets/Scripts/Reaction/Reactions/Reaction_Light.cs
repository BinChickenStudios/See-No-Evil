using UnityEngine;

public class Reaction_Light : MonoBehaviour
{
    //an array of lights
    public VR_Light[] lights = null;

    //the color to change the lights
    public Color ColorChange = Color.black;

    //override the reaction command to add light effect
    public void React()
    {
        //for every light in the lights array
        for (int i = 0; i < lights.Length; i++)
        {
            //check to change the color
            ColorChanger(i);
        }
    }

    //checks and changes color
    private void ColorChanger(int light)
    {
        //if the color isnt black (default)
        if (ColorChange != Color.black)
        {
            //change the color to the color change
            lights[light].color = ColorChange;
        }
    }


}
