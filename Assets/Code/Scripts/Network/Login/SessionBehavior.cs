using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SessionBehavior
{
    private Dictionary<string, PublicUserData> _usernameToUser;
    private DatabaseHandler _databaseHandler;
    
    public Dictionary<string, string> guidToUsername = new Dictionary<string, string>();
    public Dictionary<ulong, string> sessionToGuid = new Dictionary<ulong, string>();


    public SessionBehavior(string dbUri)
    {
        _databaseHandler = new DatabaseHandler();
        _databaseHandler.uri = dbUri;
    }

    public string ConnectUserToSession(ulong session, string userName)
    {
        if (!guidToUsername.ContainsValue(userName)) {
            var guid = System.Guid.NewGuid().ToString();
            sessionToGuid[session] = guid;
            guidToUsername[guid] = userName;

            UserData userData = new UserData();
            userData.guid = userName;
            userData.publicData.username = userName;

            _databaseHandler.Connect();
            if (!_databaseHandler.UserExists(guid))
            {
                _databaseHandler.CreateUserData(userData);
            }
            Debug.Log($"Connected user {userName}:{guid}");



            return guid;
        }
        return null;
    }

    public void DisconnectUserFromSession(ulong session, string guid)
    {
        if (sessionToGuid[session] == guid)
        {
            sessionToGuid.Remove(session);
            guidToUsername.Remove(guid);
            Debug.Log($"Disconnected user {session}:{guid}");
        }
    }

}
