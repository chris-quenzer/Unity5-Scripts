using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NxtLvlMenu : MonoBehaviour {

    gameMaster gm;

    public GameObject NxtLvlUI;
    public Text winTime;

    private Frog player;
    private bool active;

    void Start()
    {
        gm = gameMaster.master;
        NxtLvlUI.SetActive(false);
    }

    void Update()
    {
        if (gm.win)
        {
            active = true;
        }
        else
        {
            active = false;
        }
        if (active)
        {
            NxtLvlUI.SetActive(true);
            //Time.timeScale = 0.5f;
        }
        if (!active)
        {
            NxtLvlUI.SetActive(false);
            //Time.timeScale = 0.5f;
        }

        winTime.text = "Time: " + gm.winTime.ToString("F0");
    }

    public void NextLvl()
    {
        NxtLvlUI.SetActive(false);
        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i + 1);
    }

    public void Restart()
    {
        NxtLvlUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        NxtLvlUI.SetActive(false);
        gm.life = 1;
        SceneManager.LoadScene(0);
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
