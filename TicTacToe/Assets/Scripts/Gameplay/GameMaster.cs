using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public bool p1T;
    public bool p2T;
    public bool p1Win;
    public bool p2Win;
    public bool draw;
    Pieces gamePiece;
    SpawnOnClick[] spawnTriggers;
    ChangeTurn turn;
    UI ui;
    GameObject[] spawns;
    public bool setPieces = true;//don't run the loops needlessly
    public string[] row1;
    public string[] row2;
    public string[] row3;

    void Start ()
    {
        row1 = new string[3];
        row2 = new string[3];
        row3 = new string[3];

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
            detectWin();
            ui.playerTurnText();
            for (int i = 0; i < spawns.Length; i++)
            {
                spawns[i].GetComponent<SpawnOnClick>().itemToSpawn = gamePiece.gamePieces[0];
                spawns[i].GetComponent<ChangeTurn>().piecePlaced = "X";
                //print(spawns[i]);//debug
                //print("Setting Pieces");//debug
            }
            setPieces = false;
        }
        else if (p2T && setPieces)
        {
            detectWin();
            ui.playerTurnText();
            for (int i = 0; i < spawns.Length; i++)
            {
                spawns[i].GetComponent<SpawnOnClick>().itemToSpawn = gamePiece.gamePieces[1];
                spawns[i].GetComponent<ChangeTurn>().piecePlaced = "O";
                //print(spawns[i]);//debug
                //print("Setting Pieces");//debug
            }
            setPieces = false;
        }

        if(p1Win || p2Win || draw)//disable adding game pieces if win/draw
        {
            for (int i = 0; i < spawns.Length; i++)
            {
                spawns[i].GetComponent<Collider>().enabled = false;
            }
        }
    }

    void detectWin()
    {
        int drawCount = 0;
        //Player 1 win cases
        if (row1[0] == "X" && row1[1] == "X" && row1[2] == "X")//row 1 across
        {
            p1Win = true;
        }
        else if (row2[0] == "X" && row2[1] == "X" && row2[2] == "X")//row 2 across
        {
            p1Win = true;
        }
        else if (row3[0] == "X" && row3[1] == "X" && row3[2] == "X")//row 2 across
        {
            p1Win = true;
        }
        else if (row1[0] == "X" && row2[0] == "X" && row3[0] == "X")//col 1 down
        {
            p1Win = true;
        }
        else if (row1[1] == "X" && row2[1] == "X" && row3[1] == "X")//col 2 down
        {
            p1Win = true;
        }
        else if (row1[2] == "X" && row2[2] == "X" && row3[2] == "X")//col 3 down
        {
            p1Win = true;
        }
        else if (row1[0] == "X" && row2[1] == "X" && row3[2] == "X")//Diagonal 1
        {
            p1Win = true;
        }
        else if (row1[2] == "X" && row2[1] == "X" && row3[0] == "X")//Diagonal 1
        {
            p1Win = true;
        }
        //Player 2 win cases
        else if (row1[0] == "O" && row1[1] == "O" && row1[2] == "O")//row 1 across
        {
            p2Win = true;
        }
        else if (row2[0] == "O" && row2[1] == "O" && row2[2] == "O")//row 2 across
        {
            p2Win = true;
        }
        else if (row3[0] == "O" && row3[1] == "O" && row3[2] == "O")//row 2 across
        {
            p2Win = true;
        }
        else if (row1[0] == "O" && row2[0] == "O" && row3[0] == "O")//col 1 down
        {
            p2Win = true;
        }
        else if (row1[1] == "O" && row2[1] == "O" && row3[1] == "O")//col 2 down
        {
            p2Win = true;
        }
        else if (row1[2] == "O" && row2[2] == "O" && row3[2] == "O")//col 3 down
        {
            p2Win = true;
        }
        else if (row1[0] == "O" && row2[1] == "O" && row3[2] == "O")//Diagonal 1
        {
            p2Win = true;
        }
        else if (row1[2] == "O" && row2[1] == "O" && row3[0] == "O")//Diagonal 1
        {
            p2Win = true;
        }

        if(p1Win)
        {
            ui.Win();
            print("Player 1 Wins!");
        }
        if (p2Win)
        {
            ui.Win();
            print("Player 2 Wins!");
        }
        else
        {
            for(int i = 0; i < 3; i++)
            {
                if(row1[i] != null && row2[i] != null && row3[i] != null)
                {
                    drawCount++;
                }
                if(drawCount == 3 && !p1Win && !p2Win)
                {
                    draw = true;
                    ui.Win();
                    print("Draw!");
                }
            }
        }
    }
}


public abstract class Tags
{
    public static string pieceSpawn = "Spawn";
}
