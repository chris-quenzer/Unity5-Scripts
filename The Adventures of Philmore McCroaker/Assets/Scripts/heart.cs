using UnityEngine;
using System.Collections;

public class heart : MonoBehaviour {

    private Frog player;
    //private gameMaster gm;
    AudioSource heartSound;
    public bool isEntered;
    gameMaster gm;

    void Start()
    {
        //gm = GameObject.Find("GameMaster").GetComponent<gameMaster>();
        player = gameMaster.master.player;
        heartSound = GetComponent<AudioSource>();
        gm = gameMaster.master;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //other.tag = "Player";

        if (other.tag == "Player")
        {
            if (!isEntered)
            {
                if (gm.life < gm.maxLife && !gm.dead)
                {
                    gm.life += 1;
                    isEntered = true;
                    heartSound.Play();
                    gameObject.GetComponentInChildren<Renderer>().enabled = false;
                    Destroy(gameObject, 0.2f);
                }
                else if (gm.life >= gm.maxLife)
                {
                    gm.life = gm.maxLife;
                }
            }
        }
    }
}
