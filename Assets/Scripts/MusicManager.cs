using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioSource[] MusicTracks;

    public int CurrentTrack;

    public bool MusicCanPlay;

    private static bool MusicManExists;
    // Use this for initialization
    void Start()
    {
        if (!MusicManExists)
        {
            MusicManExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (MusicCanPlay)
        {
            if (!MusicTracks[CurrentTrack].isPlaying)
            {
                MusicTracks[CurrentTrack].Play();
            }
        }
        else
        {
            MusicTracks[CurrentTrack].Stop();
        }
    }

    public void SwitchTrack(int newTrack)
    {
        MusicTracks[CurrentTrack].Stop();
        CurrentTrack = newTrack;
        MusicTracks[CurrentTrack].Play();

    }
}
