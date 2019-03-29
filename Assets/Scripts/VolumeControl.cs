using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour {

    private AudioSource Audio;

    private float audioLevel;
    public float DefaultAudioLevel = 1f;

	// Use this for initialization
	void Start ()
    {
        Audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAudioLevel(float newVolume)
    {
        if(Audio == null)
        {
            Audio = GetComponent<AudioSource>();
        }

        audioLevel = DefaultAudioLevel * newVolume;
        Audio.volume = audioLevel;
    }
}
