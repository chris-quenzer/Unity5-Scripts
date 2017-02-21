using UnityEngine;
using System.Collections;

public class LadderZone : MonoBehaviour {

    gameMaster gm;

    private Frog player;
    bool isJumping;
	
	void Start ()
    {
        player = GameObject.Find("Frog").GetComponent<Frog>();
    }

    void Update()
    {
        /*if(gm.player.state == (int)Frog.PlayerState.STATE_JUMPING && gm.player.rb2d.velocity.y > 0)
        {
            player.ladder = false;
        }*/

        if (player.ladder)
        {
            player.grounded = true;
        }
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.inLadder = true;
            if (Input.GetAxis("Vertical") > 0.01f)
            {
                player.ladder = true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && player.ladder)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Platform").GetComponent<Collider2D>(), true);
            player.transform.parent = gameObject.transform.parent;
        }
        if (other.tag == "Player")
        {
            player.inLadder = true;

            //climb up or down ladder
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f)
            {
                player.ladder = true;
            }
            //able to grab ladder while running
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
            {
                player.ladder = true;
            }
            //jump off the ladder
            if (Input.GetButton("Jump"))
            {
                player.ladder = false;
            }
            //jump off the ladder while climbing
            if (Input.GetButton("Jump") && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                player.ladder = false;
            }
            if (Input.GetButton("Jump") && Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                player.ladder = false;
            }
            //jumping and attaching to ladder
            if (Input.GetButton("Jump") && Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                player.ladder = true;
            }
            //drop off the ladder
            if (Input.GetButtonDown("Drop"))
            {
                player.ladder = false;
                player.transform.parent = null;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //not on ladder anymore
        if (other.tag == "Player")
        {
            player.inLadder = false;
            player.ladder = false;
            player.grounded = false;
            player.transform.parent = null;
        }
        //check case where ladder is true when shouldn't be
        if (player.ladder && !player.grounded)
        {
            player.ladder = false;
        }
        if (!player.ladder && player.grounded)
        {
            player.grounded = false;
        }
    }
}
