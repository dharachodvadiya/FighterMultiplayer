using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : SimulationBehaviour
{
    public static GamePlayManager Instance;
    public GameObject PlayerPrefab;
    public GameObject SubPlayerPrefab;

    public Material matRed;
    public Material matBlue;

    public Player myPlayer;

    Vector3 redPos = new Vector3(-2, -1.7f, -5);
    Vector3 bluePos = new Vector3(2, -1.7f, -5);

    Quaternion redRotation = Quaternion.Euler(0, 90, 0);
    Quaternion blueRotation = Quaternion.Euler(0, -90, 0);

    //public GameObject objRotate;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private ConnectionManager connectionManager;
    // Start is called before the first frame update
    void Start()
    {

        connectionManager = GameObject.FindAnyObjectByType<ConnectionManager>();
        

        if (Globle.currPlayerType == Globle.enumPlayerType.Red)
        {
            myPlayer = connectionManager.SpawnNetworkObject(PlayerPrefab, redPos, redRotation);
        }
        else
        {
            myPlayer = connectionManager.SpawnNetworkObject(PlayerPrefab, bluePos, blueRotation);
        }

    }

    #region Button_click

    public void btn_attack1Click()
    {
        myPlayer.setPlayerState(Globle.enumPlayerState.Attack1);
    }
    public void btn_attack2Click()
    {
        myPlayer.setPlayerState(Globle.enumPlayerState.Attack2);
    }
    public void btn_dieClick()
    {
        myPlayer.setPlayerState(Globle.enumPlayerState.Dead);
    }
    public void btn_coverClick()
    {
        myPlayer.setPlayerState(Globle.enumPlayerState.cover);
    }

    #endregion

}
