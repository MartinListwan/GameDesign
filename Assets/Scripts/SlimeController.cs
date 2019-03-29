using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour {


    public float moveSpeed; 

    private Rigidbody2D myRigidbody;

    private bool moving;

    public float timeBetweenMoves;
    private float timeBetweenMovesCounter;

    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    //Perfect candidate to get moved somewhere more logical
    public float waitToReload;
    private bool reloading;
    private GameObject playerObj;

	// Use this for initialization
	void Start ()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();

        //timeToMoveCounter = timeToMove;
        //timeBetweenMovesCounter = timeBetweenMoves;

        //Encapsulate this into a property, so code won't be duplicated
        timeBetweenMovesCounter = Random.Range(timeBetweenMoves * 0.75f, timeBetweenMoves * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f,timeToMove * 1.25f);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (moving)
        {
            timeToMoveCounter -=  Time.deltaTime;
            myRigidbody.velocity = moveDirection; 

            if(timeToMoveCounter < 0)
            {
                moving = false;
                //timeBetweenMovesCounter = timeBetweenMoves;
                timeBetweenMovesCounter = Random.Range(timeBetweenMoves * 0.75f, timeBetweenMoves * 1.25f);

            }
        }
        else
        {
            timeBetweenMovesCounter -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;

            if(timeBetweenMovesCounter < 0)
            {
                moving = true;
                //timeToMoveCounter = timeToMove;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                moveDirection = new Vector3(
                    Random.Range(-1f,1f) * moveSpeed,
                    Random.Range(-1f,1f) * moveSpeed,
                    0f);
            }
        }
        //Also move this
        if (reloading)
        {
            waitToReload -= Time.deltaTime;
            if(waitToReload < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                playerObj.SetActive(true);
            }
        }
	}


}
