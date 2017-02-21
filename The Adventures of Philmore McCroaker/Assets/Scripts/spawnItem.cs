using UnityEngine;
using System.Collections;

public class spawnItem : MonoBehaviour {

    //Start spawn time
    bool hasSpawnInit;
    float spawnTimerInit;
    public float spawnTimeInit;

    public float spawnTime = 1.5f;
    public int numOfObjects;
    public bool objLimit;
    int objCount;
    bool canSpawn = true;

    float spawnTimer;

    public Transform[] SpawnPoints;
    public int force = 0;
    public float gravity = 3;
    public bool canCollide = true;
    public bool hasTimedDestroy = false;
    public float destroyTime = 3;
    //public float forceX, forceY;

    public GameObject item;
    public AudioClip sound;
    AudioSource spawnSound;
    GameObject itemClone;
    Collider2D itemCol;

    //public GameObject[] item;

    void Start ()
    {
        if(sound != null)
        {
            spawnSound = GetComponent<AudioSource>();
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (sound != null)
        {
            spawnSound.clip = sound;
        }
            
        SpawnItems();
    }

    void SpawnItems()
    {
        int spawnIndex = Random.Range(0, SpawnPoints.Length);

        if (!hasSpawnInit)
        {
            spawnTimerInit += Time.deltaTime;

            if (spawnTimerInit >= spawnTimeInit)
            {
                itemClone = Instantiate(item, SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation) as GameObject;
                //itemClone.transform.parent = gameObject.transform;

                if (sound != null)
                {
                    spawnSound.Play();
                }

                itemClone.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
                itemClone.GetComponent<Rigidbody2D>().gravityScale = gravity;

                if (!canCollide)
                {
                    itemCol = itemClone.GetComponentInChildren<Collider2D>();
                    Destroy(itemCol);
                }
                if (hasTimedDestroy)
                {
                    Destroy(itemClone, destroyTime);
                }

                objCount += 1;
                hasSpawnInit = true;
            }
        }
        else
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnTime)
            {
                if (objLimit)
                {
                    if (objCount < numOfObjects)
                    {
                        itemClone = Instantiate(item, SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation) as GameObject;
                        //itemClone.transform.parent = gameObject.transform;

                        if (sound != null)
                        {
                            spawnSound.Play();
                        }

                        itemClone.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
                        itemClone.GetComponent<Rigidbody2D>().gravityScale = gravity;

                        if (!canCollide)
                        {
                            itemCol = itemClone.GetComponentInChildren<Collider2D>();
                            Destroy(itemCol);
                        }
                        if (hasTimedDestroy)
                        {
                            Destroy(itemClone, destroyTime);
                        }

                        spawnTimer = 0;
                        objCount += 1;
                    }
                    if (objCount > numOfObjects)
                    {
                        canSpawn = false;
                    }
                }
                if (!objLimit && canSpawn)
                {
                    itemClone = Instantiate(item, SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation) as GameObject;
                    //itemClone.transform.parent = gameObject.transform;

                    if (sound != null)
                    {
                        spawnSound.Play();
                    }

                    itemClone.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
                    itemClone.GetComponent<Rigidbody2D>().gravityScale = gravity;

                    if (!canCollide)
                    {
                        itemCol = itemClone.GetComponentInChildren<Collider2D>();
                        Destroy(itemCol);
                    }
                    if (hasTimedDestroy)
                    {
                        Destroy(itemClone, destroyTime);
                    }

                    spawnTimer = 0;
                }
            }
        }
    }


        /*void SpawnItems()
    {
        if (!hasSpawnInit)
        {
            spawnTimerInit += Time.deltaTime;

            if (spawnTimerInit >= spawnTimeInit)
            {
                itemClone = Instantiate(item, SpawnPoint.position, SpawnPoint.rotation) as GameObject;
                itemClone.transform.parent = gameObject.transform;

                objCount += 1;
                hasSpawnInit = true;
            }
        }
        else
        {

            int spawnIndex = Random.Range(0, SpawnPoints.Length); //set the index # of the array randomly

            itemClone = Instantiate(item, SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation) as GameObject;

            itemClone.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
            itemClone.GetComponent<Rigidbody2D>().gravityScale = gravity;

            if (!canCollide)
            {
                itemCol = itemClone.GetComponentInChildren<Collider2D>();
                Destroy(itemCol);
            }
            if (hasTimedDestroy)
            {
                Destroy(itemClone, destroyTime);
            }
        }
    }*/
}
