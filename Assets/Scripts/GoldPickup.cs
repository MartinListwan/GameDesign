using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour {

    public int Value;
    MoneyManager MMan;

	// Use this for initialization
	void Start () {
        MMan = FindObjectOfType<MoneyManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            MMan.AddGold(Value);
            Destroy(gameObject);
        }
     
    }
}
