﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy1Controller : MonoBehaviour
{

    private Animator m_Animator;

    GameObject mPlayerObject;

    public int MoveSpeed = 100;
    public int MaxDist = 15;
    public int MinDist = 5;
    private Vector3 lastKnownPlayerLocation;

    void Start()
    {
        mPlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (mPlayerObject != null)
        {
            lastKnownPlayerLocation = mPlayerObject.transform.position;
        }
        m_Animator = GetComponent<Animator>();
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

        if (Vector3.Distance(transform.position, mPlayerObject.transform.position) <= MaxDist)
        {
            
            Vector3 direction = mPlayerObject.transform.position - transform.position;
            direction.Normalize();
            GetComponent<Rigidbody2D>().velocity = (direction) * MoveSpeed * Time.deltaTime;


            if (Vector3.Distance(transform.position, mPlayerObject.transform.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
    }
}