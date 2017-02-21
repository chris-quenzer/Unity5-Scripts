using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    //Spawn Item attributes
    public GameObject item;
    //public GameObject[] items;//multiple types of ammo?
    public AudioClip sound;
    AudioSource spawnSound;
    GameObject itemClone;
    Collider itemCol;
    //Initial spawn time control
    bool hasSpawnInit;
    public float spawnTimeInit = 0.0f;
    float spawnTimerInit = 0.0f;
    //Fire time between shots
    public float fireTime = 1.5f;
    float fireTimer = 0.0f;
    //Ammo control
    public int ammo;
    public int maxAmmo;
    public bool hasAmmoCap = false;
    int ammoCount;
    bool canShoot = true;
    bool fire = true;
    //Spawn attributes
    public Transform[] SpawnPoints;
    public int force = 0;
    public bool isGravity = true;
    public bool canCollide = true;
    public bool hasTimedDestroy = true;
    public float destroyTime = 3;
    //path prediction
    LineRenderer path;
    public bool predictPath = false;
    public float timeResolution = 0.02f;
    public float maxPredictTime = 10.0f;

    void Start()
    {
        if (sound != null)
        {
            spawnSound = GetComponent<AudioSource>();
        }
        if (GetComponent<LineRenderer>())
        {
            path = GetComponent<LineRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ammo = Mathf.Clamp(ammo, 0, maxAmmo);
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeScale != 0)
        {
            if (fireTimer <= fireTime && fire)
            {
                if (hasAmmoCap)
                {
                    if (ammo > 0)
                    {
                        Fire();
                        ammo -= 1;
                        fire = false;
                    }
                }
                else
                {
                    Fire();
                    fire = false;
                }
            }
        }
        //print(ammo);//debug

        if(!fire)
        {
            fireTimer += Time.deltaTime;
        }
        if(fireTimer >= fireTime)
        {
            fireTimer = 0.0f;//reset fire timer
            fire = true;//can fire again
        }
        //print(fireTimer);//debug

        if (predictPath && path != null)
        {
            trajPredict(isGravity);//creates curved or linear trajectory path based on gravity/no gravity
        }
    }
    /*
    void FixedUpdate()
    {
        
    }
    */
    void Fire()
    {
        int spawnIndex = Random.Range(0, SpawnPoints.Length);

        if (sound != null)
        {
            spawnSound.clip = sound;
        }

        itemClone = Instantiate(item, SpawnPoints[spawnIndex].position, SpawnPoints[spawnIndex].rotation) as GameObject;
        //itemClone.transform.parent = gameObject.transform;

        if (sound != null)
        {
            spawnSound.Play();
        }

        itemClone.GetComponent<Rigidbody>().AddForce(transform.forward * force);

        if (!isGravity)
        {
            itemClone.GetComponent<Rigidbody>().useGravity = false;
        }

        if (!canCollide)
        {
            itemCol = itemClone.GetComponentInChildren<Collider>();
            Destroy(itemCol);
        }
        if (hasTimedDestroy)
        {
            Destroy(itemClone, destroyTime);
        }
        ammoCount -= 1;
    }

    void trajPredict(bool isGravity)
    {
        //velocity = (force / mass) * t
        float v = (force / item.GetComponent<Rigidbody>().mass) * Time.fixedDeltaTime;
        Vector3 velocityVector = transform.forward * v;

        path.SetVertexCount((int)(maxPredictTime / timeResolution));

        int index = 0;

        Vector3 currentPosition = transform.position;

        for (float t = 0.0f; t < maxPredictTime; t += timeResolution)
        {
            path.SetPosition(index, currentPosition);
            currentPosition += velocityVector * timeResolution;
            if(isGravity)
            {
                velocityVector += Physics.gravity * timeResolution;
            }
            index++;
        }
    }
}
