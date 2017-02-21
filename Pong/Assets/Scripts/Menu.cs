using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    void OnMouseDown()
    {
        transform.localScale *= 0.9f;
    }

    void OnMouseUp()
    {
        Application.LoadLevel(0);
    }
}
