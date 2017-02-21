using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    gameMaster gm;
    public GameObject DeathUI;
    private bool active;

    void Start()
    {
        gm = gameMaster.master;
        DeathUI.SetActive(false);
    }

    void Update()
    {
        if (gm.dead)
        {
            active = true;
        }
        else
        {
            active = false;
        }
        if (active)
        {
            DeathUI.SetActive(true);
            //Time.timeScale = 0;
        }
        if (!active)
        {
            DeathUI.SetActive(false);
            //Time.timeScale = 1;
        }
        if(active && Input.GetButtonDown("Pause"))
        {
            Restart();
        }
    }

    public void Restart()
    {
        active = false;
        gm.life = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        active = false;
        gm.life = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
