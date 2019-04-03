using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeHealthPack : MonoBehaviour
{
    private PlayerStats pStats;
    private Time cooldown;
    // Start is called before the first frame update
    void Start()
    {
        pStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //Debug.Log("tryhealthpacks");
            StartCoroutine(makeHealthPackWait());
        }
    }
    IEnumerator makeHealthPackWait()
    {
        Debug.Log("making healthpacks");
        GameObject thePlayer = GameObject.Find("Player");
            PlayerController playerScript = thePlayer.GetComponent<PlayerController>();
        if (pStats.healthShards >= 3&&!pStats.makingHealthPack)
        {

            pStats.makingHealthPack = true;
            playerScript.canMove = false;
            yield return new WaitForSeconds(3f);
            pStats.healthShards -= 3;
            pStats.healthPacks += 1;
            playerScript.canMove = true;
            pStats.makingHealthPack = false;
        }

      
    }
   
   
}
