using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class virtualKeyboard : MonoBehaviour {

    gameMaster gm;
    EventSystem eventSys;
    PointerEventData pointerData;

    public GameObject board;
    public InputField textField;
    public GameObject defaultSelectedUI;
    public bool keyboardActive = false;
    public string keyLetter;
    public GameObject currentKey;
    public bool keyboardSet;

	void Start ()
    {
        gm = gameMaster.master;
        eventSys = EventSystem.current;
    }

    void Update()
    {
        if (keyboardActive)
        {
            if (!keyboardSet)
            {
                eventSys.firstSelectedGameObject = defaultSelectedUI;
                eventSys.SetSelectedGameObject(defaultSelectedUI);
                keyboardSet = true;
            }

            if(Input.GetButtonDown("Cancel"))
            {
                backspaceKey();
            }
        }

        ////////////////////////////////////////////////////
        ///// Independent of virtual keyboard activity /////
        ////////////////////////////////////////////////////

        //navigation away from input field
        if (textField.IsActive() && (textField.caretPosition == textField.text.Length || (textField.text.Length > 0 && textField.caretPosition == 0)))
        {
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))//exclude A and D keys
            {
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
                {
                    inputFieldActiveState(false);
                    //print("Deactivating input field");
                }
                
            }
        }
        //navigate up or down in any case
        if (textField.IsActive() && Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            if (!(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)))//exclude W and S keys
            {
                inputFieldActiveState(false);
            }
        }

        //input field unfocused and axis input detected should focus to input field
        if (eventSys.currentSelectedGameObject == null && (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0))
        {
            eventSys.SetSelectedGameObject(defaultSelectedUI);
            textField.ActivateInputField();
            textField.caretPosition = textField.text.Length;
        }
    }

    public void OnClick()
    {
        currentKey = eventSys.currentSelectedGameObject;

        switch(currentKey.tag)
        {
            case "KeyChar":
                getKeyChar();
                break;
            case "KeyBackSpace":
                backspaceKey();
                break;
            case "KeySpace":
                spaceKey();
                break;
            case "KeyEnter":
                enterKey();
                break;
        }
    }

    public void OnKeyOver()
    {
        //print(data);
        //pointerData = data as PointerEventData;
        //currentKey = pointerData.pointerEnter;

        //eventSys.SetSelectedGameObject(currentKey);
    }

    public void OnKeyExit()
    {
        
    }

    public void getKeyChar()
    {
        keyLetter = currentKey.GetComponentInChildren<Text>().text;
        textField.text = textField.text + keyLetter;
    }

    public void spaceKey()
    {
        keyLetter = currentKey.GetComponentInChildren<Text>().text;
        textField.text = textField.text + " ";
    }

    public void backspaceKey()
    {
        keyLetter = currentKey.GetComponentInChildren<Text>().text;
        if (textField.text.Length > 0)
        {
            textField.text = textField.text.Substring(0, textField.text.Length - 1);
        }
    }

    public void enterKey()
    {
        textField.ActivateInputField();
        eventSys.SetSelectedGameObject(textField.gameObject);
        ExecuteEvents.Execute(textField.gameObject, null, ExecuteEvents.submitHandler);
    }

    public void toggleKeyboard()
    {
        keyboardActive = !keyboardActive;

        if (keyboardActive && !keyboardSet)
        {
            board.SetActive(true);
            currentKey = eventSys.currentSelectedGameObject;
            keyboardSet = true;
        }
        if (!keyboardActive)
        {
            board.SetActive(false);
            //eventSys.SetSelectedGameObject(null);
            keyboardSet = false;
        }
    }

    void inputFieldActiveState(bool active)
    {
        if(active)
        {
            textField.ActivateInputField();
        }
        else if(!active)
        {
            textField.DeactivateInputField();
        }
    }
}
