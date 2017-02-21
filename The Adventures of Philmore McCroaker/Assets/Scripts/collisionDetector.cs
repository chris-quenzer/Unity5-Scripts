using UnityEngine;
using System.Collections;

public class collisionDetector : MonoBehaviour
{

    public bool hitWall;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            hitWall = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            hitWall = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            hitWall = false;
        }
    }
}
