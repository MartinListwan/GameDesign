using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadNewArea : MonoBehaviour {

    public string sceneToLoad;

    public string exitPoint;

    private PlayerController _player;

	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);
            _player.startPoint = exitPoint;
        }

    }
}
