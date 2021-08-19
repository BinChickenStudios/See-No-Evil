using UnityEngine.Audio;
using UnityEngine;

//viewable in the inspector
[System.Serializable]
public class Sound 
{
    //the name of the sound effect
    public string name = null;

    //the audio source of the sound effect (potentially unused)
    [HideInInspector] public AudioSource source = null;

    //the audio clip of the sound effect
    public AudioClip clip = null;

    //the volume of the sound effect (ranged between 0 and 1)
    [Range(0f,1f)]
    public float volume = .5f;
    //the pitch of the sound effect (ranged between .5f and 3f)
    [Range(.5f,3f)]
    public float pitch = 1f;
    //the doppler amount (determines whether the sound is 3D or 2D) (0 being 2D, 1 being 3D)
    [Range(0f,1f)]
    public float doppler = 1f;

    //the minimum distance for volume to be heard (force)
    [Range(1f,10f)]
    public float minDistance = 1f;
    //the maximum distance for volume to be heard (force)
    [Range(1f, 20f)]
    public float maxDistance = 1f;

    //the minimum amount of volume possible (force)
    [Range(0,.5f)]
    public float minVolume = .01f;
    //the maximum amount of volume possible (force)
    [Range(0f,1f)]
    public float maxVolume = 1f;
    //the minimum amount of pitch possible (force)
    [Range(0,1f)]
    public float minPitch = .75f;
    //the maximum amount of pitch possible (force)
    [Range(1f,3f)]
    public float maxPitch = 1.5f;


}
