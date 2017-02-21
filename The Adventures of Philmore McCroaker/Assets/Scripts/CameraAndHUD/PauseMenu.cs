using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    gameMaster gm;

    public GameObject PauseUI;

    private bool paused = false;

    void Start()
    {
        gm = gameMaster.master;
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "End" && !gm.dead)
        {
            paused = !paused;
        }

        if(gm.win)
        {
            paused = false;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }

        gm.paused = paused;
    }
	
    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        paused = false;
        gm.life = gm.startLife;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void lastLevel()
    {
        paused = false;
        int i = SceneManager.GetActiveScene().buildIndex;
        if (i != 1)
        {
            SceneManager.LoadScene(i - 1);
        }
    }

    public void MainMenu()
    {
        paused = false;
        gm.life = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
