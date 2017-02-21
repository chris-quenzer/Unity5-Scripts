using UnityEngine;
using System.Collections;

public class TankHead : MonoBehaviour
{
    //Public Vars
    public float speed;
    MouseAim mouseAim;

    //Private Vars
    private Vector3 mousePosition;
    private Vector3 direction;
    private float distanceFromObject;

    void Start()
    {
        mouseAim = GameObject.Find("Main Camera").GetComponent<MouseAim>();
    }

    void FixedUpdate()
    {
        //Grab the current mouse position on the screen
        mousePosition = mouseAim.mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - mouseAim.mainCam.transform.position.z));

        //Rotates toward the mouse
        //transform.eulerAngles = new Vector3(0, Mathf.Atan2((mouseAim.target.x - transform.position.x), (mouseAim.target.z - transform.position.z)) * Mathf.Rad2Deg, 0);

        Vector3 targetPostition = new Vector3(mouseAim.target.x, transform.position.y, mouseAim.target.z);
        transform.LookAt(targetPostition);

        //Vector3 direction = mouseAim.target - transform.position;
        //Quaternion toRotation = Quaternion.FromToRotation(transform.up, direction);
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.time);

        //Judge the distance from the object and the mouse
        distanceFromObject = (Input.mousePosition - mouseAim.mainCam.WorldToScreenPoint(transform.position)).magnitude;
    }
}
