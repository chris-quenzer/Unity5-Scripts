﻿using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {



    void OnMouseDown()
    {
        transform.localScale *= 0.9f;
    }

    void OnMouseUp()
    {
        Application.LoadLevel(1);
    }
}
