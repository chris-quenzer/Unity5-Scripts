using UnityEngine;
using System.Collections;

public class PlatformTrigger : MonoBehaviour {

    gameMaster gm;

    public BoxCollider2D platform;
    public bool oneWay = false;

    // Use this for initialization
    void Start()
    {
        gm = gameMaster.master;
    }

    // Update is called once per frame
    void Update()
    {
        platform.enabled = !oneWay;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        oneWay = true;
        /*if (other.tag == "GroundCheck")
        {
            gm.player.grounded = false;
        }*/
    }

    void OnTriggerStay2D(Collider2D other)
    {
        oneWay = true;
        /*if (other.tag == "GroundCheck")
        {
            gm.player.grounded = false;
        }*/
    }

    void OnTriggerExit2D(Collider2D other)
    {
        oneWay = false;
    }
}
