using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MenuManager;

public class ConnectionManager : SimulationBehaviour, INetworkRunnerCallbacks
{
    private GameStartCallBack GameStartCall;

    private Player MyPlayer;

    public void addStartGameCallback(GameStartCallBack gameStartCallBack)
    {
        GameStartCall = gameStartCallBack;
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");
        //throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("OnConnectFailed");
        //throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("OnConnectRequest");
        // throw new NotImplementedException();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("OnCustomAuthenticationResponse");
        // throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("OnDisconnectedFromServer");
        //throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("OnHostMigration");
        //throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        // Debug.Log("OnInput");
        //throw new NotImplementedException();

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        Debug.Log("OnInputMissing");
        // throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined.." + runner.SessionInfo.Name + ".." + runner.SessionInfo.PlayerCount);

        if(runner.SessionInfo.PlayerCount == 2)
        {
            //SceneChangeManager.Instance.LoadNextScreen(SceneChangeManager.EnumScene.play);
            if (GameStartCall != null)
                GameStartCall.Invoke();
        }
        // throw new NotImplementedException();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerLeft");
        //throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log("OnReliableDataReceived");
        //throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("OnSceneLoadDone");
        //throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("OnSceneLoadStart");
        // throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log("OnSessionListUpdated");
        // throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("OnShutdown" + shutdownReason.ToString());
        // throw new NotImplementedException();

        //if (shutdownReason == ShutdownReason.GameNotFound)
        //{
        //    createSession(GameMode.Server);
        //}
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("OnUserSimulationMessage");
        // throw new NotImplementedException();
    }

    public Player SpawnNetworkObject(GameObject PlayerPrefab,Vector3 pos, Quaternion rotation)
    {
        MyPlayer = Runner.Spawn(PlayerPrefab, pos, rotation, Runner.LocalPlayer).GetComponent<Player>();
        MyPlayer.setData((int)Globle.currPlayerType);
        return MyPlayer;

    }
}
