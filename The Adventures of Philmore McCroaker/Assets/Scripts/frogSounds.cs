using UnityEngine;
using System.Collections;

public class frogSounds : MonoBehaviour {

    gameMaster gm;
    AudioSource jumpSound;

    // Use this for initialization
    void Start ()
    {
        gm = gameMaster.master;
        jumpSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Jump") && gm.player.grounded && !gm.player.isFrozen && !gm.dead && !gm.paused)
        {
            jumpSound.Play();
        }
    }
}
