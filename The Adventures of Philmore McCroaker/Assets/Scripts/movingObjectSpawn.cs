using UnityEngine;
using System.Collections;

public class movingObjectSpawn : MonoBehaviour {

    public movingPlatform[] movObjArr;

    //Start spawn time
    bool hasSpawnInit;
    float spawnTimerInit;
    public float spawnTimeInit;

    public Transform SpawnPoint;
    public float spawnTime = 4.0f;
    public int numOfObjects;
    public bool objLimit;
    int objCount;
    bool canSpawn = true;

    public Transform[] Waypoints;
    public float speed;

    float spawnTimer;

    public GameObject item;
    GameObject itemClone;

    void Start()
    {
        objCount = 0;
    }

    void Update()
    {
        SpawnItems();

        movObjArr = gameObject.GetComponentsInChildren<movingPlatform>();
        for (int i = 0; i < movObjArr.Length; i++)
        {

            movObjArr[i].Waypoints = Waypoints;
            movObjArr[i].speed = speed;
            if(movObjArr[i].trigger == true)
            {
                Destroy(movObjArr[i]);
            }

        }
    }

    void SpawnItems()
    {
        if(!hasSpawnInit)
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
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnTime)
            {
                if (objLimit)
                {
                    if (objCount < numOfObjects)
                    {
                        itemClone = Instantiate(item, SpawnPoint.position, SpawnPoint.rotation) as GameObject;
                        itemClone.transform.parent = gameObject.transform;

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
                    itemClone = Instantiate(item, SpawnPoint.position, SpawnPoint.rotation) as GameObject;
                    itemClone.transform.parent = gameObject.transform;

                    spawnTimer = 0;
                }
            }
        }


        
    }
}
