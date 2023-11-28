using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image btnRedbg;
    public Image btnBluebg;

    public Text txtBtnConfirm;
    // Start is called before the first frame update

    private NetworkRunner _runner;
    void Start()
    {
        if(Globle.currPlayerType == Globle.enumPlayerType.Red)
        {
            selectRed();
        }
        else
        {
            selectBlue();
        }
        
    }

    private void selectRed()
    {
        txtBtnConfirm.text = "Continue with Red";
        btnRedbg.color = Color.white;
        btnBluebg.color = Color.blue;
        Globle.currPlayerType = Globle.enumPlayerType.Red;
    }

    private void selectBlue()
    {
        txtBtnConfirm.text = "Continue with Blue";
        btnRedbg.color = Color.red;
        btnBluebg.color = Color.white;
        Globle.currPlayerType = Globle.enumPlayerType.Blue;
    }

    #region Button_click
    public void btn_RedClick()
    {
        selectRed();
    }

    public void btn_BlueClick()
    {
        selectBlue();
    }

    public void btn_continue()
    {
        joinSession(GameMode.Shared);
    }

    async void joinSession(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = new GameObject().AddComponent<NetworkRunner>();
        _runner.gameObject.AddComponent<ConnectionManager>();
        _runner.ProvideInput = true;

        var customProps = new Dictionary<string, SessionProperty>();

        switch (Globle.currPlayerType)
        {
            case Globle.enumPlayerType.Red:
                Debug.Log("join " + 1);
                customProps["T"] = 1;
                break;
            case Globle.enumPlayerType.Blue:
                Debug.Log("join " + 0);
                customProps["T"] = 0;
                break;
        }
        //customProps["T"] = (int)Globle.currPlayerType;
        

        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionProperties = customProps,
            DisableClientSessionCreation = true
        });

        if(_runner.IsShutdown)
        {
            createSession(GameMode.Shared);
        }
    }

    async void createSession(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = new GameObject().AddComponent<NetworkRunner>();
        _runner.gameObject.AddComponent<ConnectionManager>();
        _runner.ProvideInput = true;

        var customProps = new Dictionary<string, SessionProperty>();

        customProps["T"] = (int)Globle.currPlayerType;

        Debug.Log("Create " + (int)Globle.currPlayerType);
        // Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            SessionName = UnityEngine.Random.Range(0,1000)+"",
            GameMode = mode,
            SessionProperties = customProps,
        });;
    }
    #endregion

   
}
