using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour {

    private MusicManager mcMan;

    public int NewTrack;

    public bool SwitchOnStart;

	// Use this for initialization
	void Start () {
        mcMan = FindObjectOfType<MusicManager>();

        if (SwitchOnStart)
        {
            mcMan.SwitchTrack(NewTrack);
            gameObject.SetActive(false);
        }
	}	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            mcMan.SwitchTrack(NewTrack);
            gameObject.SetActive(false); // So you won't reset the track multiple times 
        }
    }
}
