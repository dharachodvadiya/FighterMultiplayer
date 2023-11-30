using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globle
{
    public enum enumPlayerState
    {
        Idle,
        Attack1,
        Attack2,
        cover,
        Dead
    }

    public enum enumPlayerType
    {
        Red = 1,
        Blue = 2
    }

    public static enumPlayerType currPlayerType = enumPlayerType.Red;
}
