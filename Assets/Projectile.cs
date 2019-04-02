using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour


{
    public int DamageDealt, speed = 20;
    private int currentDamageDealt;
    public GameObject damageBurstEffect;
    public Rigidbody2D rb;
    public GameObject floatingNumberPrefab;
    public GameObject projectilePosition;
    private PlayerStats pStats;
    
    // Start is called before the first frame update
    void Start()
    {
        pStats = FindObjectOfType<PlayerStats>();

        GameObject thePlayer = GameObject.Find("ThePlayer");
        PlayerController playerScript = thePlayer.GetComponent<PlayerController>();
        rb.velocity = playerScript.lastMove * speed;
        Destroy(gameObject, 5f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentDamageDealt = DamageDealt + pStats.currentAttack;
            collision.gameObject.GetComponent<EnemyHPManager>().HurtEnemy(currentDamageDealt);
           // var clone = (GameObject)Instantiate(floatingNumberPrefab, , Quaternion.Euler(Vector3.zero));
            Destroy(gameObject);
        }
    }
}
