using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NavigationControl : MonoBehaviour {

    gameMaster gm;
    EventSystem eventSys;

    public GameObject pauseFirstButton;
    public GameObject deathFirstButton;
    public GameObject winFirstButton;
    public GameObject mainMenuFirstButton;
    public GameObject endMenuFirstButton;

    public GameObject consoleField;

    public bool pauseSet;
    public bool deathSet;
    public bool winSet;
    public bool mainMenuSet;
    public bool endMenuSet;
    public bool consoleSet;

    void Start ()
    {
        gm = gameMaster.master;
        eventSys = EventSystem.current;
	}
	
	void Update ()
    {
	    if(gm.paused)
        {
            pauseNavActive();
        }
        else if(gm.dead)
        {
            deathNavActive();
        }
        else if (gm.win)
        {
            winNavActive();
        }
        else if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            mainMenuNavActive();
        }
        else if (SceneManager.GetActiveScene().name == "End")
        {
            endMenuNavActive();
        }
        else if (gm.consoleOpen)
        {
            consoleNavActive();
        }
        else
        {
            pauseSet = false;
            deathSet = false;
            winSet = false;
            mainMenuSet = false;
            endMenuSet = false;
            consoleSet = false;
            eventSys.SetSelectedGameObject(null);
            eventSys.firstSelectedGameObject = null;
        }
    }

    //set navigation on pause
    public void pauseNavActive()
    {
        if (!pauseSet && !gm.consoleOpen)
        {
            eventSys.firstSelectedGameObject = pauseFirstButton;
            eventSys.SetSelectedGameObject(pauseFirstButton);
            pauseSet = true;
        }
        
        if (eventSys.currentSelectedGameObject == null)
        {
            eventSys.SetSelectedGameObject(pauseFirstButton);
        }
        if (eventSys.currentSelectedGameObject == null && Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            eventSys.SetSelectedGameObject(pauseFirstButton);
        } 
        
        //opening and closing console
        if (gm.consoleOpen)
        {
            consoleNavActive();
        }
        else if (pauseSet && !gm.consoleOpen && consoleSet)
        {
            consoleSet = false;
            pauseSet = false;
        }
    }
    //set navigation on death
    public void deathNavActive()
    {
        if (!deathSet && !gm.consoleOpen)
        {
            eventSys.firstSelectedGameObject = deathFirstButton;
            eventSys.SetSelectedGameObject(deathFirstButton);
            deathSet = true;
        }
        if (eventSys.currentSelectedGameObject == null)
        {
            eventSys.SetSelectedGameObject(deathFirstButton);
        }
        if (eventSys.currentSelectedGameObject == null && Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            eventSys.SetSelectedGameObject(deathFirstButton);
        }
        //opening and closing console
        if (gm.consoleOpen)
        {
            consoleNavActive();
        }
        else if (deathSet && !gm.consoleOpen && consoleSet)
        {
            consoleSet = false;
            deathSet = false;
        }
    }
    //set navigation on win
    public void winNavActive()
    {
        if (!winSet && !gm.consoleOpen)
        {
            eventSys.firstSelectedGameObject = winFirstButton;
            eventSys.SetSelectedGameObject(winFirstButton);
            winSet = true;
        }
        if (eventSys.currentSelectedGameObject == null)
        {
            eventSys.SetSelectedGameObject(winFirstButton);
        }
        if (eventSys.currentSelectedGameObject == null && Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            eventSys.SetSelectedGameObject(winFirstButton);
        }
        //opening and closing console
        if (gm.consoleOpen)
        {
            consoleNavActive();
        }
        else if (winSet && !gm.consoleOpen && consoleSet)
        {
            consoleSet = false;
            winSet = false;
        }
    }
    //set navigation on main menu
    public void mainMenuNavActive()
    {
        if (!mainMenuSet)
        {
            eventSys.firstSelectedGameObject = mainMenuFirstButton;
            eventSys.SetSelectedGameObject(mainMenuFirstButton);
            mainMenuSet = true;
        }
        if (eventSys.currentSelectedGameObject == null)
        {
            eventSys.SetSelectedGameObject(mainMenuFirstButton);
        }
        if (eventSys.currentSelectedGameObject == null && Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            eventSys.SetSelectedGameObject(mainMenuFirstButton);
        }
    }
    //set navigation on end menu
    public void endMenuNavActive()
    {
        if (!endMenuSet)
        {
            eventSys.firstSelectedGameObject = endMenuFirstButton;
            eventSys.SetSelectedGameObject(endMenuFirstButton);
            endMenuSet = true;
        }
        if (eventSys.currentSelectedGameObject == null)
        {
            eventSys.SetSelectedGameObject(endMenuFirstButton);
        }
        if (eventSys.currentSelectedGameObject == null && Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            eventSys.SetSelectedGameObject(endMenuFirstButton);
        }
    }
    //set navigation on console activation
    public void consoleNavActive()
    {
        if (!consoleSet)
        {
            eventSys.firstSelectedGameObject = consoleField;
            eventSys.SetSelectedGameObject(consoleField);
            consoleSet = true;
        }
    }
}
