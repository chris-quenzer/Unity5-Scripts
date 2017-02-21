using UnityEngine;
using System.Collections;

public class cannonAI : MonoBehaviour {

    gameMaster gm;

    //cannon vars
    bool hasSpawnInit;
    float fireTimerInit;
    public float fireTimeInit = 0;
    public GameObject CannBase;

    public bool awake = false;
    public Transform target;
    public float wakeRange = 5;
    public float fireInterval = 2;
    public bool track = false;
    public float trackSpeed = 2;
    public float z;
    AudioSource cannonBlast;

    Transform point;
    Quaternion baseRotation;
    Quaternion targetRotation;
    float fireTimer;
    float distance;
    
    //spawning vars
    public Transform SpawnPoint;
    public int force = 0;
    public float gravity = 3;
    public bool canCollide = true;
    public bool hasTimedDestroy = false;
    public float destroyTime = 3;
    //spawned object
    public GameObject item;
    GameObject itemClone;
    Collider2D itemCol;

    void Start ()
    {
        gm = gameMaster.master;
        target = GameObject.FindGameObjectWithTag("Frog").transform;
        cannonBlast = GetComponent<AudioSource>();
        baseRotation = transform.rotation;
        baseRotation.x = 0.0f;
        baseRotation.y = 0.0f;
    }
	
	void Update ()
    {
        

        RangeCheck();
        if (track)
        {
            Rigidbody2D rb2D_A = transform.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2D_B = target.GetComponent<Rigidbody2D>();

            //float v = (force / item.GetComponent<Rigidbody2D>().mass) * Time.fixedDeltaTime;
            /*
            //attempt1
            Vector3 totarget = target.position - transform.position;

            float a = Vector3.Dot(rb2D_B.velocity, rb2D_B.velocity) - (v * v);
            float b = 2 * Vector3.Dot(rb2D_B.velocity, totarget);
            float c = Vector3.Dot(totarget, totarget);

            float p = -b / (2 * a);
            float q = (float)Mathf.Sqrt((b * b) - 4 * a * c) / (2 * a);

            float t1 = p - q;
            float t2 = p + q;
            float t;

            if (t1 > t2 && t2 > 0)
            {
                t = t2;
            }
            else
            {
                t = t1;
            }
            */
            /*
            //attempt2
            float a = (rb2D_B.velocity.x * rb2D_B.velocity.x) + (rb2D_B.velocity.y * rb2D_B.velocity.y) - (v * v);
            float b = 2 * (rb2D_B.velocity.x * (target.position.x - transform.position.x)) + (rb2D_B.velocity.y * (target.position.y - transform.position.y));
            float c = ((rb2D_B.velocity.x - transform.position.x) * (rb2D_B.velocity.x - transform.position.x)) + ((rb2D_B.velocity.y - transform.position.y) * (rb2D_B.velocity.y - transform.position.y));

            float disc = (b * b) - 4 * a * c;

            float t1 = (-b + Mathf.Sqrt(disc)) / (2 * a);
            float t2 = (-b - Mathf.Sqrt(disc)) / (2 * a);
            float t;

            if (t1 > t2 && t2 > 0)
            {
                t = t2;
            }
            else
            {
                t = t1;
            }

            Vector3 aimSpot = new Vector3(-(t * rb2D_B.velocity.x + rb2D_B.position.x), (t * rb2D_B.velocity.y + rb2D_B.position.y));
            Vector3 bulletPath = aimSpot - transform.position;
            //float timeToImpact = bulletPath.Length() / bullet.speed;//speed must be in units per second
            */
            //cheaty way to account for player moving
            Vector3 aimSpot;
            if (rb2D_B.velocity.x > 0.01f)
            {
                aimSpot = new Vector3(target.position.x + 5, target.position.y);
            }
            else if (rb2D_B.velocity.x < 0.0f)
            {
                aimSpot = new Vector3(target.position.x - 5, target.position.y);
            }
            else
            {
                aimSpot = target.position;
            }
            Quaternion newRotation = Quaternion.LookRotation(transform.position - aimSpot, Vector3.forward);
            newRotation.x = 0.0f;
            newRotation.y = 0.0f;

            z = transform.localEulerAngles.z;

            targetRotation = newRotation;
            if((z >= 0 && z <= 90) || (z >= 270 && z <= 360) || (z <= 0) || (z == 360))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * trackSpeed);
            }
            else if (z < 270 && z > 180)
            {
                transform.localEulerAngles = new Vector3(0, 0, 270);
                //transform.rotation = Quaternion.Euler(0,0,270);
            }
            else if (z > 90 && z < 180)
            {
                transform.localEulerAngles = new Vector3(0, 0, 89.9999f);
                //transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation, Time.deltaTime * trackSpeed);
            }

        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation, Time.deltaTime * trackSpeed);
        }
    }

    void RangeCheck()
    {
        float angle = 90;
        float angleAway = Vector3.Angle(CannBase.transform.up, target.transform.position - CannBase.transform.position);
        
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < wakeRange && angleAway < angle)
            {
                track = true;
            }
            else if (distance > wakeRange || angleAway > angle)
            {
                track = false;
            }
    }

    public void Shoot(bool shoot)
    {
        if (!hasSpawnInit)
        {
            fireTimerInit += Time.deltaTime;

            if (fireTimerInit >= fireTimeInit)
            {
                if (shoot)
                {
                    itemClone = Instantiate(item, SpawnPoint.position, SpawnPoint.rotation) as GameObject;
                    cannonBlast.Play();
                    itemClone.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
                    itemClone.GetComponent<Rigidbody2D>().gravityScale = gravity;

                    fireTimer = 0;

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

                hasSpawnInit = true;
            }
        }
        else
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= fireInterval)
            {
                if (shoot)
                {
                    itemClone = Instantiate(item, SpawnPoint.position, SpawnPoint.rotation) as GameObject;
                    cannonBlast.Play();
                    itemClone.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
                    itemClone.GetComponent<Rigidbody2D>().gravityScale = gravity;

                    fireTimer = 0;

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
            }
        }
    }
}
