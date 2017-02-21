using UnityEngine;
using System.Collections;

public class barrel : MonoBehaviour {

    gameMaster gm;

    public AudioSource barrelSoundHit;
    bool hit;

    void Awake()
    {
        barrelSoundHit = GetComponent<AudioSource>();
        hit = false;
    }

    void Start()
    {
        gm = gameMaster.master;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!hit)
        {
            barrelSoundHit.Play();
            hit = true;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (barrelSoundHit.isPlaying)
        {
            barrelSoundHit.Stop();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        hit = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "barrelKill")
        {
            Destroy(gameObject);
        }
    }
}
