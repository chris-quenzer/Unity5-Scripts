using UnityEngine;
using System.Collections;

public class camControl : MonoBehaviour {

    public static camControl cam;

    void Awake()
    {
        if (cam == null)
        {
            DontDestroyOnLoad(gameObject);
            cam = this;
        }
        else if (cam != this)
        {
            Destroy(gameObject);
        }
    }
}
