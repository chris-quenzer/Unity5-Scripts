//#if !UNITY_EDITOR && UNITY_WEBGL
//        WebGLInput.captureAllKeyboardInput = true;
//#endif

using UnityEngine;
using System.Collections;


public class Frog : MonoBehaviour {
    
    //Floats
    public float maxSpeed = 3;
    public float maxVertSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;

    public float scaleX = 1.0f, scaleY = 1.0f;

    public float climbSpeed;
    float climbVel;
    float gravityStore;

    //Ints
    public int life = 1;
    public int maxLife = 3;

    //Bools
    public bool grounded;
    //public bool canDoubleJump;
    public bool jumped;
    public bool dead;
    public bool win;
    public bool checkPt;
    public bool inLadder;
    public bool ladder;
    public bool onMovObj;
    public bool canJump;
    public bool isHit;
    public bool isFrozen;
    public bool hurt;
    public bool GodMode;
    public bool UnlimitedLives;
    bool deathHop;
    bool slipGrav;

    //References
    gameMaster gm;
    public Rigidbody2D rb2d;
    private Animator anim;
    private Animation frogAnims;
    public Vector3 spawn;

    public int state;
    public enum PlayerState
    {
        STATE_IDLE,
        STATE_WALKING,
        STATE_JUMPING,
        STATE_LADDER,
        STATE_DEAD
    };

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        state = (int)PlayerState.STATE_IDLE;
        gm = gameMaster.master;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = transform.Find("player").GetComponent<Animator>();
        frogAnims = gameObject.GetComponentInChildren<Animation>();
        spawn = gameObject.transform.position;
        gravityStore = rb2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen || hurt)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(wait());
        }
        else
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        anim.SetBool("Grounded", grounded);
        anim.SetBool("Dead", dead);
        anim.SetBool("Hurt", hurt);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Invincible", GodMode);

        if (!gm.dead && !win && !isFrozen && !gm.paused)
        { 
            //Flip the sprite
            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                transform.localScale = new Vector3(-scaleX, scaleY, 1);
            }

            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                transform.localScale = new Vector3(scaleX, scaleY, 1);
            }

            //print(rb2d.velocity.y);//DEBUG
            if (Input.GetButtonDown("Jump"))
            {
                if (grounded && state != (int)PlayerState.STATE_JUMPING)
                {
                    rb2d.AddForce(Vector3.up * jumpPower);
                    //canDoubleJump = true;
                }
                /*else if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * jumpPower * 0.75f);
                }*/
            }
        }

        if (!win && (life == 0))
        {
            transform.position = transform.position;
            dead = true;
            gm.dead = dead;
        }

        if (checkPt && !dead)
        {
            checkPt = false;
        }
        else if (isHit && !gm.dead)
        {
            gameObject.transform.position = spawn;
        }

        if (gm.dead)
        {
            isFrozen = false;
            //gameObject.layer = 10;
            /*foreach (Transform trans in gameObject.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = 10;
            }*/
            if (!deathHop)
            {
                maxVertSpeed = 100;
                rb2d.AddForce(Vector3.up * 1000);
                deathHop = true;
            }
            //rb2d.velocity = new Vector3(0, rb2d.velocity.y, 0);
        }

        switch (state)
        {
            case (int)PlayerState.STATE_IDLE:
                //gm.consoleMsg = "Idle";//DEBUG;
                if (Input.GetButtonDown("Jump") || (Input.GetButtonDown("Jump") && ladder))
                {
                    if (grounded)
                    {
                        state = (int)PlayerState.STATE_JUMPING;
                        //rb2d.AddForce(Vector3.up * jumpPower);
                    }
                }
                else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
                {
                    state = (int)PlayerState.STATE_WALKING;
                }
                break;

            case (int)PlayerState.STATE_JUMPING:
                //gm.consoleMsg = "Jumping";//DEBUG;
                if (Input.GetButtonDown("Jump"))
                {
                    state = (int)PlayerState.STATE_JUMPING;
                }
                else if (grounded && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
                {
                    state = (int)PlayerState.STATE_WALKING;
                }
                else if (grounded && !(Input.GetButtonDown("Jump")))
                {
                    state = (int)PlayerState.STATE_IDLE;
                }
                /*else if(inLadder && Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    state = (int)PlayerState.STATE_LADDER;
                }*/
                break;

            case (int)PlayerState.STATE_WALKING:
                //gm.consoleMsg = "Walking";//DEBUG;
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
                {
                    if (grounded && Input.GetButtonDown("Jump"))
                    {
                        state = (int)PlayerState.STATE_JUMPING;
                        //rb2d.AddForce(Vector3.up * jumpPower);
                    }
                }
                else if (Input.GetAxis("Horizontal") == 0)
                {
                    state = (int)PlayerState.STATE_IDLE;
                }
                break;
        }
        //print(state);
        //print(Input.GetAxis("Horizontal"));

        //cheats
        //transform.localScale = new Vector3(scaleX, scaleY, 1.0f);
    }

    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;
        
        
        if (!gm.dead && !win && !isFrozen && !gm.paused)
        {
            float h = Input.GetAxis("Horizontal");
            //float v = Input.GetAxis("Vertical");

            //Fake friction / easing the x speed of player
            if (grounded)
            {
                rb2d.velocity = easeVelocity;
            }
            if (ladder)
            {
                rb2d.gravityScale = 0f;

                climbVel = climbSpeed * Input.GetAxisRaw("Vertical");
                rb2d.velocity = new Vector3(rb2d.velocity.x, climbVel);
            }
            else if(!slipGrav)
            {
                rb2d.gravityScale = gravityStore;
            }

           //Moving the player
           rb2d.AddForce((Vector3.right * speed) * h);

            //Limiting the speed of the player
            if (rb2d.velocity.x > maxSpeed)
            {
                rb2d.velocity = new Vector3(maxSpeed, rb2d.velocity.y, 0);
            }
            if (rb2d.velocity.x < -maxSpeed)
            {
                rb2d.velocity = new Vector3(-maxSpeed, rb2d.velocity.y, 0);
            }
            /*if (rb2d.velocity.y > maxVertSpeed)
            {
                rb2d.velocity = new Vector3(rb2d.velocity.x, maxVertSpeed, 0);
            }
            if (rb2d.velocity.y < -maxVertSpeed)
            {
                rb2d.velocity = new Vector3(rb2d.velocity.x, -maxVertSpeed, 0);
            }*/
        }

        int layerMask = 1 << 9;
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), -Vector2.up, 1, layerMask);

        if (hit.collider != null)
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), -Vector2.up, Color.red);

            if (hit.collider.tag == "Platform" && grounded && !ladder)
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
        if (!grounded || ladder)
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(0, 0, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Platform" && ladder)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other, true);
        }
        if (other.tag == "Platform" && !ladder)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other, false);
        }
        if (other.tag == "deadZone")
        {
            if(!GodMode)
            {
                if (!isHit)
                {
                    if(!UnlimitedLives)
                    {
                        //if(gm.life > 0)
                        //{
                            gm.life -= 1;
                        print("Life Lost");
                        //}
                        isHit = true;
                        hurt = true;
                        isFrozen = true;
                    }
                    
                }
            }
        }
        if (other.tag == "Reset" && !dead)
        {
            gameObject.transform.position = spawn;
        }
        if (dead && other.tag == "Platform")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other, true);
        }
        if (other.tag == "Platform" && Mathf.Abs(transform.localRotation.z) > 0)
        {
            if (grounded)
            {
                rb2d.gravityScale = 0f;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Platform" && ladder)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other, true);
        }
        if (other.tag == "Platform" && !ladder)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other, false);
        }
        if (dead && other.tag == "Platform")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other, true);
        }
        if (other.tag == "Platform" && Mathf.Abs(transform.localRotation.z) > 0)
        {
            //print(Mathf.Abs(transform.localRotation.z));
            if (grounded)
            {
                slipGrav = true;
                rb2d.gravityScale = 0f;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "deadZone")
        {
            isHit = false;
        }
        if (other.tag == "Platform")
        {
            slipGrav = false;
            rb2d.gravityScale = gravityStore;
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        isFrozen = false;
        hurt = false;
    }
}
