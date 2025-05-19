using UnityEngine;

public class User
{
    public string username;
    public string sessionID;

    public User(string username, string sessionID)
    {
        this.username = username;
        this.sessionID = sessionID;
    }
}
