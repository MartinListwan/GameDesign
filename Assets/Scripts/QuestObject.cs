using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour {

    public int QuestNum;
    public QuestManager QM;

    public string startText;
    public string endText;

    public bool IsItemQuest;
    public string TargetItem;

    public bool IsEnemyQuest;
    public string TargetEnemyName;
    public int EnemiesToKill;
    private int enemyKillCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (IsItemQuest)
        {
            if(QM.ItemCollected == this.TargetItem)
            {
                QM.ItemCollected = null;
                EndQuest();
            }
        }
        if (IsEnemyQuest)
        {
            if(QM.EnemyKilled == this.TargetEnemyName)
            {
                QM.EnemyKilled = null;
                enemyKillCount++;

            }
            if(enemyKillCount >= EnemiesToKill)
            {
                EndQuest();
            }
        }
	}

    public void StartQuest()
    {
        QM.ShowQuestText(startText);
    }

    public void EndQuest()
    {
        QM.ShowQuestText(endText);
        QM.questsCompleted[QuestNum] = true;
        this.gameObject.SetActive(false);
    }
}
