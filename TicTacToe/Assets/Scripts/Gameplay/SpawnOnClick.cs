using UnityEngine;
using System.Collections;

public class SpawnOnClick : MonoBehaviour {

    public Transform spawnpoint;
    public GameObject itemToSpawn;
    public bool clicked = false;
    public bool oneTimeEvent = true;
    GameObject itemClone;
    
    void OnMouseDown()
    {
        //Spawn item at the given transform when mouse is clicked
        itemClone = Instantiate(itemToSpawn, spawnpoint.position, spawnpoint.rotation) as GameObject;
        //Disable the trigger if only 1 click is allowed: Defaults to true
        if (oneTimeEvent)
        {
            clicked = true;
            transform.GetComponent<Collider>().enabled = false;
        }
    }
}
