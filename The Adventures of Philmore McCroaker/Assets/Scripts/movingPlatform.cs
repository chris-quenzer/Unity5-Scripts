using UnityEngine;
using System.Collections;

public class movingPlatform : MonoBehaviour {

    private Frog player;

    public Transform[] Waypoints;
    public float speed = 2;
    public int CurrentPoint = 0;
    public bool trigger;

    //public bool onMovPlatform;

    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position != Waypoints[CurrentPoint].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentPoint].transform.position, speed * Time.deltaTime);
        }

        if (transform.position == Waypoints[CurrentPoint].transform.position)
        {
            CurrentPoint += 1;
        }
        if (CurrentPoint >= Waypoints.Length)
        {
            CurrentPoint = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlatformKill")
        {
            Destroy(gameObject);
        }
    }
}
