using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

    //publics
    public Camera cam;
    public bool toggleZoom = false;
    public int MaxFov = 60;
    public int MinFov = 30;
    //privates
    bool zoomed = true;

    void Update()
    {
        if(toggleZoom)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && !zoomed)
            {
                cam.fieldOfView = MinFov;
                zoomed = true;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && zoomed)
            {
                cam.fieldOfView = MaxFov;
                zoomed = false;
            }
        }
        else if(!toggleZoom)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                cam.fieldOfView = MinFov;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                cam.fieldOfView = MaxFov;
            }
        }
        
    }
}
