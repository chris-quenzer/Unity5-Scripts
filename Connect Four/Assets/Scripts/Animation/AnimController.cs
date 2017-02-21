using UnityEngine;
using System.Collections;

public class AnimController : MonoBehaviour {

    Animator slider;
    UI ui;
    public bool playSlider;

	void Start ()
    {
        slider = GameObject.FindGameObjectWithTag(Tags.slide).GetComponent<Animator>();
        ui = GameObject.Find("Main Camera").GetComponent<UI>();
    }
	
	public void slideAnimation ()
    {
        playSlider = true;
        slider.SetBool("PlaySlider", playSlider);
        GameObject.Find("COL_bottom").GetComponent<Collider>().enabled = false;
        StartCoroutine(waitForSlider());
    }

    IEnumerator waitForSlider()
    {
        yield return new WaitForSeconds(2.0f);
        ui.ResetScene();
        playSlider = false;
    }
}
