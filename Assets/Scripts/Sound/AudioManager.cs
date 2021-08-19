using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //allow the script to be accessible anywhere
    #region Singleton
    //set the script to be static (accessible everywhere)
    public static AudioManager instance;

    //before the game starts (via loading)
    private void Awake()
    {
        //if there was already a audiomanager in the scene ... destroy this version
        if (instance) Destroy(this);
        //set the current instance of audiomanager to this object
        else instance = this;
        //dont destroy (on a scene change)
        //DontDestroyOnLoad(this);
    }

    #endregion

    //a list of sounds (may contain all sounds possible or most sounds)
    public Sound[] sounds;

    //play the sound in the source
    public void Play(AudioSource source)
    {
        //plays the sound in the source
        source.Play();
    }

    //play a sound effect (via the name of the sound effect, via the source to play it from)
    public void Play(string name, GameObject sourceObj)
    {
        //search for the sound in the sounds array (via the name) and store that (as "s")
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //if s was not found
        if (s == null)
        {
            //print an error to say that the sound was not able to be found
            Debug.LogError(name + " was not found in the sound array");
            //dont continue the function
            return;
        }

        //returns/adds the source of the source object
        AudioSource source = GetSource(sourceObj);

        //apply all the audio information into the source
        UpdateSource(source,s);

        //play the source
        Play(source);
    }

    //play a sound effect (via the name of the sound effect, via the source to play it from, affected via force)
    public void Play(string name, GameObject sourceObj, float force)
    {
        //search for the sound in the sounds array (via the name) and store that (as "s")
        Sound s = Array.Find(sounds, sound => sound.name == name);

        //if s was not found
        if (s == null)
        {
            //print an error to say that the sound was not able to be found
            Debug.LogError(name + " was not found in the sound array");
            //dont continue the function
            return;
        }

        //returns/adds the source of the source object
        AudioSource source = GetSource(sourceObj);

        //apply all the audio information into the source
        UpdateSource(source, s);

        //play the source
        Play(source);
    }

    //play a sound effect (inputting particular sound, via the source to play it from)
    public void Play(Sound s, GameObject sourceObj)
    {
        //returns/adds the source of the source object
        AudioSource source = GetSource(sourceObj);

        //apply all the audio information into the source
        UpdateSource(source, s);

        //play the source
        Play(source);
    }

    //play a sound effect (inputting particular sound, via the source to play it from, affected via force)
    public void Play(Sound s, GameObject sourceObj, float force)
    {
        //returns/adds the source of the source object
        AudioSource source = GetSource(sourceObj);

        //apply all the audio information into the source
        UpdateSource(source, s);

        //play the source
        Play(source);
    }

    //stop the sound from playing
    public void Stop(AudioSource source)
    {
        //stop the sound being played on the audiosource
        source.Stop();
    }

    //stop the sound effect being played
    public void Stop(GameObject sourceObj)
    {
        //the audio source to stop
        AudioSource source = null;

        //if there is no current audio source on the source object
        if (!sourceObj.GetComponent<AudioSource>())
        {
            //log an error
            Debug.LogError(sourceObj.name + "'s Audio Source Component Does not Exist");
            //end here
            return;
        }
        //if there is a current audio source on teh source object
        else
        {
            //set the source to use as the source objects audio source
            source = sourceObj.GetComponent<AudioSource>();
        }

        //stop the audio source
        Stop(source);
    }


    //returns the source of an object (by getting or adding one)
    private AudioSource GetSource(GameObject sourceObj)
    {
        //set an audio source to be used for playing the sound effect
        AudioSource source = null;

        //if there is no current audio source on the source object
        if (!sourceObj.GetComponent<AudioSource>())
        {
            //add a new audiosource to the object
            source = sourceObj.AddComponent<AudioSource>();
        }
        //if there is a current audio source on teh source object
        else
        {
            //set the source to use as the source objects audio source
            source = sourceObj.GetComponent<AudioSource>();
        }
        //return the source
        return source;
    }

    //returns an audio source (with sound information stored into it)
    private void UpdateSource(AudioSource source ,Sound s)
    {
        //apply the information stored in the sound file (apply the clip)
        source.clip = s.clip;
        //apply the information stored in the sound file (apply the volume)
        source.volume = s.volume;
        //apply the information stored in the sound file (apply the pitch)
        source.pitch = s.pitch;
        //apply the information stored in the sound file (apply the doppler)
        source.dopplerLevel = s.doppler;
    }
    //returns an audio source (with sound information stored into it)
    private void UpdateSource(AudioSource source, Sound s, float force)
    {
        //apply the information stored in the sound file (apply the clip)
        source.clip = s.clip;
        //apply the information stored in the sound file (apply the volume)
        source.volume = s.volume / force;
        //apply the information stored in the sound file (apply the pitch)
        source.pitch = s.pitch / force;
        //apply the information stored in the sound file (apply the doppler)
        source.dopplerLevel = s.doppler;
        //apply the minimum distance stored in the sound file (apply the min distance)
        source.minDistance = s.minDistance;
        //apply the maximum distance stored in the sound file (apply the maximum distance)
        source.maxDistance = (s.maxDistance / force) + source.minDistance;
    }

    //an ienumerator that transitions from one sound to another (t = first timer) (T = second timer)
    private IEnumerator Transition(AudioSource source, Sound s, float a_timer = 1, float b_Timer = 1)
    {
        //stores the sources original volume (before transition)
        float o_Volume = source.volume;

        //stores the timers original amount (before transition)
        float o_Time = a_timer;

        //while the timer is not 0
        while (a_timer > 0)
        {
            
            //change the volume amount based on the timer (original volume amount e.g. .5f / current timer amount e.g. 1 second / original timer amount e.g. 2 seconds)
            source.volume = o_Volume/a_timer/o_Time;

            //decrease the timer overtime
            a_timer -= Time.deltaTime;
            //end here (to reloop)
            yield return null;
        }

        //update the sources values
        UpdateSource(source,s);

        //store the volume amount that will sound will rise to
        o_Volume = s.volume;

        //reuse the timer to store the b timer full amount
        o_Time = b_Timer;

        //set the volume back to 0
        source.volume = 0;



        //while the timer is not 0
        while (b_Timer > 0)
        {
            //set the volume amount based on the timer 
            source.volume = s.volume * b_Timer / o_Time;
            
            //decrease the timer overtime
            b_Timer -= Time.deltaTime;

            //end here (to reloop)
            yield return null; 
        }
    }
}
