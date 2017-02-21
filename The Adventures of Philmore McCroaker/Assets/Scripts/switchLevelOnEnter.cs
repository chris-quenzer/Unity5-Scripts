using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class switchLevelOnEnter : MonoBehaviour {

    public string levelName;

	void Start ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadSceneAsync(levelName);
        }
    }
}
