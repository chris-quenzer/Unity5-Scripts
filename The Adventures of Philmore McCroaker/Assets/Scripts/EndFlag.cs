using UnityEngine;
using System.Collections;

public class EndFlag : MonoBehaviour {

    gameMaster gm;
    public GameObject particles;
    public Transform spawn;

    bool once;

    void Start()
    {
        gm = gameMaster.master;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(gm.win && !once)
        {
            gm.WinActions();
            Instantiate(particles, spawn.position, Quaternion.Euler(-90f,0f,0f));
            once = true;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gm.win = true;
        }
    }
}
