using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour {

    public float timeToDestruction;



    void Update()
    {
        timeToDestruction -= Time.deltaTime;

        if (timeToDestruction < 0) Destroy(gameObject);
    }

}
