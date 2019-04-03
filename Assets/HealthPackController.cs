using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPackController : MonoBehaviour
{
    public Text healthPackTextBox;
    private PlayerStats pStats;
    // Start is called before the first frame update
    void Start()
    {
        pStats = FindObjectOfType<PlayerStats>();
        healthPackTextBox.text = "HealthPack: " + pStats.healthPacks.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthPackTextBox.text = "HealthPack: " + pStats.healthPacks.ToString();
    }
}
