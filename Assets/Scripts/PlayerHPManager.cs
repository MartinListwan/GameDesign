using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPManager : MonoBehaviour {

    public int playerMaxHP;
    public int playerCurrentHP;

    private bool flashActive;
    public float flashLength;
    private float flashCounter;

    private SpriteRenderer playerSprite;

    private SFXManager sfxMan;

	// Use this for initialization
	void Start () {
        sfxMan = FindObjectOfType<SFXManager>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerCurrentHP = playerMaxHP;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(playerCurrentHP <= 0)
        {
            gameObject.SetActive(false); //And delete this once Respawn() works

            sfxMan.playerDead.Play();
            
            //GameController.Respawn();
        }
        if (flashActive)
        {
            if(flashCounter > (flashLength * 0.66f))
            {
                //1f = 255
                playerSprite.color = new Color(
                 playerSprite.color.r,
                 playerSprite.color.g,
                 playerSprite.color.b,
                 0f);
            }
            else if (flashCounter > (flashLength * 0.33f))
            {
                playerSprite.color = new Color(
             playerSprite.color.r,
             playerSprite.color.g,
             playerSprite.color.b,
             1f);
            }
            else if(flashCounter > 0f)
            {
                playerSprite.color = new Color(
              playerSprite.color.r,
              playerSprite.color.g,
              playerSprite.color.b,
              0f);
            }
            else
            {               
                playerSprite.color = new Color(
                    playerSprite.color.r,
                    playerSprite.color.g,
                    playerSprite.color.b,
                    1f); 
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
		
	}

    public void HurtPlayer(int damage)
    {
        //can't get damaged during invincibility frames
        if (!flashActive)
        {
            playerCurrentHP -= damage;
            flashActive = true;
            flashCounter = flashLength;

            sfxMan.playerHurt.Play();
        }
      
    }
    public void SetMaxHealth()
    {
        playerCurrentHP = playerMaxHP;
    }

}
