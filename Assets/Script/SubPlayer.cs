using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globle;

public class SubPlayer : MonoBehaviour
{
    public int playerType;

    public enumPlayerState myCurrState = 0;
    public Animator animator;

    public Renderer renderTorso;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void setData(int type, enumPlayerState playerState,Material material)
    {
        playerType = type;
        myCurrState = playerState;

        Material[] mat = renderTorso.materials;
        mat[1] = material;
        renderTorso.materials = mat;
    }

    public void setPlayerState(enumPlayerState playerState)
    {
        myCurrState = playerState;
        animator.SetTrigger(playerState.ToString());
    }
    public void endAnim()
    {
        setPlayerState(enumPlayerState.Idle);
    }
}
