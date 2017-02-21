using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameMaster : MonoBehaviour {

    public static gameMaster master;

    //game vars
    public bool paused;
    public bool consoleOpen;
    public float levelTime;
    public float winTime;

    //level vars
    public float levelTimeLimit = 400;
    public int startLife;

    //player vars
    public Frog player;
    public int life = 1;
    public int maxLife = 5;
    public bool hasCoin;
    public bool win;
    public bool dead;
    public bool frozen;
    public bool frogCoin;

    //cheat vars
    public string consoleMsg;
    bool godmode;
    bool teenyToggled;
    bool largeToggled;
    bool uLife;
    bool speedy;
    bool sJump;
    bool timeScl;
    int storedLife;
    float playerScale;
    float storedSpdX;
    float storedSpdY;
    float storedJmp;
    float storedTimeScale;

    

    void Awake()
    {
        if (master == null)
        {
            DontDestroyOnLoad(gameObject);
            master = this;
        }
        else if(master != this)
        {
            Destroy(gameObject);
        }
    }

    void Start ()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().name != "End")
        {
            player = GameObject.Find("Frog").GetComponent<Frog>();
            player.life = life;
        }
        //testing ground level
        if (SceneManager.GetActiveScene().name == "testing_Grounds")
        {
            //godmode = true;
        }

        startLife = life;
        levelTime = levelTimeLimit;
    }

    void OnLevelWasLoaded(int level)
    {
        startLife = life;
        levelTime = levelTimeLimit;
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "End")
        {
            player = GameObject.Find("Frog").GetComponent<Frog>();
        }
        win = false;
        frozen = false;
        frogCoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "End")
        {
            player.maxLife = maxLife;
            player.life = life;
            player.win = win;
            player.dead = dead;


            if (life <= 0)
            {
                dead = true;
            }
            else
            {
                dead = false;
            }

            if (frozen && !dead)
            {
                player.isFrozen = true;
            }
            else if (!player.hurt)
            {
                player.isFrozen = false;
            }

            if (!win)
            {
                if (levelTime > 0)
                {
                    levelTime = levelTimeLimit - Time.timeSinceLevelLoad;
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //cheat stuff//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //godmode
            if (godmode)
            {
                player.GodMode = true;
            }
            if (!godmode)
            {
                player.GodMode = false;
            }

            //unlimited lives
            if (uLife)
            {
                player.UnlimitedLives = true;
            }
            if (!uLife)
            {
                player.UnlimitedLives = false;
            }

            //player scale
            if (teenyToggled)
            {
                if (largeToggled)
                {
                    teenyToggled = false;
                    player.scaleX = 1.5f;
                    player.scaleY = 1.5f;
                }
                player.scaleX = 0.5f;
                player.scaleY = 0.5f;
            }
            if (largeToggled)
            {
                if (teenyToggled)
                {
                    largeToggled = false;
                    player.scaleX = 0.5f;
                    player.scaleY = 0.5f;
                }
                player.scaleX = 1.5f;
                player.scaleY = 1.5f;
            }
            if (!largeToggled && !teenyToggled)
            {
                player.scaleX = 1.0f;
                player.scaleY = 1.0f;
            }

            //slowmo
            if (!timeScl)
            {
                storedTimeScale = Time.timeScale;
            }
            if (timeScl)
            {
                Time.timeScale = 0.5f;
            }
            if (!timeScl)
            {
                Time.timeScale = storedTimeScale;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //end cheat stuff//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }

    public void WinActions()
    {
        bool getTime = false;

        frozen = true;

        if(!getTime)
        {
            winTime = levelTime;
            getTime = true;
        }
    }

    public void Cheats(string cmd)
    {
        //cheats
        switch (cmd)
        {
            case "/unflinching":
                godmode = !godmode;
                consoleMsg = "Invincibility toggled";
                if (godmode && uLife)
                {
                    player.UnlimitedLives = false;
                    consoleMsg = "Invincibility toggled\nUnlimited lives off";
                }
                player.GodMode = !player.GodMode;
                break;
            case "/unstoppable":
                if (!uLife)
                {
                    storedLife = life;
                }

                player.UnlimitedLives = !player.UnlimitedLives;

                if (player.UnlimitedLives)
                {
                    life = 5;
                    uLife = true;
                }
                if (!player.UnlimitedLives)
                {
                    life = storedLife;
                    uLife = false;
                }
                consoleMsg = "Unlimited lives - toggleable";
                break;
            case "/maxlife":
                life = 5;
                consoleMsg = "Max lives";
                break;
            case "/kill":
                life = 0;
                consoleMsg = "Took the easy way out...";
                break;
            case "/+life":
                life += 1;
                consoleMsg = "Life + 1";
                break;
            case "/-life":
                life -= 1;
                consoleMsg = "Life - 1";
                break;
            case "/teeny":
                teenyToggled = !teenyToggled;
                consoleMsg = "Teeny mode - toggleable";
                break;
            case "/wumbo":
                largeToggled = !largeToggled;
                consoleMsg = "Wumbo mode - toggleable";
                break;
            case "/speedy":
                if (!speedy)
                {
                    storedSpdX = player.maxSpeed;
                }

                speedy = !speedy;

                if (speedy)
                {
                    player.maxSpeed = player.maxSpeed * 2;
                }
                if (!speedy)
                {
                    player.maxSpeed = storedSpdX;
                }
                consoleMsg = "Speedy frog enabled - toggleable";
                break;
            case "/superjump":
                if (!sJump)
                {
                    storedJmp = player.jumpPower;
                    storedSpdY = player.maxVertSpeed;
                    storedSpdX = player.maxSpeed;
                }

                sJump = !sJump;

                if (sJump)
                {
                    player.jumpPower = player.jumpPower * 1.5f;
                    player.maxVertSpeed = player.maxVertSpeed * 2f;
                    player.maxSpeed = player.maxSpeed * 1.25f;
                }
                if (!sJump)
                {
                    player.jumpPower = storedJmp;
                    player.maxVertSpeed = storedSpdY;
                    player.maxSpeed = storedSpdX;
                }
                consoleMsg = "Super jump enabled - toggleable";
                break;
            case "/slowmo":
                timeScl = !timeScl;
                consoleMsg = "Slow Mo Toggled";
                break;
            //messages
            case "hello":
                consoleMsg = "it's me";
                break;
            //end messages
        }
    }
}
