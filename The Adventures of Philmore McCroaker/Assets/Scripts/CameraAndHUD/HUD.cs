using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour {

    gameMaster gm;

    public Sprite[] HeartSprites;
    public Sprite[] FrogCoinSprites;
    public Image HeartUI;
    public Image frogCoin;
    public GameObject hudObject;
    public Text time;

    void Start()
    {
        gm = gameMaster.master;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "End")
        {
            hudObject.SetActive(false);
        }
        else
        {
            hudObject.SetActive(true);
        }

        if (!gm.dead)
        {
            if (gm.life <= gm.maxLife)
            {
                HeartUI.sprite = HeartSprites[gm.life];
            }
        }
        else
        {
            HeartUI.sprite = HeartSprites[0];
        }

        if (gm.frogCoin)
        {
            frogCoin.sprite = FrogCoinSprites[0];
        }
        else
        {
            frogCoin.sprite = FrogCoinSprites[1];
        }

        time.text = "Time: " + gm.levelTime.ToString("F0");
    }

}
