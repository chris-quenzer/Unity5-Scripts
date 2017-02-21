using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Ammo : MonoBehaviour {

    public Text ammoDisplay;

    Shoot ammo;

	void Start ()
    {
        ammo = GameObject.Find("ShellSpawn").GetComponent<Shoot>();
	}
	
	
	void Update ()
    {
        ammoDisplay.text = ammo.ammo + "/" + ammo.maxAmmo;
	}
}
