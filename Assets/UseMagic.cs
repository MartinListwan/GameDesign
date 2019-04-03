using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMagic : MonoBehaviour
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
        if (Input.GetButtonDown("Magic"))
        {
            
            StartCoroutine(useMagic());
        }
    }

    IEnumerator useMagic()
    {
        GameObject thePlayer = GameObject.Find("Player");
        PlayerController playerControllerScript = thePlayer.GetComponent<PlayerController>();
        PlayerHPManager playerScript = thePlayer.GetComponent<PlayerHPManager>();

        if (pStats.magicMeter >=75 && !pStats.magicActive)
        {
            pStats.magicActive = true;
            pStats.magicMeter -= 50;
            yield return new WaitForSeconds(20);
            
            pStats.magicActive = false;
            
        }
       
    }
}
