using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHealthShards : MonoBehaviour
{
    public Text healthShardTextBox;
    private PlayerStats pStats;
    // Start is called before the first frame update
    void Start()
    {
        pStats = FindObjectOfType<PlayerStats>();
        healthShardTextBox.text = "HealthShards: "+ pStats.healthShards.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthShardTextBox.text = "HealthShards: " + pStats.healthShards.ToString();
    }
}
