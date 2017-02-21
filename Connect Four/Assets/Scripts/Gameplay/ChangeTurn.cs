using UnityEngine;
using System.Collections;

public class ChangeTurn : MonoBehaviour {

    GameMaster gm;
    public bool turnChanged;
    public string piecePlaced;
    public string spawnName;
    public int slotIndex;
    public SpawnOnClick pieceSpawn;
    public GameObject piece;
    Checker checkerInfo;

    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        pieceSpawn = transform.GetComponent<SpawnOnClick>();
        spawnName = pieceSpawn.spawnpoint.name;
    }

    void OnMouseDown()
    {
        if(pieceSpawn.itemClone != null)
        {
            piece = pieceSpawn.itemClone;
            checkerInfo = piece.GetComponent<Checker>();
        }

        if (gm.canSpawn)
        {
            if (gm.p1T)
            {
                piecePlaced = "Red";
            }
            if (gm.p2T)
            {
                piecePlaced = "Black";
            }

            if (spawnName == "Slot1")
            {
                checkerInfo.color = piecePlaced;
                checkerInfo.slotNum = 0;
                checkerInfo.slotIndex = gm.s1Count;
                gm.slot1arr[gm.s1Count] = piece;//put the piece with info into the corresponding slot arr
                gm.s1Count++;
                if (gm.s1Count > 5)//disable piece placement
                {
                    transform.GetComponent<Collider>().enabled = false;
                }
            }
            else if (spawnName == "Slot2")
            {
                checkerInfo.color = piecePlaced;
                checkerInfo.slotNum = 1;
                checkerInfo.slotIndex = gm.s2Count;
                gm.slot2arr[gm.s2Count] = piece;//put the piece with info into the corresponding slot arr

                gm.s2Count++;
                if (gm.s2Count > 5)//disable piece placement
                {
                    transform.GetComponent<Collider>().enabled = false;
                }
            }
            else if (spawnName == "Slot3")
            {
                checkerInfo.color = piecePlaced;
                checkerInfo.slotNum = 2;
                checkerInfo.slotIndex = gm.s3Count;
                gm.slot3arr[gm.s3Count] = piece;//put the piece with info into the corresponding slot arr

                gm.s3Count++;
                if (gm.s3Count > 5)//disable piece placement
                {
                    transform.GetComponent<Collider>().enabled = false;
                }
            }
            else if (spawnName == "Slot4")
            {
                checkerInfo.color = piecePlaced;
                checkerInfo.slotNum = 3;
                checkerInfo.slotIndex = gm.s4Count;
                gm.slot4arr[gm.s4Count] = piece;//put the piece with info into the corresponding slot arr

                gm.s4Count++;
                if (gm.s4Count > 5)//disable piece placement
                {
                    transform.GetComponent<Collider>().enabled = false;
                }
            }
            else if (spawnName == "Slot5")
            {
                checkerInfo.color = piecePlaced;
                checkerInfo.slotNum = 4;
                checkerInfo.slotIndex = gm.s5Count;
                gm.slot5arr[gm.s5Count] = piece;//put the piece with info into the corresponding slot arr

                gm.s5Count++;
                if (gm.s5Count > 5)//disable piece placement
                {
                    transform.GetComponent<Collider>().enabled = false;
                }
            }
            else if (spawnName == "Slot6")
            {
                checkerInfo.color = piecePlaced;
                checkerInfo.slotNum = 5;
                checkerInfo.slotIndex = gm.s6Count;
                gm.slot6arr[gm.s6Count] = piece;//put the piece with info into the corresponding slot arr

                gm.s6Count++;
                if (gm.s6Count > 5)//disable piece placement
                {
                    transform.GetComponent<Collider>().enabled = false;
                }
            }
            else if (spawnName == "Slot7")
            {
                checkerInfo.color = piecePlaced;
                checkerInfo.slotNum = 6;
                checkerInfo.slotIndex = gm.s7Count;
                gm.slot7arr[gm.s7Count] = piece;//put the piece with info into the corresponding slot arr

                gm.s7Count++;
                if (gm.s7Count > 5)//disable piece placement
                {
                    transform.GetComponent<Collider>().enabled = false;
                }
            }

            gm.p1T = !gm.p1T;
            gm.p2T = !gm.p2T;
            turnChanged = true;
            gm.setPieces = true;//set the flag to switch the piece to spawn
        }
    }
}
