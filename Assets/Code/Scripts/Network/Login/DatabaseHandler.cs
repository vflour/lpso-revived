using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Bson;

public class DatabaseHandler 
{
    public string uri;
    private MongoClient _client;
    private IMongoDatabase _database;

    public void Connect()
    {
        _client = new MongoClient(uri);
        _database =  _client.GetDatabase("lpso");
    }

    public UserData GetUserData(string guid)
    {
        var users = _database.GetCollection<UserData>("users");
        var filter = Builders<UserData>.Filter.Eq(user => user.guid, guid);
        return users.Find(filter).FirstOrDefault();
    }

    public void CreateUserData(UserData user)
    {
        var users = _database.GetCollection<UserData>("users");
        users.InsertOne(user);
    }

    public bool UserExists(string guid)
    {
        return GetUserData(guid) != null;
    }
}
