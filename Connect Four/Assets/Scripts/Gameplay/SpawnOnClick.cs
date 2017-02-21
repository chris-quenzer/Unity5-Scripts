using UnityEngine;
using System.Collections;

public class SpawnOnClick : MonoBehaviour {

    GameMaster gm;
    public Transform spawnpoint;
    public GameObject itemToSpawn;
    public bool clicked = false;
    public bool oneTimeEvent = true;
    public GameObject itemClone;
    
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    void OnMouseDown()
    {
        if(gm.canSpawn)
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
}
