using UnityEngine;
using System.Collections;

public class Croc : MonoBehaviour {

    public float maxLife = 2;
    public float life = 2;
    public float speed = 1;
    public float maxSpeed = 0.01f;
    public int direction = 1;
    public bool alwaysAwake;
    public bool grounded;
    public objGroundCheck groundCheckObj;
    public playerAttackZone biteZone;
    public collisionDetector wallCheck;
    public checkPlatformExit platformCheck;
    public playerRepel hitBox;
    public Transform target;
    public float wakeRange = 7;
    public float distance;

    public float scaleX = 1;
    public float scaleY = 1;

    float pauseTimer = 0;
    float pauseTime;

    float awakeTimer = 0;
    float awakeTime = 30.0f;

    //References
    gameMaster gm;
    Rigidbody2D rb2d;
    Animator anim;

    public int state;
    public enum CrocState
    {
        STATE_SLEEPING,
        STATE_PAUSE, 
        STATE_WALKING,
        STATE_IDLE,
        STATE_BITE,
        STATE_HURT,
        STATE_DEAD
    };

    //state bools
    public bool bite;
    public bool awake;
    public bool dead;
    public bool hurt;
    public bool idle;
    public bool pause;
    //public bool walk;

    public bool awakeTimeOver;

    void Start ()
    {
        gm = gameMaster.master;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
        groundCheckObj = gameObject.GetComponentInChildren<objGroundCheck>();
        wallCheck = gameObject.GetComponentInChildren<collisionDetector>();
        platformCheck = gameObject.GetComponentInChildren<checkPlatformExit>();
        biteZone = gameObject.GetComponentInChildren<playerAttackZone>();
        hitBox = gameObject.GetComponentInChildren<playerRepel>();
        target = GameObject.FindGameObjectWithTag("Frog").transform;

        if(alwaysAwake)
        {
            awake = true;
            state = (int)CrocState.STATE_WALKING;
        }
        else
        {
            state = (int)CrocState.STATE_SLEEPING;
        }

        pauseTime = Random.Range(3, 10);
    }
	
	void Update ()
    {
        if(life > 0)
        {
            life = maxLife - hitBox.repelCount;
            if(hitBox.repelled)
            {
                hurt = true;
                state = (int)CrocState.STATE_HURT;
            }
        }
        if(life < 1)
        {
            state = (int)CrocState.STATE_DEAD;
        }

        anim.SetBool("Pause", pause);
        anim.SetBool("Idle", idle);
        anim.SetBool("Bite", bite);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Awake", awake);
        anim.SetBool("Hurt", hurt);
        anim.SetBool("Dead", dead);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        RangeCheck();
        grounded = groundCheckObj.grounded;

        if (state == (int)CrocState.STATE_SLEEPING || state == (int)CrocState.STATE_DEAD || state == (int)CrocState.STATE_HURT)
        {
            biteZone.checkForAttack(false);
        }
        
        switch (state)
        {
            case (int)CrocState.STATE_SLEEPING:       
                if (awake)
                {
                    pause = true;
                    state = (int)CrocState.STATE_PAUSE;
                    //awakeTimer();
                }
                break;

            case (int)CrocState.STATE_PAUSE:
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("croc_walk"))
                {
                    pause = false;
                    state = (int)CrocState.STATE_WALKING;
                }
                break;

            case (int)CrocState.STATE_IDLE:
                StartCoroutine(idleWait());
                if (bite)
                {
                    idle = false;
                    bite = true;
                    state = (int)CrocState.STATE_BITE;
                }
                else if (!idle)
                {
                    state = (int)CrocState.STATE_WALKING;
                }
                break;

            case (int)CrocState.STATE_BITE:
                
                if (!bite)
                {
                    state = (int)CrocState.STATE_WALKING;
                }
                break;

            case (int)CrocState.STATE_WALKING:
                if (idle)
                {
                    state = (int)CrocState.STATE_IDLE;
                }
                else if(pause)
                {
                    state = (int)CrocState.STATE_PAUSE;
                }
                else if (!awake)
                {
                    state = (int)CrocState.STATE_SLEEPING;
                }
                break;

            case (int)CrocState.STATE_HURT:
                hurt = false;
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("croc_walk"))
                {
                    pause = false;
                    state = (int)CrocState.STATE_WALKING;
                }
                break;

            case (int)CrocState.STATE_DEAD:
                dead = true;
                StartCoroutine(disable());
                break;
        }

        //Flip the sprite (whole gameObject)
        if (direction < 0)
        {
            transform.localScale = new Vector3(-scaleX, scaleY, 1);
        }

        if (direction > 0)
        {
            transform.localScale = new Vector3(scaleX, scaleY, 1);
        }

        if(state == (int)CrocState.STATE_WALKING)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer > pauseTime)
            {
                pause = true;
                //anim.SetBool("Pause", pause);
                pauseTimer = 0;
                pauseTime = Random.Range(3, 10);
            }
            //print(pauseTimer);
            //print(pauseTime);
        }

        if (awake)
        {
            if (!alwaysAwake)
            {
                awakeTimer += Time.deltaTime;
                if (awakeTimer > awakeTime)
                {
                        awakeTimeOver = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        //Fake friction / easing the x speed
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        if(state == (int)CrocState.STATE_WALKING && grounded)
        {
            //Moving
            rb2d.AddForce((Vector3.right * speed) * direction);

            //Limiting the speed
            if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector3(maxSpeed, rb2d.velocity.y, 0);
            }
            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector3(-maxSpeed, rb2d.velocity.y, 0);
            }

            changeDir();
        }
    }

    void RangeCheck()
    {
        //float angle = 90;
        //float angleAway = Vector3.Angle(CannBase.transform.up, target.transform.position - CannBase.transform.position);

        distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < wakeRange && (Mathf.Abs(target.GetComponent<Rigidbody2D>().velocity.x) > 3 || gm.player.state == (int)Frog.PlayerState.STATE_JUMPING))
        {
            awakeTimer = 0;
            awake = true;
        }
        else if (distance > wakeRange && awakeTimeOver)
        {
            awake = false;
            awakeTimeOver = false;
        }
    }

    void changeDir()
    {
        bool dirChanged = false;

        if ((wallCheck.hitWall || (rb2d.velocity.x == 0 && state == (int)CrocState.STATE_WALKING)))
        {
            idle = true;
            state = (int)CrocState.STATE_IDLE;
            dirChanged = true;
        }
        else if(platformCheck != null)
        {
            if (platformCheck.platExit)
            {
                idle = true;
                state = (int)CrocState.STATE_IDLE;
                dirChanged = true;
            }
        }

        if(dirChanged)
        {
            direction = -direction;
            dirChanged = false;
        }
    }

    IEnumerator idleWait()
    {
        yield return new WaitForSeconds(2.0f);
        idle = false;
    }
        
    IEnumerator disable()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
