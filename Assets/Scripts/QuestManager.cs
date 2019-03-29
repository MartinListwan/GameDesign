using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public QuestObject[] quests;
    public bool[] questsCompleted;

    public DialogManager DM;

    public string ItemCollected;
    public string EnemyKilled;

	// Use this for initialization
	void Start () {
        questsCompleted = new bool[quests.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowQuestText(string text)
    {
        DM.dialogLines.Clear();
        DM.dialogLines.Add(text);

        DM.currentLine = 0;
        DM.ShowDialog();
    }
}
