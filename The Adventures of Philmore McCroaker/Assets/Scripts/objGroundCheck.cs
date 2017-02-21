using UnityEngine;
using System.Collections;

public class objGroundCheck : MonoBehaviour {

    public bool grounded;

    void Start()
    {
        //player = gameMaster.master.player;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Platform"))
        {
            grounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Platform"))
        {
            grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Platform"))
        {
            grounded = false;
        }
    }
}

