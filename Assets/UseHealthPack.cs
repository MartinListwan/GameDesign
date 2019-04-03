using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHealthPack : MonoBehaviour
{
    private PlayerStats pStats;
    // Start is called before the first frame update
    void Start()
    {
        pStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(useHealthPackTimer());
        }
    }

   
    IEnumerator useHealthPackTimer()
    {   GameObject thePlayer = GameObject.Find("Player");
        PlayerController playerControllerScript = thePlayer.GetComponent<PlayerController>();
        PlayerHPManager playerScript = thePlayer.GetComponent<PlayerHPManager>();
        if (pStats.healthPacks > 0&& playerScript.playerCurrentHP!= playerScript.playerMaxHP&&!pStats.usingHealthPack)
        {
            playerControllerScript.canMove = false;
            pStats.usingHealthPack = true;
            yield return new WaitForSeconds(2);
            pStats.healthPacks -= 1;           
            playerScript.playerCurrentHP+= playerScript.playerMaxHP / 3;
            playerScript.playerCurrentHP = Mathf.Min(playerScript.playerCurrentHP, playerScript.playerMaxHP);
            pStats.usingHealthPack = false;

        }
        playerControllerScript.canMove = true;
    }
}
