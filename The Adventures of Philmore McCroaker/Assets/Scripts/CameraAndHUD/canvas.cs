using UnityEngine;
using System.Collections;

public class canvas : MonoBehaviour {

    public static canvas canvasControl;

    public Canvas canv;

    void OnLevelWasLoaded(int level)
    {
        canv.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

}
