using UnityEngine;
using System.Collections;

public class pointToVel : MonoBehaviour {

    Rigidbody rbdy;

    void Start()
    {
        rbdy = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 velocityVector = transform.forward * (rbdy.velocity.z);
        //transform.LookAt(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, velocityVector.z));
    }
}
