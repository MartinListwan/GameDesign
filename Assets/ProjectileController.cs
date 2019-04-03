using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public int DamageDealt;
    public float speed = 20;
    private int currentDamageDealt;
    public GameObject damageBurstEffect;
    public Rigidbody2D rb;
    public GameObject floatingNumberPrefab;
    public Transform projectilePosition;
    private PlayerStats pStats;

    // Start is called before the first frame update
    void Start()
    {
        pStats = FindObjectOfType<PlayerStats>();

        GameObject thePlayer = GameObject.Find("Player");
        PlayerController playerScript = thePlayer.GetComponent<PlayerController>();
        rb.velocity = playerScript.lastMove * speed;
        Destroy(gameObject, 5f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"||collision.gameObject.tag == "enemy")
        {
            currentDamageDealt = DamageDealt + pStats.currentAttack;
            collision.gameObject.GetComponent<EnemyHPManager>().HurtEnemy(currentDamageDealt);
            Instantiate(damageBurstEffect, projectilePosition.position, projectilePosition.rotation);
            var clone = (GameObject)Instantiate(floatingNumberPrefab, projectilePosition.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamageDealt;
            Destroy(gameObject);
        }
            
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

}
