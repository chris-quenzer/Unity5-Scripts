using UnityEngine;
using System.Collections;

public class MouseAim : MonoBehaviour {

    public GameObject reticle;
    public Camera mainCam;
    public RaycastHit hit;
    public Vector3 target;
    Vector3 rayEnd;

    GameObject aimClone;
    bool activeCurser = false;

    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        rayEnd = ray.origin + (ray.direction * 10000);

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow); //Debug

        // Do something with the object that was hit by the raycast
        if (Physics.Raycast(ray, out hit))
        {
            if (!activeCurser)
            {
                aimClone = Instantiate(reticle);
                activeCurser = true;
            }
            
            if (hit.collider.tag != "NoAim") //Can draw dot on
            {
                target = hit.point;
            }
            if (hit.collider.tag == "NoAim") //Cannot draw dot on
            {
                Destroy(aimClone);
                target = rayEnd;
                activeCurser = false;
            }
        }//else do something with no raycast hit
        else if(!Physics.Raycast(ray, out hit))
        {
            target = rayEnd;
            Destroy(aimClone);
            activeCurser = false;
        }

        if (activeCurser)
        {
            aimClone.transform.position = hit.point;
        }
    }
}
