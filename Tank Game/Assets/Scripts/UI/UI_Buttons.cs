using UnityEngine;
using System.Collections;

public class UI_Buttons : MonoBehaviour {

    Shoot weapon;

    void Start ()
    {
        weapon = GameObject.Find("ShellSpawn").GetComponent<Shoot>();
    }
	/*
	void Update ()
    {
	    
	}
    */
    public void gravToggle()
    {
        weapon.isGravity = !weapon.isGravity;
    }
}
