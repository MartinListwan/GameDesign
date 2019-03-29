using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {

    private QuestManager qM;

    public int QuestNumber;

    public bool startQuest;
    public bool endQuest;

	// Use this for initialization
	void Start () {
        qM = FindObjectOfType<QuestManager>();


	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!qM.questsCompleted[QuestNumber])
            {
                if (startQuest && !qM.quests[QuestNumber].gameObject.activeSelf)
                {
                    qM.quests[QuestNumber].gameObject.SetActive(true);
                    qM.quests[QuestNumber].StartQuest();
                }

                if (endQuest && qM.quests[QuestNumber].gameObject.activeSelf)
                {
                    qM.quests[QuestNumber].EndQuest();
                    // qM.quests[QuestNumber].gameObject.SetActive(true);
                   
                }
            }
        }
    }
}
