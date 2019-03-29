using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour {

    public int DamageDealt;
    private int currentDamageDealt;
    public GameObject damageBurstEffect;
    public Transform strikePoint;
    public GameObject floatingNumberPrefab;

    private PlayerStats pStats;

	// Use this for initialization
	void Start () {
        pStats = FindObjectOfType<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            currentDamageDealt = DamageDealt + pStats.currentAttack;

            other.gameObject.GetComponent<EnemyHPManager>().HurtEnemy(currentDamageDealt);
            Instantiate(damageBurstEffect,strikePoint.position,strikePoint.rotation);
            var clone = (GameObject)Instantiate(floatingNumberPrefab, strikePoint.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamageDealt;
        }
    }
}
