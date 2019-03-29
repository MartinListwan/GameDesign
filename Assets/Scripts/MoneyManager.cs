using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    public Text MoneyText;
    public int CurrentGold;

	// Use this for initialization
	void Start ()
    {
        if (PlayerPrefs.HasKey("current_money"))
        {
            CurrentGold = PlayerPrefs.GetInt("current_money");
        }
        else
        {
            CurrentGold = 0;
            PlayerPrefs.SetInt("current_money",0);
        }
        MoneyText.text = "Gold: " + CurrentGold;
	}
	
	
    public void AddGold(int amount)
    {
        CurrentGold += amount;
        PlayerPrefs.SetInt("current_money", CurrentGold);
        MoneyText.text = "Gold: " + CurrentGold;
    }


}
