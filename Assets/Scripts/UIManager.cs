using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Slider healthBar;
    public Text healthText;
    public PlayerHPManager playerHPM;
    private PlayerStats playerS;
    public Text lvlText;

    private static bool UIExists;

	// Use this for initialization
	void Start ()
    {
        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        playerS = GetComponent<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        healthBar.maxValue = playerHPM.playerMaxHP;
        healthBar.value = playerHPM.playerCurrentHP;
        healthText.text = string.Format("HP: {0}/{1}",playerHPM.playerCurrentHP,playerHPM.playerMaxHP);

        lvlText.text = string.Format("Lvl {0}",playerS.currentLevel);
	}
}
