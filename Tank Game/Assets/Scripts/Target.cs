using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    //publics
    public float appearTime = 10.0f;

    //privates
    float timer;
    bool hit = false;
    /*
    void Start ()
    {
	
	}
	*/
	void Update ()
    {
	    if(hit)
        {
            Disappear();
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Weapon")
        {
            hit = true;
        }
    }

    void Disappear()
    {
        timer += Time.deltaTime;
        print(timer);
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        //StartCoroutine(wait());
        

        if (timer >= appearTime)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
            
            hit = false;
            timer = 0.0f;
        }
        //Destroy(gameObject);
    }
    /*
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        
    }
    */

}
