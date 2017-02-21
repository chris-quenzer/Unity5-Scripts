using UnityEngine;
using System.Collections;

public class activateSlide : MonoBehaviour {

    AnimController animControl;

    void Start()
    {
        animControl = GameObject.Find("GameMaster").GetComponent<AnimController>();
    }

    void OnMouseDown()
    {
        animControl.slideAnimation();
    }
}
