using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour {

    public int QuestNumber;

    private QuestManager QM;

    public string ItemName;

	// Use this for initialization
	void Start () {
        QM = GameObject.FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            if (!QM.questsCompleted[QuestNumber] && QM.quests[QuestNumber].gameObject.activeSelf)
            {
                QM.ItemCollected = this.ItemName;
                gameObject.SetActive(false);
            }
        }
    }

}
