using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globle;

public class Player : NetworkBehaviour
{
    public int playerType;

    private int copyPlayer = 5;
    public SubPlayer[] subPlayers;
    public override void Spawned()
    {      
        subPlayers = new SubPlayer[copyPlayer];
        Vector3 pos = transform.position;
        for (int i = 0; i < copyPlayer; i++)
        {
            pos.z += 2;
            subPlayers[i] = Instantiate(GamePlayManager.Instance.SubPlayerPrefab, pos, transform.rotation).GetComponent<SubPlayer>();
            subPlayers[i].setData(playerType);
        }
    }

    public void setData(int type)
    {
        playerType = type;
    }
}
