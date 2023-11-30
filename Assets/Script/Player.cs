using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globle;

public class Player : NetworkBehaviour
{
    [Networked(OnChanged = nameof(NetworPlayerTypeChanged))]
    public int playerType { get; set; }

    [Networked(OnChanged = nameof(NetworkStateChanged))]
    public int currState { get; set; }

    public enumPlayerState myCurrState;


    private int copyPlayer = 5;
    public SubPlayer[] subPlayers;

    public Animator animator;

    public Renderer renderTorso;

    public override void Spawned()
    {
        myCurrState = enumPlayerState.Idle;
        currState = (int)enumPlayerState.Idle;

        animator = GetComponent<Animator>();
        subPlayers = new SubPlayer[copyPlayer];
        Vector3 pos = transform.position;
        for (int i = 0; i < copyPlayer; i++)
        {
            pos.z += 2;
            subPlayers[i] = Instantiate(GamePlayManager.Instance.SubPlayerPrefab, pos, transform.rotation).GetComponent<SubPlayer>();
        }
    }

    private static void NetworPlayerTypeChanged(Changed<Player> changed)
    {
        changed.Behaviour.setColor();
    }

    public void setData(int type)
    {
        playerType = type;
        
    }

    public void setColor()
    {
        Material[] mat = renderTorso.materials;

        if (playerType == (int)Globle.enumPlayerType.Red)
        {
            mat[1] = GamePlayManager.Instance.matRed;
        }
        else
        {
            mat[1] = GamePlayManager.Instance.matBlue;
        }

        renderTorso.materials = mat;

        for (int i = 0; i < copyPlayer; i++)
        {
            subPlayers[i].setData(playerType, myCurrState, renderTorso.materials[1]);
        }

    }

    private static void NetworkStateChanged(Changed<Player> changed)
    {
        Debug.Log("network state change.." + ((enumPlayerState)changed.Behaviour.currState).ToString());
        changed.Behaviour.triggerAnimation((enumPlayerState)changed.Behaviour.currState);
    }
    public void setPlayerState(enumPlayerState playerState)
    {
        Debug.Log("state change.." + playerState.ToString());
        myCurrState = playerState;
        currState = (int)myCurrState;


    }

    public void triggerAnimation(enumPlayerState playerState)
    {
        if(playerState != enumPlayerState.Idle)
        {
            animator.SetTrigger(playerState.ToString());
            for (int i = 0; i < copyPlayer; i++)
            {
                subPlayers[i].setPlayerState(playerState);
            }
        }
       
        
    }

    public void endAnim()
    {
        setPlayerState(enumPlayerState.Idle);
    }
}
