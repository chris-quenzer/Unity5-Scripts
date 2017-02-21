using UnityEngine;
using System.Collections;

public class playerScore : MonoBehaviour {

    public TextMesh currSco;
    public GameObject ballpref;
    public Transform paddleObj;

    GameObject ball;
    int pScore;
	
	void Update ()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        currSco.text = "" + pScore;

        if(pScore == 7)
        {
            Application.LoadLevel(2);
        }

	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            pScore += 1;
            Destroy(ball);
            (Instantiate(ballpref, new Vector3(paddleObj.transform.position.x + 2, paddleObj.position.y, 0), Quaternion.identity) as GameObject).transform.parent = paddleObj;
        }
    }
}
