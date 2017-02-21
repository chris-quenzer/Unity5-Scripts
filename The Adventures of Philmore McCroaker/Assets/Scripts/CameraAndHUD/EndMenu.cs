using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour {

    public GameObject EndUI;

    private bool active;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "End")
        {
            EndUI.SetActive(true);
        }
        else
        {
            EndUI.SetActive(false);
        }
    }

    public void MainM()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
