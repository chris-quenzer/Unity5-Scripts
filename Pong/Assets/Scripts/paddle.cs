using UnityEngine;
using System.Collections;

public class paddle : MonoBehaviour {

    public float paddleSpeed = 1;
    public Vector3 playerPos;
	
	//void Start ()
    //{
	    
	//}
	
	void Update ()
    {
        float yPos = gameObject.transform.position.y + (Input.GetAxis("Vertical") * paddleSpeed);
        playerPos = new Vector3(-20, Mathf.Clamp(yPos, -12.5f, 12.5f), 0);
        gameObject.transform.position = playerPos;
	}
}
