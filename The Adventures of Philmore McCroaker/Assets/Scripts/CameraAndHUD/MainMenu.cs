using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public GameObject MainUI;

    private bool active;

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainUI.SetActive(true);
        }
        else if(SceneManager.GetActiveScene().name != "End")
        {
            MainUI.SetActive(false);
        }
    }

    public void NextLvl()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i + 1);
    }

    public void MainM()
    {
        PlayerPrefs.SetInt("Life", 1);
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
