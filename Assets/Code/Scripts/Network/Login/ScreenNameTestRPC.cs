using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;
using TMPro;

public class ScreenNameTestRPC : NetworkBehaviour
{
    public TMP_InputField usernameField;
    public SessionBehavior sessionBehavior;
    public UnityEvent<string, string> successfulConnection;
    public string guid;

    public string dbURI;
    
    public override void OnNetworkSpawn()
    {
        if (IsOwner && IsServer)
        {
            sessionBehavior = new SessionBehavior(dbURI);
        }
    }
    public void SubmitScreenname() 
    {
        if (usernameField.text.Length > 0)
        {
            ServerConnectScreennameRpc(usernameField.text);
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void ClientReceiveGuidRpc(string newGuid, string newUsername)
    {
        if (IsOwner)
        {
            guid = newGuid;
            successfulConnection.Invoke(newGuid, newUsername);
        }
    }

    [Rpc(SendTo.Server, AllowTargetOverride = true)]
    public void ServerConnectScreennameRpc(string screenName, RpcParams rpcParams = default) 
    {
        ulong session = rpcParams.Receive.SenderClientId;
        string guid = sessionBehavior.ConnectUserToSession(session, screenName);
        if (guid != null)
        {
            // todo: filter screenname
            ClientReceiveGuidRpc(guid, screenName);
        }
    }

    [Rpc(SendTo.Server, AllowTargetOverride = true)]
    public void ServerDisconnectRpc(string guid, RpcParams rpcParams = default) 
    {
        ulong session = rpcParams.Receive.SenderClientId;
        sessionBehavior.DisconnectUserFromSession(session, guid);
    }

}
