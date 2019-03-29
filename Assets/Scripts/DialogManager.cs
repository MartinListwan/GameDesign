using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public GameObject DialogBox;
    public Text dialogText;

    public bool dialogActive;

    public List<string> dialogLines;
    public int currentLine;

    private PlayerController thePlayer;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (dialogActive && Input.GetKeyUp(KeyCode.Space))
        {
            //DialogBox.SetActive(false);
            //dialogActive = false;
            currentLine++;
        }
        if(currentLine >= dialogLines.Count)
        {
            DialogBox.SetActive(false);
            dialogActive = false;

            currentLine = 0;
            thePlayer.canMove = true;
        }

        dialogText.text = dialogLines[currentLine];

    }

    public void ShowDialogBox(string dialog)
    {
        dialogActive = true;
        DialogBox.SetActive(true);
        dialogText.text = dialog;
    }

    public void ShowDialog()
    {
        dialogActive = true;
        DialogBox.SetActive(true);
        thePlayer.canMove = false;
    }
}
