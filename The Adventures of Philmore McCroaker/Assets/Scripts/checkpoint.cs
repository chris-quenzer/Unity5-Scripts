using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour {

    gameMaster gm;

    //private Frog player;
    private Animator anim;

    public bool entered;
    public bool active;

    // Use this for initialization
    void Start ()
    {
        gm = gameMaster.master;
        anim = gameObject.GetComponentInParent<Animator>();
    }

    void Update()
    {
        anim.SetBool("Entered", entered);
        anim.SetBool("Active", active);
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            gm.player.spawn = gameObject.transform.position;
            gm.player.checkPt = true;
            entered = true;
            active = true;

            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
