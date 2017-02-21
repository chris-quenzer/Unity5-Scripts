using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    gameMaster gm;

    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    //debugging
    public Vector3 playerPos;

    void Start ()
    {
        gm = gameMaster.master;
        player = GameObject.FindGameObjectWithTag("Frog");
    }
	
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        if (!gm.dead)
        {
            transform.position = new Vector3(posX, posY, transform.position.z);
        }
        else if (gm.dead)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                                            Mathf.Clamp(transform.position.y,minCameraPos.y, maxCameraPos.y),
                                            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
        

        //debug
        playerPos = player.transform.position;
    }
}
