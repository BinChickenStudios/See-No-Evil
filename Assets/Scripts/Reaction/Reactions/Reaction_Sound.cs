using UnityEngine;

public class Reaction_Sound : MonoBehaviour
{
    //sound information
    [SerializeField] private Sound sound = null;

    //where to spawn the object
    [SerializeField] private GameObject soundObj;

    //override the reaction command to add light effect
    public void play()
    {
        //play the sound effect on the desired object
        AudioManager.instance.Play(sound, soundObj);
    }
    public void stop()
    {
        //stop the sound effect on the sound object
        AudioManager.instance.Stop(soundObj);
    }
}
