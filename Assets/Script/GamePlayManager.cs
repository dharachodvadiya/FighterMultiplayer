using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Globle;
using static SceneChangeManager;

public class GamePlayManager : SimulationBehaviour
{
    public static GamePlayManager Instance;
    public GameObject PlayerPrefab;
    public GameObject SubPlayerPrefab;

    public Material matRed;
    public Material matBlue;

    public Player myPlayer;
    public Player oppoPlayer;

    public Image imgLeftHealth;
    public Image imgRightHealth;

    public Text txtLeftHealth;
    public Text txtRightHealth;

    Vector3 redPos = new Vector3(-0.7f, -1.7f, -5);
    Vector3 bluePos = new Vector3(0.7f, -1.7f, -5);

    Quaternion redRotation = Quaternion.Euler(0, 90, 0);
    Quaternion blueRotation = Quaternion.Euler(0, -90, 0);

    bool isResultDeclare = false;

    public GameObject objWin;
    public Text txtWin;

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
            txtLeftHealth.text = "Me";
            txtRightHealth.text = "Opponent";
        }
        else
        {
            myPlayer = connectionManager.SpawnNetworkObject(PlayerPrefab, bluePos, blueRotation);
            txtLeftHealth.text = "Opponent";
            txtRightHealth.text = "Me";
        }

    }

    public void countHealth()
    {
        int reduceHealth = 0;
        string bothState = ((int)myPlayer.myCurrState).ToString() + ((int)oppoPlayer.myCurrState).ToString();
        Debug.Log(bothState);
        switch(bothState)
        {
            case "00":
                reduceHealth = 0;
                break;
            case "01":
                reduceHealth = 10;
                break;
            case "02":
                reduceHealth = 15;
                break;
            case "03":
                reduceHealth = 0;
                break;
            case "10":
                reduceHealth = 0;
                break;
            case "11":
                reduceHealth = 0;
                break;
            case "12":
                reduceHealth = 5;
                break;
            case "13":
                reduceHealth = 0;
                break;
            case "20":
                reduceHealth = 0;
                break;
            case "21":
                reduceHealth = 5;
                break;
            case "22":
                reduceHealth = 0;
                break;
            case "23":
                reduceHealth = 0;
                break;
            case "30":
                reduceHealth = 0;
                break;
            case "31":
                reduceHealth = 0;
                break;
            case "32":
                reduceHealth = 0;
                break;
            case "33":
                reduceHealth = 0;
                break;

        }

        myPlayer.NetworkedHealth -= reduceHealth;

       
    }

    public void setHealthData()
    {
        if(!isResultDeclare)
        {
            if (myPlayer != null && oppoPlayer != null)
            {
                if (myPlayer.playerType == (int)enumPlayerType.Red)
                {
                    imgLeftHealth.fillAmount = myPlayer.NetworkedHealth / 100;
                    imgRightHealth.fillAmount = oppoPlayer.NetworkedHealth / 100;
                }
                else
                {
                    imgLeftHealth.fillAmount = oppoPlayer.NetworkedHealth / 100;
                    imgRightHealth.fillAmount = myPlayer.NetworkedHealth / 100;
                }

                if (myPlayer.NetworkedHealth <= 0 || oppoPlayer.NetworkedHealth <= 0)
                {
                    if(myPlayer.NetworkedHealth <= 0)
                    {
                        myPlayer.setPlayerState(Globle.enumPlayerState.Dead);
                        declareResult(false);
                    }
                    else
                    {
                        declareResult(true);
                    }
                    
                }
            }
        }
        
      
    }

    public void declareResult(bool meWin)
    {
        if(!isResultDeclare)
        {
            isResultDeclare = true;
            objWin.SetActive(true);

            if (meWin)
            {
                txtWin.text = "You Win";
            }
            else
            {
                txtWin.text = "Opponent Win";
            }
        }
        
        
    }

    #region Button_click

    public void btn_attack1Click()
    {
        if(!isResultDeclare)
            myPlayer.setPlayerState(Globle.enumPlayerState.Attack1);
    }
    public void btn_attack2Click()
    {
        if (!isResultDeclare)
            myPlayer.setPlayerState(Globle.enumPlayerState.Attack2);
    }
    public void btn_dieClick()
    {
        if (!isResultDeclare)
            myPlayer.setPlayerState(Globle.enumPlayerState.Dead);
    }
    public void btn_coverClick()
    {
        if (!isResultDeclare)
            myPlayer.setPlayerState(Globle.enumPlayerState.cover);
    }

    public void btn_exitXlixk()
    {
        FindObjectOfType<ConnectionManager>().endSession();
        SceneChangeManager.Instance.LoadNextScreen(EnumScene.home);
    }

    #endregion

}
