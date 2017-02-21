using UnityEngine;
using System.Collections;

public class fallingPlatform : MonoBehaviour {

    Rigidbody2D platRb2d;

    public bool playerDetected;
    public bool fall;
    public float fallTime = 1.0f;
    float fallTimer;
    public bool allowTimerReset;
    bool disable;
    float disableTime = 5.0f;
    float disableTimer;

    void Start ()
    {
        platRb2d = gameObject.GetComponentInParent<Rigidbody2D>();
        disableTime = fallTime + disableTime;//account for time taken to cause platform to fall
	}
	
	void Update ()
    {
        //Timer 
        if (playerDetected)
        {
            fallTimer += Time.deltaTime;
            if (fallTimer > fallTime)
            {
                fall = true;
            }
        }
        if(fall)
        {
            platRb2d.isKinematic = false;
            disableTimer += Time.deltaTime;
            if (disableTimer > disableTime)
            {
                disable = true;
            }
            if(disable)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Frog")
        {
            playerDetected = true;
        }

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Frog")
        {
            playerDetected = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Frog")
        {
            if(allowTimerReset)
            {
                playerDetected = false;
                if (!fall)
                {
                    fallTimer = 0;
                }
            }
        }
    }
}
