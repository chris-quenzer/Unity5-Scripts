using UnityEngine;
using System.Collections;

public class playerRepel : MonoBehaviour {

    gameMaster gm;
    public Transform player;
    public Rigidbody2D playerRb2d;
    public float repelPwr = 300;
    public float repelDur = 0.01f;
    public bool limitRepels = false;
    public int repelLimit = 2;
    public bool repelled;
    public int repelCount = 0;
    public bool playerDetected;

    void Start ()
    {
        gm = gameMaster.master;
        player = GameObject.Find("Frog").GetComponent<Transform>();
        playerRb2d = GameObject.Find("Frog").GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(limitRepels)
        {
            if (repelCount < repelLimit)
            {
                if (other.tag == "Frog" && !gm.dead)
                {
                    playerDetected = true;
                    StartCoroutine(Repel(repelDur, repelPwr));
                }
            }
        }
        else
        {
            if (other.tag == "Frog" && !gm.dead)
            {
                playerDetected = true;
                StartCoroutine(Repel(repelDur, repelPwr)); 
            }  
        }
    }
    /*
    void OnTriggerStay2D(Collider2D other)
    {
        
    }*/
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Frog")
        {
            playerDetected = false;
            repelled = false;
        }
    }

    public IEnumerator Repel(float repelDur, float repelPwr)
    {
        float timer = 0;
        while(repelDur > timer)
        {
            timer += Time.deltaTime;
            if(playerRb2d.velocity.y < 0 && !gm.player.grounded && !gm.dead)
            {
                if (!repelled)
                {
                    repelCount++;
                }
                repelled = true;
            }
            if(repelled)
            {
                playerRb2d.AddForce(transform.up * repelPwr);
            }
        }

        yield return 0;
    }
}
