using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject PauseUI;
    //public gameMaster gm;
    //public Frog player;

    private bool paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
        //gm = GameObject.Find("GameMaster").GetComponent<gameMaster>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            paused = !paused;
        }
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void lastLevel()
    {
        SaveState();
        int i = SceneManager.GetActiveScene().buildIndex;
        if (i != 1)
        {
            SceneManager.LoadScene(i - 1);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void SaveState()
    {
        //PlayerPrefs.SetInt("Life", player.life);
    }
}
