using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController player;
    private CameraController playerCamera;

    public Vector2 startDirection;

    public string pointName;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindObjectOfType<PlayerController>();

        playerCamera = GameObject.FindObjectOfType<CameraController>();
        player.lastMove = startDirection;

        player.transform.position = this.transform.position;
        playerCamera.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            playerCamera.transform.position.z);



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
