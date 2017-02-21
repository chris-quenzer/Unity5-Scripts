using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

    public float speed = 8;
    Vector3 targetPos;
    Vector3 playerPos;
    GameObject ballObj;

	//void Start ()
    //{
        
	//}
	
	void Update ()
    {
        ballObj = GameObject.FindGameObjectWithTag("Ball");
     
        if (ballObj != null && ballObj.transform.position.x > 0)
        {
            targetPos = Vector3.Lerp(gameObject.transform.position, ballObj.transform.position, Time.deltaTime * speed);
            playerPos = new Vector3(-20, Mathf.Clamp(targetPos.y, -12.5f, 12.5f), 0);
            gameObject.transform.position = new Vector3(20, playerPos.y, 0);
        }
        if (ballObj != null && ballObj.transform.position.x < 0)
        {
            targetPos = Vector3.Lerp(gameObject.transform.position, new Vector3(20,0,0), Time.deltaTime * (speed / 2));
            playerPos = new Vector3(-20, Mathf.Clamp(targetPos.y, -12.5f, 12.5f), 0);
            gameObject.transform.position = new Vector3(20, playerPos.y, 0);
        }

    }
}
