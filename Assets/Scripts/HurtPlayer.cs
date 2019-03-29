using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageDealt;
    private int currentDamageDealt;
    public GameObject damageBurstEffect;
    public GameObject floatingNumberPrefab;

    private PlayerStats pStats;

    // Use this for initialization
    void Start () {
        pStats = FindObjectOfType<PlayerStats>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            currentDamageDealt = damageDealt - pStats.currentDefence;
            if (currentDamageDealt < 0) currentDamageDealt = 0;

            other.gameObject.GetComponent<PlayerHPManager>().HurtPlayer(currentDamageDealt);

            //Hmm, move this to PlayerHPManager? so it's more encapsulated
            var clone = (GameObject)Instantiate(floatingNumberPrefab, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamageDealt;
        }

    }
}
