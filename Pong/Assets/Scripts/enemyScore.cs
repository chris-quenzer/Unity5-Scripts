using UnityEngine;
using System.Collections;

public class enemyScore : MonoBehaviour {

    public TextMesh currSco;
    public GameObject ballpref;
    public Transform paddleObj;

    GameObject ball;
    int eScore;

	//void Start ()
    //{
	
	//}
	
	void Update ()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        currSco.text = "" + eScore;

        if (eScore == 7)
        {
            Application.LoadLevel(3);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            eScore += 1;
            Destroy(ball);
            (Instantiate(ballpref, new Vector3(paddleObj.transform.position.x + 2, paddleObj.position.y, 0), Quaternion.identity) as GameObject).transform.parent = paddleObj;
        }
    }
}
