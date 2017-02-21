using UnityEngine;
using System.Collections;

public class ChangeTurn : MonoBehaviour {

    GameMaster gm;
    public bool turnChanged;
    public string piecePlaced;
    public string spawnName;

    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        spawnName = transform.GetComponent<SpawnOnClick>().spawnpoint.name;
    }

    void OnMouseDown()
    {
        if (spawnName == "Row1Col1")
        {
            gm.row1[0] = piecePlaced;
        }
        else if (spawnName == "Row1Col2")
        {
            gm.row1[1] = piecePlaced;
        }
        else if (spawnName == "Row1Col3")
        {
            gm.row1[2] = piecePlaced;
        }
        else if (spawnName == "Row2Col1")
        {
            gm.row2[0] = piecePlaced;
        }
        else if (spawnName == "Row2Col2")
        {
            gm.row2[1] = piecePlaced;
        }
        else if (spawnName == "Row2Col3")
        {
            gm.row2[2] = piecePlaced;
        }
        else if (spawnName == "Row3Col1")
        {
            gm.row3[0] = piecePlaced;
        }
        else if (spawnName == "Row3Col2")
        {
            gm.row3[1] = piecePlaced;
        }
        else if (spawnName == "Row3Col3")
        {
            gm.row3[2] = piecePlaced;
        }

        gm.p1T = !gm.p1T;
        gm.p2T = !gm.p2T;
        turnChanged = true;
        gm.setPieces = true;//set the flag to switch the piece to spawn
    }
}
