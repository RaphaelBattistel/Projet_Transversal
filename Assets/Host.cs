using UnityEngine;

public class Host
{
    public string username;
    public string sessionID;

    public bool startTimer;
    public int currentQuestion;


    public Host(string username, string sessionID, bool startTimer, int currentQuestion)
    {
        this.username = username;
        this.sessionID = sessionID;
        this.startTimer = startTimer;
        this.currentQuestion = currentQuestion;
    }

    
}
