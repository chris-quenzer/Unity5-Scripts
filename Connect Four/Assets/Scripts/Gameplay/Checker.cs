using UnityEngine;
using System.Collections;

public class Checker : MonoBehaviour {

    public string color;
    public int slotIndex;
    public int slotNum;
    public bool winningChecker;

    public float FadeDuration = 1f;
    public Color Color1;// = Color.gray;
    public Color Color2;// = Color.white;

    private Color startColor;
    private Color endColor;
    private float lastColorChangeTime;

    private Material material;

    void Start()
    {
        winningChecker = false;

        material = GetComponent<Renderer>().material;
        startColor = GetComponent<Renderer>().material.color;//Color1;
        endColor = Color.cyan;
    }

    void Update()
    {
        if(winningChecker)
        {
            var ratio = (Time.time - lastColorChangeTime) / FadeDuration;
            ratio = Mathf.Clamp01(ratio);
            //material.color = Color.Lerp(startColor, endColor, ratio);
            //material.color = Color.Lerp(startColor, endColor, Mathf.Sqrt(ratio)); // A cool effect
            material.color = Color.Lerp(startColor, endColor, ratio * ratio); // Another cool effect

            if (ratio == 1f)
            {
                lastColorChangeTime = Time.time;

                // Switch colors
                var temp = startColor;
                startColor = endColor;
                endColor = temp;
            }
        }
    }
}
