using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int currentLevel;
    public int currentExp;
    public int[] toLevelUp;
    public int[] HPLevels;
    public int[] attackLevels;
    public int[] defenceLevels;

    public int currentHP;
    public int currentAttack;
    public int currentDefence;

    private PlayerHPManager playerHP;


	// Use this for initialization
	void Start ()
    {
        currentHP = HPLevels[1];
        currentAttack = attackLevels[1];
        currentDefence = defenceLevels[1];
        playerHP = FindObjectOfType<PlayerHPManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //move this so it only checks when you gain xps
		if(currentExp >= toLevelUp[currentLevel])
        {
            LevelUp();
        }
	}
    public void AddExperience(int exp)
    {
        currentExp += exp;
    }
    public void LevelUp()
    {
        currentLevel++;

        currentHP = HPLevels[currentLevel];
        currentAttack = attackLevels[currentLevel];
        currentDefence = defenceLevels[currentLevel];

        playerHP.playerMaxHP = currentHP;
        playerHP.playerCurrentHP += currentHP - HPLevels[currentLevel-1];  //This will heal up player by the difference between old and new health lvl, on level up
    }
}
