using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour {

    public VolumeControl[] VCObjects;

    public float CurrentVolumeLevel;
    public float MaxVolumeLevel = 1f;

	// Use this for initialization
	void Start ()
    {
        VCObjects = FindObjectsOfType<VolumeControl>();
        if(CurrentVolumeLevel > MaxVolumeLevel)
        {
            CurrentVolumeLevel = MaxVolumeLevel;
        }

        foreach(var vc in VCObjects)
        {
            vc.SetAudioLevel(CurrentVolumeLevel);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
