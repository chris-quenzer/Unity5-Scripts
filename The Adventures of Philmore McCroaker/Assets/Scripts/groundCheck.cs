using UnityEngine;
using System.Collections;

public class groundCheck : MonoBehaviour {

    gameMaster gm;

    void Start()
    {
        gm = gameMaster.master;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Platform"))
        {
            gm.player.grounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Platform"))
        {
            gm.player.grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Platform"))
        {
            gm.player.grounded = false;
        }
    }
}
