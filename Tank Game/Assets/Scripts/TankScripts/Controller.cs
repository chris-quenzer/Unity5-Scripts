using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    //publics
    public float maxSpeed = 150f;
    public float speed = 50.0f;
    public float boostSpeedMult = 1.25f;
    public float rotSpeed = 50.0f;
    public float boostTime = 2.5f;
    public float boostCooldown = 5.0f;

    //privates
    Rigidbody rbdy;
    Transform body;
    Quaternion rot;
    float storedSpd;
    float boostSpeed;
    float boostTimer;
    float boostCoolTimer = 0.0f;
    bool coolDown = false;

    // Use this for initialization
    void Start ()
    {
        storedSpd = speed;
        boostSpeed = speed * boostSpeedMult;
        rbdy = gameObject.GetComponent<Rigidbody>();
        body = gameObject.transform.FindChild("Body");
        rot = transform.rotation;
    }

    void Update()
    {
        //Rotation
        if (Input.GetKey(KeyCode.A))
        {
            body.Rotate(Vector3.down * rotSpeed * Time.deltaTime);
        }
            
        if (Input.GetKey(KeyCode.D))
        {
            body.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }

        //Speed Boost
        if (Input.GetKey(KeyCode.LeftShift))//boost
        {
            //if boosting, start countdown to end of boost
            boostTimer += Time.deltaTime;
            if((boostTimer <= boostTime) && boostCoolTimer == 0)//while less than boost time limit, and cooldown is not active, keep boosting
            {
                speed = boostSpeed;
            }
            else
            {
                speed = storedSpd;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))//stop boosting
        {
            speed = storedSpd;
        }
        else if(!Input.GetKey(KeyCode.LeftShift) && !coolDown)//if not boosting
        {
            if (boostTimer > 0)
            {
                boostTimer -= (Time.deltaTime / 10);//if you have boosted, take *some* time away from boost timer while not boosting and not cooling down
            }
        }
        if (boostTimer >= boostTime)//if you hit the boost limit, start the cooldown timer
        {
            coolDown = true;
        }
        if(coolDown)
        {
            boostCoolTimer += Time.deltaTime;
            if (boostCoolTimer >= boostCooldown)//if the cooldown reaches the cool time, reset both timers
            {
                boostTimer = 0.0f;
                boostCoolTimer = 0.0f;
                coolDown = false;
            }
            //print(boostCoolTimer);//debug
        }
        print(boostTimer);//debug
    }

	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 easeVelocity = rbdy.velocity;
        easeVelocity.y = rbdy.velocity.y;
        easeVelocity.z *= 0.9f;
        easeVelocity.x *= 0.9f;

        float h = Input.GetAxis("Vertical");

        rbdy.velocity = easeVelocity;

        //Translation
        rbdy.AddForce((body.forward * speed) * h);
        /*
        if (Input.GetAxis("Vertical") > 0.1f)
        {
            rbdy.AddForce((body.forward * speed));
        }
        if (Input.GetAxis("Vertical") < 0.1f)
        {
            rbdy.AddForce((body.forward * -speed));
        }
        */
        //Limiting the speed of the player
        if (rbdy.velocity.z > maxSpeed)
        {
            rbdy.velocity = new Vector3(rbdy.velocity.x, rbdy.velocity.y, maxSpeed);
        }
        if (rbdy.velocity.z < -maxSpeed)
        {
            rbdy.velocity = new Vector3(rbdy.velocity.x, rbdy.velocity.y, -maxSpeed);
        }
    }
}
