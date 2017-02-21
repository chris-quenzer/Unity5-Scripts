using UnityEngine;
using System.Collections;

public class FrogCoin : MonoBehaviour
{

    //private Frog player;
    gameMaster gm;
    AudioSource coinSound;
    bool collected;

    void Start()
    {
        gm = gameMaster.master;
        coinSound = GetComponent<AudioSource>();
        //player = GameObject.Find("Frog").GetComponent<Frog>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            if (!collected)
            {
                coinSound.Play();
                collected = true;
                gm.frogCoin = true;
                gameObject.GetComponent<Renderer>().enabled = false;
                Destroy(gameObject, 0.2f);
            }
        }
    }
}
