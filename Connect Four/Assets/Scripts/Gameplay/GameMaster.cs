using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameMaster : MonoBehaviour {

    public bool p1T;
    public bool p2T;
    public bool p1Win;
    public bool p2Win;
    public bool draw;
    public bool canSpawn = true;
    public float spawnTimer = 1.0f;
    Pieces gamePiece;
    SpawnOnClick[] spawnTriggers;
    UI ui;
    GameObject[] spawns;
    public bool setPieces = true;//don't run the loops needlessly
    bool checkForWin;
    public GameObject[] slot1arr, slot2arr, slot3arr, slot4arr, slot5arr, slot6arr, slot7arr;
    public int s1Count = 0, s2Count = 0, s3Count = 0, s4Count = 0, s5Count = 0, s6Count = 0, s7Count = 0;
    Checker[] slot1Win, slot2Win, slot3Win, slot4Win, slot5Win, slot6Win, slot7Win;

    void Start ()
    {
        //holds each checker in correct slot once placed
        slot1arr = new GameObject[6];
        slot2arr = new GameObject[6];
        slot3arr = new GameObject[6];
        slot4arr = new GameObject[6];
        slot5arr = new GameObject[6];
        slot6arr = new GameObject[6];
        slot7arr = new GameObject[6];
        //keeps track of checker attributes/win state
        slot1Win = new Checker[6];
        slot2Win = new Checker[6];
        slot3Win = new Checker[6];
        slot4Win = new Checker[6];
        slot5Win = new Checker[6];
        slot6Win = new Checker[6];
        slot7Win = new Checker[6];

        p1T = true;
        p2T = false;

        gamePiece = transform.GetComponent<Pieces>();
        ui = GameObject.Find("Main Camera").GetComponent<UI>();
    }
	
	void Update ()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawn");
        
        if (p1T && setPieces)
        {
            canSpawn = false;
            checkForWin = true;
            StartCoroutine(waitForSpawn());
            ui.playerTurnText();
            for (int i = 0; i < spawns.Length; i++)
            {
                spawns[i].GetComponent<SpawnOnClick>().itemToSpawn = gamePiece.gamePieces[0];

                //print(spawns[i]);//debug
                //print("Setting Pieces");//debug
            }
            setPieces = false;
        }
        else if (p2T && setPieces)
        {
            canSpawn = false;
            checkForWin = true;
            StartCoroutine(waitForSpawn());
            ui.playerTurnText();
            for (int i = 0; i < spawns.Length; i++)
            {
                spawns[i].GetComponent<SpawnOnClick>().itemToSpawn = gamePiece.gamePieces[1];
                
                //print(spawns[i]);//debug
                //print("Setting Pieces");//debug
            }
            setPieces = false;
        }

        if (checkForWin)//check current board state for player wins
        {
            detectWin();
            checkForWin = false;
        }

        if (p1Win || p2Win || draw)//disable adding game pieces if win/draw
        {
            for (int i = 0; i < spawns.Length; i++)
            {
                spawns[i].GetComponent<Collider>().enabled = false;
            }
        }
    }

    void detectWin()
    {
        for (int i = 0; i <= 2; i++)
        {
            SetWinArr(i);
        }

        /************** /
        /--Wall of Win--/
        /***************/
        for (int i = 0;i <= 2; i++)//Diagonal "up" win cases
        {
            //SetWinArr(i);
            //check indexes of slot arrays 1-4
            if (slot1arr[i] != null && slot1Win[i].color == "Red" &&
                slot2arr[i + 1] != null && slot2Win[i + 1].color == "Red" &&
                slot3arr[i + 2] != null && slot3Win[i + 2].color == "Red" &&
                slot4arr[i + 3] != null && slot4Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot1Win[i].winningChecker = true;
                slot2Win[i + 1].winningChecker = true;
                slot3Win[i + 2].winningChecker = true;
                slot4Win[i + 3].winningChecker = true;
            }
            else if (slot1arr[i] != null && slot1Win[i].color == "Black" &&
                slot2arr[i + 1] != null && slot2Win[i + 1].color == "Black" &&
                slot3arr[i + 2] != null && slot3Win[i + 2].color == "Black" &&
                slot4arr[i + 3] != null && slot4Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot1Win[i].winningChecker = true;
                slot2Win[i + 1].winningChecker = true;
                slot3Win[i + 2].winningChecker = true;
                slot4Win[i + 3].winningChecker = true;
            }
            //check indexes of slot arrays 2-5
            if (slot2arr[i] != null && slot2Win[i].color == "Red" &&
                slot3arr[i + 1] != null && slot3Win[i + 1].color == "Red" &&
                slot4arr[i + 2] != null && slot4Win[i + 2].color == "Red" &&
                slot5arr[i + 3] != null && slot5Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot2Win[i].winningChecker = true;
                slot3Win[i + 1].winningChecker = true;
                slot4Win[i + 2].winningChecker = true;
                slot5Win[i + 3].winningChecker = true;
            }
            else if (slot2arr[i] != null && slot2Win[i].color == "Black" &&
                slot3arr[i + 1] != null && slot3Win[i + 1].color == "Black" &&
                slot4arr[i + 2] != null && slot4Win[i + 2].color == "Black" &&
                slot5arr[i + 3] != null && slot5Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot2Win[i].winningChecker = true;
                slot3Win[i + 1].winningChecker = true;
                slot4Win[i + 2].winningChecker = true;
                slot5Win[i + 3].winningChecker = true;
            }
            //check indexes of slot arrays 3-6
            if (slot3arr[i] != null && slot3Win[i].color == "Red" &&
                slot4arr[i + 1] != null && slot4Win[i + 1].color == "Red" &&
                slot5arr[i + 2] != null && slot5Win[i + 2].color == "Red" &&
                slot6arr[i + 3] != null && slot6Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i + 1].winningChecker = true;
                slot5Win[i + 2].winningChecker = true;
                slot6Win[i + 3].winningChecker = true;
            }
            else if (slot3arr[i] != null && slot3Win[i].color == "Black" &&
                slot4arr[i + 1] != null && slot4Win[i + 1].color == "Black" &&
                slot5arr[i + 2] != null && slot5Win[i + 2].color == "Black" &&
                slot6arr[i + 3] != null && slot6Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i + 1].winningChecker = true;
                slot5Win[i + 2].winningChecker = true;
                slot6Win[i + 3].winningChecker = true;
            }
            //check indexes of slot arrays 4-7
            if (slot4arr[i] != null && slot4Win[i].color == "Red" &&
                slot5arr[i + 1] != null && slot5Win[i + 1].color == "Red" &&
                slot6arr[i + 2] != null && slot6Win[i + 2].color == "Red" &&
                slot7arr[i + 3] != null && slot7Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i + 1].winningChecker = true;
                slot6Win[i + 2].winningChecker = true;
                slot7Win[i + 3].winningChecker = true;
            }
            else if (slot4arr[i] != null && slot4Win[i].color == "Black" &&
                slot5arr[i + 1] != null && slot5Win[i + 1].color == "Black" &&
                slot6arr[i + 2] != null && slot6Win[i + 2].color == "Black" &&
                slot7arr[i + 3] != null && slot7Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i + 1].winningChecker = true;
                slot6Win[i + 2].winningChecker = true;
                slot7Win[i + 3].winningChecker = true;
            }
        }
        
        for (int i = 5; i >= 3; i--)//Diagonal "down" win cases
        {
            //SetWinArr(i);
            //check indexes of slot arrays 1-4
            if (slot1arr[i] != null && slot1Win[i].color == "Red" &&
                slot2arr[i - 1] != null && slot2Win[i - 1].color == "Red" &&
                slot3arr[i - 2] != null && slot3Win[i - 2].color == "Red" &&
                slot4arr[i - 3] != null && slot4Win[i - 3].color == "Red")
            {
                p1Win = true;
                slot1Win[i].winningChecker = true;
                slot2Win[i - 1].winningChecker = true;
                slot3Win[i - 2].winningChecker = true;
                slot4Win[i - 3].winningChecker = true;
            }
            else if (slot1arr[i] != null && slot1Win[i].color == "Black" &&
                slot2arr[i - 1] != null && slot2Win[i - 1].color == "Black" &&
                slot3arr[i - 2] != null && slot3Win[i - 2].color == "Black" &&
                slot4arr[i - 3] != null && slot4Win[i - 3].color == "Black")
            {
                p2Win = true;
                slot1Win[i].winningChecker = true;
                slot2Win[i - 1].winningChecker = true;
                slot3Win[i - 2].winningChecker = true;
                slot4Win[i - 3].winningChecker = true;
            }
            //check indexes of slot arrays 2-5
            if (slot2arr[i] != null && slot2Win[i].color == "Red" &&
                slot3arr[i - 1] != null && slot3Win[i - 1].color == "Red" &&
                slot4arr[i - 2] != null && slot4Win[i - 2].color == "Red" &&
                slot5arr[i - 3] != null && slot5Win[i - 3].color == "Red")
            {
                p1Win = true;
                slot2Win[i].winningChecker = true;
                slot3Win[i - 1].winningChecker = true;
                slot4Win[i - 2].winningChecker = true;
                slot5Win[i - 3].winningChecker = true;
            }
            else if (slot2arr[i] != null && slot2Win[i].color == "Black" &&
                slot3arr[i - 1] != null && slot3Win[i - 1].color == "Black" &&
                slot4arr[i - 2] != null && slot4Win[i - 2].color == "Black" &&
                slot5arr[i - 3] != null && slot5Win[i - 3].color == "Black")
            {
                p2Win = true;
                slot2Win[i].winningChecker = true;
                slot3Win[i - 1].winningChecker = true;
                slot4Win[i - 2].winningChecker = true;
                slot5Win[i - 3].winningChecker = true;
            }
            //check indexes of slot arrays 3-6
            if (slot3arr[i] != null && slot3Win[i].color == "Red" &&
                slot4arr[i - 1] != null && slot4Win[i - 1].color == "Red" &&
                slot5arr[i - 2] != null && slot5Win[i - 2].color == "Red" &&
                slot6arr[i - 3] != null && slot6Win[i - 3].color == "Red")
            {
                p1Win = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i - 1].winningChecker = true;
                slot5Win[i - 2].winningChecker = true;
                slot6Win[i - 3].winningChecker = true;
            }
            else if (slot3arr[i] != null && slot3Win[i].color == "Black" &&
                slot4arr[i - 1] != null && slot4Win[i - 1].color == "Black" &&
                slot5arr[i - 2] != null && slot5Win[i - 2].color == "Black" &&
                slot6arr[i - 3] != null && slot6Win[i - 3].color == "Black")
            {
                p2Win = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i - 1].winningChecker = true;
                slot5Win[i - 2].winningChecker = true;
                slot6Win[i - 3].winningChecker = true;
            }
            //check indexes of slot arrays 4-7
            if (slot4arr[i] != null && slot4Win[i].color == "Red" &&
                slot5arr[i - 1] != null && slot5Win[i - 1].color == "Red" &&
                slot6arr[i - 2] != null && slot6Win[i - 2].color == "Red" &&
                slot7arr[i - 3] != null && slot7Win[i - 3].color == "Red")
            {
                p1Win = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i - 1].winningChecker = true;
                slot6Win[i - 2].winningChecker = true;
                slot7Win[i - 3].winningChecker = true;
            }
            else if (slot4arr[i] != null && slot4Win[i].color == "Black" &&
                slot5arr[i - 1] != null && slot5Win[i - 1].color == "Black" &&
                slot6arr[i - 2] != null && slot6Win[i - 2].color == "Black" &&
                slot7arr[i - 3] != null && slot7Win[i - 3].color == "Black")
            {
                p2Win = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i - 1].winningChecker = true;
                slot6Win[i - 2].winningChecker = true;
                slot7Win[i - 3].winningChecker = true;
            }
        }
        for (int i = 0; i <= 5; i++)//Horizontal win cases
        {
            //SetWinArr(i);

            //check indexes of slot arrays 1-4
            if (slot1arr[i] != null && slot1Win[i].color == "Red" &&
                slot2arr[i] != null && slot2Win[i].color == "Red" &&
                slot3arr[i] != null && slot3Win[i].color == "Red" &&
                slot4arr[i] != null && slot4Win[i].color == "Red")
            {
                p1Win = true;
                slot1Win[i].winningChecker = true;
                slot2Win[i].winningChecker = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i].winningChecker = true;
            }
            else if (slot1arr[i] != null && slot1Win[i].color == "Black" &&
                slot2arr[i] != null && slot2Win[i].color == "Black" &&
                slot3arr[i] != null && slot3Win[i].color == "Black" &&
                slot4arr[i] != null && slot4Win[i].color == "Black")
            {
                p2Win = true;
                slot1Win[i].winningChecker = true;
                slot2Win[i].winningChecker = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i].winningChecker = true;
            }
            //check indexes of slot arrays 2-5
            if (slot2arr[i] != null && slot2Win[i].color == "Red" &&
                slot3arr[i] != null && slot3Win[i].color == "Red" &&
                slot4arr[i] != null && slot4Win[i].color == "Red" &&
                slot5arr[i] != null && slot5Win[i].color == "Red")
            {
                p1Win = true;
                slot2Win[i].winningChecker = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i].winningChecker = true;
            }
            else if (slot2arr[i] != null && slot2Win[i].color == "Black" &&
                slot3arr[i] != null && slot3Win[i].color == "Black" &&
                slot4arr[i] != null && slot4Win[i].color == "Black" &&
                slot5arr[i] != null && slot5Win[i].color == "Black")
            {
                p2Win = true;
                slot2Win[i].winningChecker = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i].winningChecker = true;
            }
            //check indexes of slot arrays 3-6
            if (slot3arr[i] != null && slot3Win[i].color == "Red" &&
                slot4arr[i] != null && slot4Win[i].color == "Red" &&
                slot5arr[i] != null && slot5Win[i].color == "Red" &&
                slot6arr[i] != null && slot6Win[i].color == "Red")
            {
                p1Win = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i].winningChecker = true;
                slot6Win[i].winningChecker = true;
            }
            else if (slot3arr[i] != null && slot3Win[i].color == "Black" &&
                slot4arr[i] != null && slot4Win[i].color == "Black" &&
                slot5arr[i] != null && slot5Win[i].color == "Black" &&
                slot6arr[i] != null && slot6Win[i].color == "Black")
            {
                p2Win = true;
                slot3Win[i].winningChecker = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i].winningChecker = true;
                slot6Win[i].winningChecker = true;
            }
            //check indexes of slot arrays 4-7
            if (slot4arr[i] != null && slot4Win[i].color == "Red" &&
                slot5arr[i] != null && slot5Win[i].color == "Red" &&
                slot6arr[i] != null && slot6Win[i].color == "Red" &&
                slot7arr[i] != null && slot7Win[i].color == "Red")
            {
                p1Win = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i].winningChecker = true;
                slot6Win[i].winningChecker = true;
                slot7Win[i].winningChecker = true;
            }
            else if (slot4arr[i] != null && slot4Win[i].color == "Black" &&
                slot5arr[i] != null && slot5Win[i].color == "Black" &&
                slot6arr[i] != null && slot6Win[i].color == "Black" &&
                slot7arr[i] != null && slot7Win[i].color == "Black")
            {
                p2Win = true;
                slot4Win[i].winningChecker = true;
                slot5Win[i].winningChecker = true;
                slot6Win[i].winningChecker = true;
                slot7Win[i].winningChecker = true;
            }
        }
        for (int i = 0; i <= 2; i++)//Vertical win cases
        {

            //SetWinArr(i);

            //slot 1
            if (slot1arr[i] != null && slot1Win[i].color == "Red" &&
                slot1arr[i + 1] != null && slot1Win[i + 1].color == "Red" &&
                slot1arr[i + 2] != null && slot1Win[i + 2].color == "Red" &&
                slot1arr[i + 3] != null && slot1Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot1Win[i].winningChecker = true;
                slot1Win[i + 1].winningChecker = true;
                slot1Win[i + 2].winningChecker = true;
                slot1Win[i + 3].winningChecker = true;
            }
            else if (slot1arr[i] != null && slot1Win[i].color == "Black" &&
                slot1arr[i + 1] != null && slot1Win[i + 1].color == "Black" &&
                slot1arr[i + 2] != null && slot1Win[i + 2].color == "Black" &&
                slot1arr[i + 3] != null && slot1Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot1Win[i].winningChecker = true;
                slot1Win[i + 1].winningChecker = true;
                slot1Win[i + 2].winningChecker = true;
                slot1Win[i + 3].winningChecker = true;
            }
            //slot 2
            if (slot2arr[i] != null && slot2Win[i].color == "Red" &&
                slot2arr[i + 1] != null && slot2Win[i + 1].color == "Red" &&
                slot2arr[i + 2] != null && slot2Win[i + 2].color == "Red" &&
                slot2arr[i + 3] != null && slot2Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot2Win[i].winningChecker = true;
                slot2Win[i + 1].winningChecker = true;
                slot2Win[i + 2].winningChecker = true;
                slot2Win[i + 3].winningChecker = true;
            }
            else if (slot2arr[i] != null && slot2Win[i].color == "Black" &&
                slot2arr[i + 1] != null && slot2Win[i + 1].color == "Black" &&
                slot2arr[i + 2] != null && slot2Win[i + 2].color == "Black" &&
                slot2arr[i + 3] != null && slot2Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot2Win[i].winningChecker = true;
                slot2Win[i + 1].winningChecker = true;
                slot2Win[i + 2].winningChecker = true;
                slot2Win[i + 3].winningChecker = true;
            }
            //slot 3
            if (slot3arr[i] != null && slot3Win[i].color == "Red" &&
                slot3arr[i + 1] != null && slot3Win[i + 1].color == "Red" &&
                slot3arr[i + 2] != null && slot3Win[i + 2].color == "Red" &&
                slot3arr[i + 3] != null && slot3Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot3Win[i].winningChecker = true;
                slot3Win[i + 1].winningChecker = true;
                slot3Win[i + 2].winningChecker = true;
                slot3Win[i + 3].winningChecker = true;
            }
            else if (slot3arr[i] != null && slot3Win[i].color == "Black" &&
                slot3arr[i + 1] != null && slot3Win[i + 1].color == "Black" &&
                slot3arr[i + 2] != null && slot3Win[i + 2].color == "Black" &&
                slot3arr[i + 3] != null && slot3Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot3Win[i].winningChecker = true;
                slot3Win[i + 1].winningChecker = true;
                slot3Win[i + 2].winningChecker = true;
                slot3Win[i + 3].winningChecker = true;
            }
            //slot 4
            if (slot4arr[i] != null && slot4Win[i].color == "Red" &&
                slot4arr[i + 1] != null && slot4Win[i + 1].color == "Red" &&
                slot4arr[i + 2] != null && slot4Win[i + 2].color == "Red" &&
                slot4arr[i + 3] != null && slot4Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot4Win[i].winningChecker = true;
                slot4Win[i + 1].winningChecker = true;
                slot4Win[i + 2].winningChecker = true;
                slot4Win[i + 3].winningChecker = true;
            }
            else if (slot4arr[i] != null && slot4Win[i].color == "Black" &&
                slot4arr[i + 1] != null && slot4Win[i + 1].color == "Black" &&
                slot4arr[i + 2] != null && slot4Win[i + 2].color == "Black" &&
                slot4arr[i + 3] != null && slot4Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot4Win[i].winningChecker = true;
                slot4Win[i + 1].winningChecker = true;
                slot4Win[i + 2].winningChecker = true;
                slot4Win[i + 3].winningChecker = true;
            }
            //slot 5
            if (slot5arr[i] != null && slot5Win[i].color == "Red" &&
                slot5arr[i + 1] != null && slot5Win[i + 1].color == "Red" &&
                slot5arr[i + 2] != null && slot5Win[i + 2].color == "Red" &&
                slot5arr[i + 3] != null && slot5Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot5Win[i].winningChecker = true;
                slot5Win[i + 1].winningChecker = true;
                slot5Win[i + 2].winningChecker = true;
                slot5Win[i + 3].winningChecker = true;
            }
            else if (slot5arr[i] != null && slot5Win[i].color == "Black" &&
                slot5arr[i + 1] != null && slot5Win[i + 1].color == "Black" &&
                slot5arr[i + 2] != null && slot5Win[i + 2].color == "Black" &&
                slot5arr[i + 3] != null && slot5Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot5Win[i].winningChecker = true;
                slot5Win[i + 1].winningChecker = true;
                slot5Win[i + 2].winningChecker = true;
                slot5Win[i + 3].winningChecker = true;
            }
            //slot 6
            if (slot6arr[i] != null && slot6Win[i].color == "Red" &&
                slot6arr[i + 1] != null && slot6Win[i + 1].color == "Red" &&
                slot6arr[i + 2] != null && slot6Win[i + 2].color == "Red" &&
                slot6arr[i + 3] != null && slot6Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot6Win[i].winningChecker = true;
                slot6Win[i + 1].winningChecker = true;
                slot6Win[i + 2].winningChecker = true;
                slot6Win[i + 3].winningChecker = true;
            }
            else if (slot6arr[i] != null && slot6Win[i].color == "Black" &&
                slot6arr[i + 1] != null && slot6Win[i + 1].color == "Black" &&
                slot6arr[i + 2] != null && slot6Win[i + 2].color == "Black" &&
                slot6arr[i + 3] != null && slot6Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot6Win[i].winningChecker = true;
                slot6Win[i + 1].winningChecker = true;
                slot6Win[i + 2].winningChecker = true;
                slot6Win[i + 3].winningChecker = true;
            }
            //slot 7
            if (slot7arr[i] != null && slot7Win[i].color == "Red" &&
                slot7arr[i + 1] != null && slot7Win[i + 1].color == "Red" &&
                slot7arr[i + 2] != null && slot7Win[i + 2].color == "Red" &&
                slot7arr[i + 3] != null && slot7Win[i + 3].color == "Red")
            {
                p1Win = true;
                slot7Win[i].winningChecker = true;
                slot7Win[i + 1].winningChecker = true;
                slot7Win[i + 2].winningChecker = true;
                slot7Win[i + 3].winningChecker = true;
            }
            else if (slot7arr[i] != null && slot7Win[i].color == "Black" &&
                slot7arr[i + 1] != null && slot7Win[i + 1].color == "Black" &&
                slot7arr[i + 2] != null && slot7Win[i + 2].color == "Black" &&
                slot7arr[i + 3] != null && slot7Win[i + 3].color == "Black")
            {
                p2Win = true;
                slot7Win[i].winningChecker = true;
                slot7Win[i + 1].winningChecker = true;
                slot7Win[i + 2].winningChecker = true;
                slot7Win[i + 3].winningChecker = true;
            }
        }

        if (p1Win)
        {
            ui.Win();
            //print("Player 1\nWins!");
        }
        if (p2Win)
        {
            ui.Win();
            //print("Player 2\nWins!");
        }
        else
        {
            for(int i = 0; i < 5; i++)
            {
                if(s1Count == 6 && s2Count == 6 && s3Count == 6 && s4Count == 6 && s5Count == 6 && s6Count == 6 && s7Count == 6 && !(p1Win || p2Win))
                {
                    draw = true;
                    ui.Win();
                    //print("Draw!");
                }
            }
        }
    }

    void SetWinArr(int index)
    {
        if (index < 3)//don't let array go out of bounds
        {
            //slot1
            if (slot1arr[index] != null)
            {
                slot1Win[index] = slot1arr[index].GetComponent<Checker>();
            }
            if (slot1arr[index + 1] != null)
            {
                slot1Win[index + 1] = slot1arr[index + 1].GetComponent<Checker>();
            }
            if (slot1arr[index + 2] != null)
            {
                slot1Win[index + 2] = slot1arr[index + 2].GetComponent<Checker>();
            }
            if (slot1arr[index + 3] != null)
            {
                slot1Win[index + 3] = slot1arr[index + 3].GetComponent<Checker>();
            }
            //slot2
            if (slot2arr[index] != null)
            {
                slot2Win[index] = slot2arr[index].GetComponent<Checker>();
            }
            if (slot2arr[index + 1] != null)
            {
                slot2Win[index + 1] = slot2arr[index + 1].GetComponent<Checker>();
            }
            if (slot2arr[index + 2] != null)
            {
                slot2Win[index + 2] = slot2arr[index + 2].GetComponent<Checker>();
            }
            if (slot2arr[index + 3] != null)
            {
                slot2Win[index + 3] = slot2arr[index + 3].GetComponent<Checker>();
            }
            //slot3
            if (slot3arr[index] != null)
            {
                slot3Win[index] = slot3arr[index].GetComponent<Checker>();
            }
            if (slot3arr[index + 1] != null)
            {
                slot3Win[index + 1] = slot3arr[index + 1].GetComponent<Checker>();
            }
            if (slot3arr[index + 2] != null)
            {
                slot3Win[index + 2] = slot3arr[index + 2].GetComponent<Checker>();
            }
            if (slot3arr[index + 3] != null)
            {
                slot3Win[index + 3] = slot3arr[index + 3].GetComponent<Checker>();
            }
            //slot4
            if (slot4arr[index] != null)
            {
                slot4Win[index] = slot4arr[index].GetComponent<Checker>();
            }
            if (slot4arr[index + 1] != null)
            {
                slot4Win[index + 1] = slot4arr[index + 1].GetComponent<Checker>();
            }
            if (slot4arr[index + 2] != null)
            {
                slot4Win[index + 2] = slot4arr[index + 2].GetComponent<Checker>();
            }
            if (slot4arr[index + 3] != null)
            {
                slot4Win[index + 3] = slot4arr[index + 3].GetComponent<Checker>();
            }
            //slot5
            if (slot5arr[index] != null)
            {
                slot5Win[index] = slot5arr[index].GetComponent<Checker>();
            }
            if (slot5arr[index + 1] != null)
            {
                slot5Win[index + 1] = slot5arr[index + 1].GetComponent<Checker>();
            }
            if (slot5arr[index + 2] != null)
            {
                slot5Win[index + 2] = slot5arr[index + 2].GetComponent<Checker>();
            }
            if (slot5arr[index + 3] != null)
            {
                slot5Win[index + 3] = slot5arr[index + 3].GetComponent<Checker>();
            }
            //slot6
            if (slot6arr[index] != null)
            {
                slot6Win[index] = slot6arr[index].GetComponent<Checker>();
            }
            if (slot6arr[index + 1] != null)
            {
                slot6Win[index + 1] = slot6arr[index + 1].GetComponent<Checker>();
            }
            if (slot6arr[index + 2] != null)
            {
                slot6Win[index + 2] = slot6arr[index + 2].GetComponent<Checker>();
            }
            if (slot6arr[index + 3] != null)
            {
                slot6Win[index + 3] = slot6arr[index + 3].GetComponent<Checker>();
            }
            //slot7
            if (slot7arr[index] != null)
            {
                slot7Win[index] = slot7arr[index].GetComponent<Checker>();
            }
            if (slot7arr[index + 1] != null)
            {
                slot7Win[index + 1] = slot7arr[index + 1].GetComponent<Checker>();
            }
            if (slot7arr[index + 2] != null)
            {
                slot7Win[index + 2] = slot7arr[index + 2].GetComponent<Checker>();
            }
            if (slot7arr[index + 3] != null)
            {
                slot7Win[index + 3] = slot7arr[index + 3].GetComponent<Checker>();
            }
        }
    }

    IEnumerator waitForSpawn()
    {
        yield return new WaitForSeconds(spawnTimer);
        canSpawn = true;
    }
}

public abstract class Tags
{
    public static string pieceSpawn = "Spawn";
    public static string slide = "Slide";
    public static string checker = "Checker";
}
