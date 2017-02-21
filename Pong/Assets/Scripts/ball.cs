using UnityEngine;
using System.Collections;

public class ball : MonoBehaviour {

    public float ballVelocity = 3000;

    Rigidbody rb;
    bool isPlay, bonusDone = false;
    int randInt;
    GameObject bonus, message;

    void Awake ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        randInt = Random.Range(1, 2);
	}
	
	void Update ()
    {
        message = GameObject.Find("startMessage");

        if (Input.GetKeyDown(KeyCode.Space) == true && isPlay == false)
        {
            Destroy(message);

            transform.parent = null;
            isPlay = true;
            rb.isKinematic = false; //uncheck box in unity
            if(randInt == 1)
            {
                rb.AddForce(new Vector3(ballVelocity, ballVelocity, 0));
            }
            if (randInt == 2)
            {
                rb.AddForce(new Vector3(-ballVelocity, -ballVelocity, 0));
            }
        }

	}

    /*
    void OnTriggerEnter(Collider other)
    {
        //bonus = GameObject.Find("superBounce");

        if (other.tag == "Bonus" && !bonusDone)
        {
            ballVelocity = (ballVelocity * 2);
            bonusDone = true;
        }
    }*/
}
