using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
