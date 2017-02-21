using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class console : MonoBehaviour {

    gameMaster gm;

    public GameObject consoleField;
    public Text arrow;
    public Text consoleMsg;
    public GameObject keyboard;

    InputField input;
    bool consoleOpen = false;

    public string cmd;
    public bool typed = false;
	
	void Start()
    {
        gm = gameMaster.master;
        input = consoleField.GetComponent<InputField>();
        consoleField.SetActive(false);
        arrow.enabled = false;
    }

    void Update()
    {
        if(Input.GetButtonDown("Console"))
        {
            consoleOpen = !consoleOpen;
            if (consoleOpen)
            {
                arrow.enabled = true;
                consoleField.SetActive(true);
                input.ActivateInputField();
                gm.frozen = true;
                /*if (Input.GetButtonDown("Cancel") && gameMaster.master.paused == true)
                {
                    gameMaster.master.paused = true;
                    consoleOpen = false;
                }*/
            }
            else if (!consoleOpen)
            {
                consoleField.SetActive(false);
                gm.frozen = false;
            }
        }

        gm.consoleOpen = consoleOpen;
    }

    public void OnInput()
    {
        if (input.text == "`")
        {
            input.text = "";
        }
    }
    
    public void EnterText()
    {
        
        /*if (input.text == "")
        {
            arrow.enabled = false;
        }*/
        if (Input.GetButtonDown("Submit"))
        {
            consoleOpen = false;
            input.DeactivateInputField();
            cmd = input.text;
            gm.Cheats(cmd);
            input.text = "";
            cmd = input.text;
            gm.Cheats(cmd);
            consoleField.SetActive(false);
            gm.frozen = false;
        }
        
        consoleMsg.text = gm.consoleMsg;
        StartCoroutine(clrConsoleMsg());
    }

    IEnumerator clrConsoleMsg()
    {
        yield return new WaitForSeconds(3.0f);
        consoleMsg.text = "";
        gm.consoleMsg = consoleMsg.text;
    }
}
