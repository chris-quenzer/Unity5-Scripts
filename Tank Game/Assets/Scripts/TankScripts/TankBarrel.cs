using UnityEngine;
using System.Collections;

public class TankBarrel : MonoBehaviour {

    //Public Vars
    public float speed;
    public GameObject curser;
    MouseAim mouseAim;

    //Private Vars
    Vector3 mousePosition;
    Vector3 direction;
    float distanceFromObject;
    GameObject aimClone;
    
    void Start()
    {
        mouseAim = GameObject.Find("Main Camera").GetComponent<MouseAim>();
    }
    /*
    void Update()
    {
        
    }
    */
    void FixedUpdate()
    {
        //Grab the current mouse position on the screen
        mousePosition = mouseAim.mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - mouseAim.mainCam.transform.position.z));

        //Rotates toward the mouse
        //transform.eulerAngles = new Vector3(Mathf.Atan2((transform.position.y - mouseAim.target.y), (mouseAim.target.z - transform.position.z)) * Mathf.Rad2Deg, transform.eulerAngles.y, 0);

        Vector3 targetPostition = new Vector3(mouseAim.target.x, mouseAim.target.y, mouseAim.target.z);
        transform.LookAt(targetPostition);
        //if (transform.eulerAngles.x < 70)
        //{
        //    transform.eulerAngles = new Vector3(70, transform.eulerAngles.y, transform.eulerAngles.z);
        //}

        //print(transform.eulerAngles); //debug
        
        //Judge the distance from the object and the mouse
        distanceFromObject = (Input.mousePosition - mouseAim.mainCam.WorldToScreenPoint(transform.position)).magnitude;
    }
}
