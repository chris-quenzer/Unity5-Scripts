using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text playerName;
    public Text winText;
    public GameObject result;
    GameMaster gm;

    // Use this for initialization
    void Start ()
    {
        result.SetActive(false);
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
	
	public void playerTurnText()
    {
	    if(gm.p1T)
        {
            playerName.text = "Player 1 - Red";
            playerName.color = Color.red;
        }
        else if (gm.p2T)
        {
            playerName.text = "Player 2 - Black";
            playerName.color = Color.black;
        }
    }

    public void Win()
    {
        if (gm.p1Win)
        {
            winText.text = "Player 1\nWins!";
            winText.color = Color.red;
        }
        if (gm.p2Win)
        {
            winText.text = "Player 2\nWins!";
            winText.color = Color.black;
        }
        if (gm.draw)
        {
            winText.text = "Draw!";
        }
        result.SetActive(true);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
