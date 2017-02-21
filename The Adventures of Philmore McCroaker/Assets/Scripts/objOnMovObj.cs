using UnityEngine;
using System.Collections;

public class objOnMovObj : MonoBehaviour {

    Rigidbody2D objRb2d;

    void OnTriggerEnter2D(Collider2D other)
    {
        //objRb2d = other.transform.parent.gameObject.GetComponent<Rigidbody2D>();
        if(other.transform.parent.gameObject.GetComponent<Rigidbody2D>())
        {
            if (other.tag != "Platform")
            {
                if (gameObject.transform.parent != null)
                {
                    other.transform.parent.gameObject.transform.parent = gameObject.transform.parent;//assuming trigger/collider is a child of the object
                }
            }
        }
           
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //objRb2d = other.transform.parent.gameObject.GetComponent<Rigidbody2D>();
        if (other.transform.parent.gameObject.GetComponent<Rigidbody2D>())
        {
            if (other.tag != "Platform")
            {
                if (gameObject.transform.parent != null)
                {
                    other.transform.parent.gameObject.transform.parent = gameObject.transform.parent;//assuming trigger/collider is a child of the object
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //if(other.transform.parent.gameObject.GetComponent<Rigidbody2D>())

        //objRb2d = other.transform.parent.gameObject.GetComponent<Rigidbody2D>();
        if (other.transform.parent.gameObject.GetComponent<Rigidbody2D>())
        {
            if (other.tag != "Platform")
            {
                other.transform.parent.gameObject.transform.parent = null;
            }
        }
    }
}
