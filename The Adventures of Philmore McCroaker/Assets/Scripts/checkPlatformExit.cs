using UnityEngine;
using System.Collections;

public class checkPlatformExit : MonoBehaviour {

    public bool platExit;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            platExit = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            platExit = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            platExit = true;
        }
    }
}
