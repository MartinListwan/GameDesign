using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour {

    public string dialog;
    private DialogManager dManager;

    public List<string> dialogLines;
	// Use this for initialization
	void Start () {
        dManager = FindObjectOfType<DialogManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // dManager.ShowDialogBox(dialog);
                if (!dManager.dialogActive)
                {
                    dManager.dialogLines = dialogLines;
                    dManager.currentLine = 0; //reset, just in case something went wrong with earlier dialog
                    dManager.ShowDialog();
                  
                }

                if (transform.parent.GetComponent<VillagerMovement>() != null)
                {
                    transform.parent.GetComponent<VillagerMovement>().canMove = false;
                }

            }
        }
    }
}
