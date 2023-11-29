using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : SimulationBehaviour
{
    public static GamePlayManager Instance;
    public GameObject PlayerPrefab;
    public GameObject SubPlayerPrefab;

    public GameObject objRotate;

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
        connectionManager.SpawnNetworkObject(PlayerPrefab);

        //if(Globle.currPlayerType == Globle.enumPlayerType.Blue)
        //{
        //    objRotate.transform.rotation = Quaternion.Euler(0, 180, 0);
        //}
    }

}
