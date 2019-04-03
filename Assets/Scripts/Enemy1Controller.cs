using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy1Controller : MonoBehaviour
{

    private Animator m_Animator;

    GameObject mPlayerObject;
    TileAutomata tileAutomata;

    public int MoveSpeed = 100;
    public int MaxDist = 15;
    public int MinDist = 5;
    private Vector3 lastKnownPlayerLocation;
    private List<Vector2> directionsToPlayer;
    private Vector3 currentDestination = Vector3.zero;
    private Timer timer;

    void Start()
    {
        mPlayerObject = GameObject.FindGameObjectWithTag("Player");
        tileAutomata = GameObject.FindGameObjectWithTag("TileAutomata").GetComponent<TileAutomata>();
        if (mPlayerObject != null)
        {
            lastKnownPlayerLocation = mPlayerObject.transform.position;
        }
        if (directionsToPlayer == null && mPlayerObject != null)
        {
            this.directionsToPlayer = tileAutomata.getShortestPath(transform.position.x, transform.position.y, mPlayerObject.transform.position.x, mPlayerObject.transform.position.y);
        }
        timer = Timer.Register(2f, () => updatePlayerLocation(), isLooped: true);
        //Debug.Log(tileAutomata.name);
        m_Animator = GetComponent<Animator>();
    }

    void Destroy()
    {
        timer.Pause();
    }

    void updatePlayerLocation()
    {
        if (gameObject != null || gameObject.activeInHierarchy)
        {
            this.directionsToPlayer = tileAutomata.getShortestPath(transform.position.x, transform.position.y, lastKnownPlayerLocation.x, lastKnownPlayerLocation.y);
            Debug.Log("Human = " + string.Join("",
                new List<Vector2>(directionsToPlayer)
                .ConvertAll(i => i.ToString())
                .ToArray()));
            currentDestination = Vector3.zero;
            Debug.Log("Reset timer");
        } else
        {
            timer.Pause();
        }
    }


    void Update()
    {
        Vector3 playerLocation;
        if (mPlayerObject == null)
        {
            playerLocation = this.lastKnownPlayerLocation;
        } else
        {
            playerLocation = mPlayerObject.transform.position;
        }

        lastKnownPlayerLocation = playerLocation;

        if (Vector3.Distance(transform.position, playerLocation) < 2)
        {
            Vector3 direction = currentDestination - transform.position;
            direction.Normalize();
            Debug.Log("Heading towards: " + currentDestination);
            GetComponent<Rigidbody2D>().velocity = (direction) * MoveSpeed * Time.deltaTime;
            currentDestination = playerLocation;
        }


            /**
             * Have a current path that we still need to get to
             */
        else if (Vector3.Distance(transform.position, currentDestination) < .2)
        {
            Debug.Log("Reached destination");
            currentDestination = Vector3.zero;
        } else if (currentDestination != Vector3.zero)
        {
            Debug.Log("Have this much distance: " + Vector3.Distance(transform.position, currentDestination));
            Vector3 direction = currentDestination - transform.position;
            direction.Normalize();
            Debug.Log("Heading towards: " + currentDestination);
            GetComponent<Rigidbody2D>().velocity = (direction) * MoveSpeed * Time.deltaTime;
        }

        // Need new destination
        if (currentDestination == Vector3.zero && directionsToPlayer != null && directionsToPlayer.Count > 0)
        {
            foreach (Vector2 dest in directionsToPlayer)
            {
                currentDestination = new Vector3(dest.x, dest.y, 0);
                break;
            }
            currentDestination = new Vector3(directionsToPlayer[0].x, directionsToPlayer[0].y, 0);
            directionsToPlayer.RemoveAt(0);

            if (Vector3.Distance(transform.position, playerLocation) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }
        }

        if ((directionsToPlayer == null || directionsToPlayer.Count == 0) && playerLocation != null)
        {
            this.directionsToPlayer = tileAutomata.getShortestPath(transform.position.x, transform.position.y, playerLocation.x, playerLocation.y);
        }
    }
}