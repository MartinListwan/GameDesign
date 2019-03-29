using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPManager : MonoBehaviour {

    public int MaxHP;
    public int CurrentHP;
    public int ExpValue;

    private PlayerStats _playerStats;

    public string EnemyQuestName;
    QuestManager QM;

    // Use this for initialization
    void Start()
    {
        QM = FindObjectOfType<QuestManager>();
        CurrentHP = MaxHP;
        _playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHP <= 0)
        {
            QM.EnemyKilled = this.EnemyQuestName;
            Destroy(gameObject);
            _playerStats.AddExperience(ExpValue);
          
        }

    }

    public void HurtEnemy(int damage)
    {
        CurrentHP -= damage;
    }
    public void SetMaxHealth()
    {
        CurrentHP = MaxHP;
    }

}
