﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicMeterControll : MonoBehaviour
{
    public Text text;
    private PlayerStats pStats;
    // Start is called before the first frame update
    void Start()
    {
        pStats = FindObjectOfType<PlayerStats>();
        text.text = "Magic Meter: %" + pStats.magicMeter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Magic Meter: %" + pStats.magicMeter.ToString();
    }
}