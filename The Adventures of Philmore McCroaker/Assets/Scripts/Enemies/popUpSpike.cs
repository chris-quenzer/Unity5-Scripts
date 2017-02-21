using UnityEngine;
using System.Collections;

public class popUpSpike : MonoBehaviour {

    private Frog player;
    private Animator anim;
    Transform underSpike;

    public bool entered;
    public bool active;
    public bool exit;

    // Use this for initialization
    void Start()
    {
        underSpike = gameObject.transform.parent;
        player = gameMaster.master.player;
        anim = underSpike.Find("spike_single").GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("Entered", entered);
        anim.SetBool("Active", active);
        anim.SetBool("Exit", exit);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            exit = false;
            entered = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        entered = false;
        active = false;
        exit = true;
    }

}
