using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMovement : MonoBehaviour {

    public float moveSpeed;
    private Vector2 minMovePoint;
    private Vector2 maxMovePoint;


    public bool isMoving;
    public float moveTime;
    private float moveCounter;
    public float waitTime;
    private float waitCounter;

    private int moveDirection;

    public Collider2D moveZone; //allowed walk zone. If not assigned then NPC can move freely over the map
    private bool hasMoveZone;
    private Rigidbody2D rigid2D;

    public bool canMove;
    private DialogManager dManager;
    // Use this for initialization
    void Start () {

        rigid2D = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        moveCounter = moveTime;

        ChooseDirection();

        if (moveZone != null)
        {
            hasMoveZone = true;
            minMovePoint = moveZone.bounds.min;
            maxMovePoint = moveZone.bounds.max;
        }
        dManager = FindObjectOfType<DialogManager>();

        canMove = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!dManager.dialogActive)
        {
            canMove = true;
        }
        if (!canMove)
        {
            rigid2D.velocity = Vector2.zero;
            return;
        }

        if (isMoving)
        {
            moveCounter -= Time.deltaTime;

            switch (moveDirection)
            {
                case 0:
                    rigid2D.velocity = new Vector2(0,moveSpeed);
                    if (hasMoveZone && transform.position.y > maxMovePoint.y)
                    {
                        StopMove();
                    }
                    break;
                case 1:
                    rigid2D.velocity = new Vector2(moveSpeed,0);
                    if (hasMoveZone && transform.position.x > maxMovePoint.x)
                    {
                        StopMove();
                    }
                    break;
                case 2:
                    rigid2D.velocity = new Vector2(0,-moveSpeed);
                    if (hasMoveZone && transform.position.y < minMovePoint.y)
                    {
                        StopMove();
                    }
                    break;
                case 3:
                    rigid2D.velocity = new Vector2(-moveSpeed, 0);
                    if (hasMoveZone && transform.position.x < minMovePoint.x)
                    {
                        StopMove();
                    }
                    break;
                default:
                    Debug.Log("MOVE DIRECTION ERROR!");
                    break;
            }

            if (moveCounter < 0)
            {
                StopMove();
            }



        }
        else
        {
            waitCounter -= Time.deltaTime;
            rigid2D.velocity = Vector2.zero;

            if(waitCounter < 0)
            {
                ChooseDirection();
            }
        }
	}

    public void ChooseDirection()
    {
        moveDirection = Random.Range(0,4); //y is exclusive, it only 0,1,2 and 3 possible
        isMoving = true;
        moveCounter = moveTime;
    }
    private void StopMove()
    {
        isMoving = false;
        waitCounter = waitTime;
    }

}
